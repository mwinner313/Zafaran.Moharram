using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.File.Protocol;
using Payment.Refah;
using Zafaran.Charity.Models;
using Zafaran.Charity.Services;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.Controllers
{
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ZarinPalPaymentProvider _paymentProvider;
        private readonly IOrderPaymentProviderFactory _orderPaymentProviderFactory;

        public OrdersController(AppDbContext dbContext, IMapper mapper, AppDbContext dbContext1,
            IOrderPaymentProviderFactory orderPaymentProviderFactory)
        {
            _mapper = mapper;
            _orderPaymentProviderFactory = orderPaymentProviderFactory;
            _dbContext = dbContext;
            _paymentProvider = new ZarinPalPaymentProvider();
        }

        private string GetDomain()
        {
            var domain = Request.Host.Value;
            if (!domain.StartsWith(Request.Scheme))
                domain = Request.Scheme + "://" + domain;
            return domain;
        }

        [HttpPut("update-description")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        public IActionResult Put([FromBody] OrderDescriptionUpdateModel model)
        {
            var order = _dbContext.Orders.Find(model.OrderId);
            order.Description = model.Description;
            _dbContext.Update(order);
            _dbContext.SaveChanges();
            return Ok(new {succes = true});
        }

        [HttpPut("changestate")]
        public IActionResult Patch_State([FromBody] ChangeOrderStateModel model)
        {
            var order = _dbContext.Orders.Find(model.OrderId);
            order.State = model.State;
            _dbContext.Update(order);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        public IActionResult Post([FromBody] OrderAddOrUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
            var order = _mapper.Map<Order>(model);
            order.State = OrderState.UnKnown;
            FillOrderItemProductPrice(order);
            order.PaymentStatus = PaymentStatus.Pending;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            LoadProductsAndCharity(order);
            FillOrderTotals(order);
            var orderVm = _mapper.Map<OrderViewModel>(order);
            orderVm.OffPrecentage = 100 - (orderVm.TotalPriceSofre * 100 / orderVm.TotalPrice);

            return Ok(new
            {
                orderVm,
                redirectUrl = GetDomain() + Url.Action("Create", new {orderId = order.Id})
            });
        }

        private void FillOrderItemProductPrice(Order order)
        {
            var productIds = order.OrderItems.Select(x => x.ProductId).ToList();
            var products = _dbContext.Products.Where(x => productIds.Contains(x.Id)).ToList();
            foreach (var item in order.OrderItems)
            {
                var product = products.First(x => x.Id == item.ProductId);
                item.ProductPrice = product.Price;
                item.ProductSofrehPrice = product.SofrehPrice;
            }
        }

        [HttpGet("Create")]
        public IActionResult Create(int orderId)
        {
            var provider = _orderPaymentProviderFactory.GetProvider(PaymentProviders.Refah);
            var order = _dbContext.Orders.Find(orderId);
            return View((RefahPaymentRequestResult) provider.CreatePayment(orderId, order.TotalPriceSofre,
                GetDomain() + Url.Action("PostConfirm")));
        }

        private void FillOrderTotals(Order order)
        {
            order.TotalPrice = order.OrderItems.Sum(x => x.Count * x.Product.Price);
            order.TotalPriceSofre = order.OrderItems.Sum(x => x.Count * x.Product.SofrehPrice);
            foreach (var item in order.OrderItems)
            {
                item.TotalPrice = item.Count * item.Product.Price;
                item.TotalPriceSofre = item.Count * item.Product.SofrehPrice;
            }

            order.OffPrecentage = 100 - (order.TotalPriceSofre * 100 / order.TotalPrice);
            _dbContext.SaveChanges();
        }

        private void LoadProductsAndCharity(Order order)
        {
            foreach (var orderItem in order.OrderItems)
                _dbContext.Entry(orderItem).Reference(x => x.Product).Load();
            _dbContext.Entry(order).Reference(x => x.Charity).Load();
        }

        [HttpPost("confirmpayment")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        public async Task<IActionResult> PostConfirm([FromForm] RefahConfirmationContext context)
        {
            var order = _dbContext.Orders
                .FirstOrDefault(x => x.Id == context.OrderId);
            if (order is null) return BadRequest("این مورد وجود ندارد");
            var paymentProvider = _orderPaymentProviderFactory.GetProvider(PaymentProviders.Refah);
            var paymentSuccess = paymentProvider.Confirm(context, order.TotalPriceSofre);
            if (paymentSuccess.Status == ResponseStatus.Success)
            {
                order.PaymentStatus = PaymentStatus.Paid;
                order.PaymentId = context.ResNum.ToString();
                _dbContext.Orders.Update(order);
                _dbContext.SaveChanges();
            }

            return View("RedirectToAndroidApp", context);
        }


        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [HttpGet("{userIdentifier}")]
        public IActionResult Get(string userIdentifier, int pageSize = 12, int pageNumber = 1)
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Charity)
                .Where(x => x.UserIdentifier == userIdentifier && x.PaymentStatus == PaymentStatus.Paid)
                .OrderByDescending(x => x.CreatedDateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(_mapper.Map<List<OrderViewModel>>(orders));
        }

        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [HttpGet("getAll")]
        public IActionResult GetAll(int pageSize = 15, int pageNumber = 1)
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Charity)
                .Where(x => x.PaymentStatus == PaymentStatus.Paid)
                .OrderByDescending(x => x.CreatedDateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(new
            {
                allCount = _dbContext.Orders.Count(x => x.PaymentStatus == PaymentStatus.Paid),
                items = _mapper.Map<List<OrderViewModel>>(orders)
            });
        }

        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [HttpGet("byId/{id}")]
        public IActionResult Get(int id)
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Charity)
                .FirstOrDefault(x => x.Id == id);
            return Ok(_mapper.Map<OrderViewModel>(orders));
        }
    }

    public class ChangeOrderStateModel
    {
        public int OrderId { get; set; }
        public OrderState State { get; set; }
    }
}