using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public OrdersController(AppDbContext dbContext, IMapper mapper, AppDbContext dbContext1)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _paymentProvider = new ZarinPalPaymentProvider();
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        public IActionResult Post([FromBody] OrderAddOrUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
            var order = _mapper.Map<Order>(model);
            order.PaymentStatus = PaymentStatus.Pending;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            LoadProductsAndCharity(order);
            FillOrderTotals(order);
            var orderVm = _mapper.Map<OrderViewModel>(order);
            orderVm.OffPrecentage = orderVm.TotalPriceSofre * 100 / orderVm.TotalPrice;
            return Ok(orderVm);
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
            _dbContext.Entry(order).Reference(x=>x.Charity).Load();
        }

        [HttpPost("confirmpayment/{orderId}/{authority}")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        public async Task<IActionResult> PostConfirm(int orderId, string authority)
        {
            var order = _dbContext.Orders
                .FirstOrDefault(x => x.Id == orderId);
            if (order is null) return BadRequest("این مورد وجود ندارد");
            var (paymentSuccess, refId) = await _paymentProvider.IsPaymnetValid(order.TotalPriceSofre, authority);
            if (paymentSuccess)
            {
                order.PaymentStatus = PaymentStatus.Paid;
                order.PaymentId = refId.ToString();
                _dbContext.Orders.Update(order);
                _dbContext.SaveChanges();

               
                return Ok(new {succes=true});
            }
            return BadRequest();
        }

      
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [HttpGet("{userIdentifier}")]
        public IActionResult Get(string userIdentifier, int pageSize = 12, int pageNumber = 1)
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Charity)
                .Where(x => x.UserIdentifier == userIdentifier && x.PaymentStatus==PaymentStatus.Paid)
                .OrderBy(x => x.CreatedDateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(_mapper.Map<List<OrderViewModel>>(orders));
        }
    }
}