using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace eCommerceWebsite.Controllers
{
   // [Authorize]

    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;  /*Admin*/

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //List All the Role created by Users
        public ActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View();
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(IdentityRole model) //will be passing IdentityRole
        {
            //Avoid duplicate role
            //Check if role exists
            if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }


            return RedirectToAction("Index");
        }
    }
}
