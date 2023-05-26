using eCommerceWebsite.IRepositories;
using eCommerceWebsite.Models;
using eCommerceWebsite.Models.SalesSubsystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace eCommerceWebsite.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnit _unit;
        private readonly UserManager<eCommerceUser> _userManager;


        public HomeController(ILogger<HomeController> logger, IUnit unit)
        {
            _logger = logger;
            _unit = unit;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unit.Product.GetAll(includeProperties: "Category");

            //ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            return View(products);
        }

        [HttpGet]

        public IActionResult Details(int? productId)
        {
            if (productId == null)
            {
                // Handle the case when productId is null
                // For example, you could return a NotFound or BadRequest result
                return NotFound();
            }

            Cart cart = new Cart()
            {
                Product = _unit.Product.GetT(x => x.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = (int)productId

            };
            return View(cart);


        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]

        public IActionResult Details(Cart cart)
        {
            if (ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cart.eCommerceUserId = claims.Value;

                var cartItem = _unit.Cart.GetT(x => x.ProductId == cart.ProductId &&
                x.eCommerceUserId == claims.Value);

                if (cartItem == null)
                {
                    _unit.Cart.Add(cart);
                    _unit.Save();
                    //HttpContext.Session.SetInt32("SessionCart", _unit
                    //    .Cart.GetAll(x => x.eCommerceUserId == claims.Value).ToList().Count);

                }
                //else
                //{
                //    _unit.Cart.IncrementCartItem(cartItem, cart.Count);
                //    _unit.Save();

                //}



            }


            return RedirectToAction("Index");


        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}