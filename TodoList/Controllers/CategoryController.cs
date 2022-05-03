using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.interfaces;

namespace TodoList.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryDataProvider categoryDataProvider;

        public CategoryController(ICategoryDataProvider categoryProvider)
        {
            this.categoryDataProvider = categoryProvider;
        }

        [HttpGet]
        public ActionResult GetCategoryList()
        {
            var categoryList = categoryDataProvider.GetCategoryList();
            return View("CategoryList", categoryList);
        }

        [HttpGet]
        public ActionResult GetCreatePage()
        {
            return View("CreateCategory");
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateCategory", model);
            }

            categoryDataProvider.CreateCategory(model);
            return RedirectToAction("GetCategoryList");
        }

        [HttpGet]
        public ActionResult GetEditPage(int id)
        {
            CategoryModel model = categoryDataProvider.EditCategory(id);
            return View("EditCategory", model);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCategory", model);
            }

            categoryDataProvider.UpdateCategory(model);
            return RedirectToAction("GetCategoryList");
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            categoryDataProvider.DeleteCategory(id);
            return RedirectToAction("GetCategoryList", "Category");
        }
    }
}
