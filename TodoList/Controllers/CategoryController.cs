using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.interfaces;
using TodoList.DataAccess;

namespace TodoList.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryDataProvider categoryDataProvider;

        public CategoryController(IDataProviderResolver dataProviderResolver)
        {
            this.categoryDataProvider = dataProviderResolver.GetCategoryDataProvider(SourceDataRepository.SourceName);
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
            CategoryModel model = categoryDataProvider.GetCategoryById(id);
            return View("EditCategoryPage", model);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCategoryPage", model);
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
