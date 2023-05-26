using eCommerceWebsite.IRepositories;
using eCommerceWebsite.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace eCommerceWebsite.Controllers
{

    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
       
            private IUnit _unit;

            public CategoryController(IUnit unit)
            {
                _unit = unit;
            }

            public IActionResult Index()
            {
                CategoryVM categoryVM = new CategoryVM();
                categoryVM.categories = _unit.Category.GetAll();
                return View(categoryVM);
            }


        [HttpGet]

        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = _unit.Category.GetT(x => x.Id==id);
                if(vm.Category == null)
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
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Category.Id == 0)
                {
                    _unit.Category.Add(vm.Category);
                    TempData["success"] = "Category Created Done";
                }
                else
                {
                    _unit.Category.Update(vm.Category);
                    TempData["success"] = "Category Updated Done";
                }

                _unit.Save();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            var category = _unit.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteData(int? id)
        {
            var category = _unit.Category.GetT(x => x.Id==id);
            if (category == null)
            {
                return NotFound();
            }
            _unit.Category.Delete(category);
            _unit.Save();
            TempData["success"] = "Category Deleted Done!";
            return RedirectToAction("Index");
        }






    }
}
