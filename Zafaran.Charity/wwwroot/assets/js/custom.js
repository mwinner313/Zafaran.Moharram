$(document).ready(function (e) {
  $('#supporters-slider').slick({
    dots: false,
    rtl: true,
    infinite: true,
    speed: 300,
    slidesToShow: 5,
    prevArrow: '<img id="silder-prev" src="/assets/img/prev.png" alt=""/>',
    nextArrow: '<img id="silder-next" src="/assets/img/next.png" alt=""/>',
    responsive: [
      {
        breakpoint: 768,
        settings: {
          arrows: false,
          slidesToShow: 3
        }
      },
      {
        breakpoint: 480,
        settings: {
          arrows: false,
          slidesToShow: 1
        }
      }
    ],
   
  })
})
