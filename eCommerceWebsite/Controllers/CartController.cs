using eCommerceWebsite.IRepositories;
using eCommerceWebsite.Models.SalesSubsystem;
using eCommerceWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using UserEmailSende;


namespace eCommerceWebsite.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private IUnit _unit;
        public CartVM vm { get; set; }
        public CartController(IUnit unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm = new CartVM()
            {
                ListOfCart = _unit.Cart.GetAll(x => x.eCommerceUserId == claims.Value, includeProperties: "Product"),
                OrderHeader = new /*eCommerceWebsite.Models.SalesSubsystem.*/OrderHeader()
            };


            foreach (var item in vm.ListOfCart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }


            return View(vm);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm = new CartVM()
            {
                ListOfCart = _unit.Cart.GetAll(x => x.eCommerceUserId == claims.Value, includeProperties: "Product"),
                OrderHeader = new eCommerceWebsite.Models.SalesSubsystem.OrderHeader()
            };
            vm.OrderHeader.eCommerceUser = _unit.eCommerceUser.GetT(x => x.Id == claims.Value);
            vm.OrderHeader.Name = vm.OrderHeader.eCommerceUser.FirstName;
            vm.OrderHeader.Phone = vm.OrderHeader.eCommerceUser.PhoneNumber;
            vm.OrderHeader.Address = vm.OrderHeader.eCommerceUser.Address;
            vm.OrderHeader.City = vm.OrderHeader.eCommerceUser.City;
            vm.OrderHeader.State = vm.OrderHeader.eCommerceUser.State;
            vm.OrderHeader.PostalCode = vm.OrderHeader.eCommerceUser.PinCode;



            foreach (var item in vm.ListOfCart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }


            return View(vm);
        }
        //[HttpPost]
        //public IActionResult Summary(CartVM vm)
        //{
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        //    vm.ListOfCart = _unit.Cart.GetAll(x => x.eCommerceUserId
        //    == claims.Value, includeProperties: "Product");
        //    vm.OrderHeader.OrderStatus = OrderStatus.StatusPending;
        //    vm.OrderHeader.PaymentStatus = PaymentStatus.StatusPending;
        //    vm.OrderHeader.DateOfOrder = DateTime.Now;
        //    vm.OrderHeader.eCommerceUserId = claims.Value;

        //    foreach (var item in vm.ListOfCart)
        //    {
        //        vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
        //    }
        //    _unit.OrderHeader.Add(vm.OrderHeader);
        //    _unit.Save();
        //    foreach (var item in vm.ListOfCart)
        //    {
        //        OrderDetail orderDetail = new OrderDetail()
        //        {
        //            ProductId = item.ProductId,
        //            OrderHeaderId = vm.OrderHeader.Id,
        //            Count = item.Count,
        //            Price = item.Product.Price
        //        };
        //        _unit.OrderDetail.Add(orderDetail);
        //        _unit.Save();
        //    }

        //    // Card Details Here
        //    var domain = "https://localhost:7129/";
        //    var options = new SessionCreateOptions
        //    {
        //        LineItems = new List<SessionLineItemOptions>(),
        //        Mode = "payment",
        //        SuccessUrl = domain + $"customer/cart/ordersuccess?id={vm.OrderHeader.Id}",
        //        CancelUrl = domain + $"customer/cart/Index",
        //    };

        //    foreach (var item in vm.ListOfCart)
        //    {

        //        var lineItemsOptions = new SessionLineItemOptions
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions
        //            {
        //                UnitAmount = (long)(item.Product.Price * 100),
        //                Currency = "INR",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = item.Product.Name,
        //                },

        //            },
        //            Quantity = item.Count,
        //        };
        //        options.LineItems.Add(lineItemsOptions);
        //    }

        //    var service = new SessionService();
        //    Session session = service.Create(options);
        //    _unit.OrderHeader.PaymentStatus(vm.OrderHeader.Id, session.Id, session.PaymentIntentId);
        //    _unit.Save();


        //    _unit.Cart.DeleteRange(vm.ListOfCart);
        //    _unit.Save();
        //    //extra code 

        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);


        //    return RedirectToAction("Index", "home");



        //}
        //public IActionResult ordersuccess(int id)
        //{

        //    var orderHeader = _unit.OrderHeader.GetT(x => x.Id == id);
        //    var service = new SessionService();
        //    Session session = service.Get(orderHeader.SessionId);
        //    if (session.PaymentStatus.ToLower() == "paid")
        //    {
        //        _unit.OrderHeader.UpdateStatus(id, OrderStatus.StatusApproved, PaymentStatus.StatusApproved);
        //    }
        //    List<Cart> cart = _unit.Cart.GetAll(x => x.eCommerceUserId == orderHeader.eCommerceUserId).ToList();
        //    _unit.Cart.DeleteRange(cart);
        //    _unit.Save();
        //    return View(id);




        //}
        public IActionResult plus(int id)
        {
            var cart = _unit.Cart.GetT(x => x.Id == id);
            _unit.Cart.IncrementCartItem(cart, 1);
            _unit.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int id)
        {
            var cart = _unit.Cart.GetT(x => x.Id == id);
            if (cart.Count <= 1)
            {
                _unit.Cart.Delete(cart);
                var count = _unit.Cart.GetAll(x => x.eCommerceUserId == cart.eCommerceUserId)
               .ToList().Count - 1;
                HttpContext.Session.SetInt32("SessionCart", count);

            }
            else
            {
                _unit.Cart.DecrementCartItem(cart, 1);
            }

            _unit.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult delete(int id)
        {
            var cart = _unit.Cart.GetT(x => x.Id == id);
            _unit.Cart.Delete(cart);
            _unit.Save();
            var count = _unit.Cart.GetAll(x => x.eCommerceUserId == cart.eCommerceUserId)
                .ToList().Count;
            HttpContext.Session.SetInt32("SessionCart", count);

            return RedirectToAction(nameof(Index));
        }



    }
}
