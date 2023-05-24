using Microsoft.AspNetCore.Mvc;
using eCommerceWebsite.Models;
using eCommerceWebsite.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCommerceWebsite.Models.SalesSubsystem;
using eCommerceWebsite.ViewModels;
using eCommerceWebsite.IRepositories;

namespace eCommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        private IUnit _unit;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnit unit, IWebHostEnvironment hostEnvironment)
        {
            _unit = unit;
            _hostEnvironment = hostEnvironment;
        }
        #region APICALL

        public ActionResult AllProducts() 
        {
            var products = _unit.Product.GetAll(includeProperties: "Category");
            return Json(new {data = products});
        }
        #endregion

        public IActionResult Index()
        {
            //ProductVM productvm = new ProductVM();
            //ProductVM.Products = _unit.Product.GetAll();
            return View();
        }

        //[HttpGet]

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unit.Category.Add(category);
        //        _unit.Save();
        //        TempData["success"] = "Category created Done!";
        //        return RedirectToAction("Index");   
        //    }
        //    return View(category);
        //}

        [HttpGet]

        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                    Product = new(),
                    Categories = _unit.Category.GetAll().Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unit.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty; ;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostEnvironment.WebRootPath,
                        "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    if (vm.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath,
                            vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;
                }
                if (vm.Product.Id == 0)
                {
                    _unit.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done!";
                }
                else
                {
                    _unit.Product.Update(vm.Product);
                    TempData["success"] = "Product Update Done!";
                }

                _unit.Save();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null|| id==0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _unit.Category.GetT(x => x.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}

        #region DeleteAPICALL
        [HttpDelete]

        public IActionResult Delete(int? id)
        {
            var product = _unit.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error in Fetching Data" });
            }
            else
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath,
                         product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unit.Product.Delete(product);
                _unit.Save();
                return Json(new { success = true, message = "Product Removed" });
            }
        }
        #endregion


    }
}
