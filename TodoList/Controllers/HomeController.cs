using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.interfaces;
using TodoList.DataAccess;


namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private ITodoDataProvider todoDataProvider;

        private ICategoryDataProvider categoryDataProvider;

        public HomeController(IDataProviderResolver dataProviderResolver)
        {
            this.todoDataProvider = dataProviderResolver.GetTodoDataProvider(SourceDataRepository.SourceName);
            this.categoryDataProvider = dataProviderResolver.GetCategoryDataProvider(SourceDataRepository.SourceName); 
        }
    
        [HttpGet]
        public ActionResult Index(int? categoryId)
        {
            var completeList = todoDataProvider.GetCompleteTodo(categoryId);
            var unCompleteList = todoDataProvider.GetUnCompleteTodo(categoryId);
            var categoryList = categoryDataProvider.GetCategoryList();

            HomeModel model = new HomeModel
            {
                CompleteTodoList = completeList,
                UnCompleteTodoList = unCompleteList,
                CategoryList = categoryList
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeDataSource(string dataSource)
        {
            SourceDataRepository.SourceName = dataSource;

            return RedirectToAction("Index");
        }
    }
}