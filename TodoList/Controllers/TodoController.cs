using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.interfaces;
using TodoList.DataAccess;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private ITodoDataProvider todoDataProvider;

        private ICategoryDataProvider categoryDataProvider;

        public TodoController(IDataProviderResolver dataProviderResolver)
        {
            this.todoDataProvider = dataProviderResolver.GetTodoDataProvider(SourceDataRepository.SourceName);
            this.categoryDataProvider = dataProviderResolver.GetCategoryDataProvider(SourceDataRepository.SourceName);
        }

        [HttpPost]
        public ActionResult CreateTodo(TodoModel todoModel)
        {
            var completeList = todoDataProvider.GetCompleteTodo(null);
            var unCompleteList = todoDataProvider.GetUnCompleteTodo(null);
            var categoryList = categoryDataProvider.GetCategoryList();

            HomeModel homeModel = new HomeModel
            {
                CompleteTodoList = completeList,
                UnCompleteTodoList = unCompleteList,
                CategoryList = categoryList
            };

            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", homeModel);
            }

            todoDataProvider.CreateTodo(todoModel);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SolveTodo(int id)
        {
            todoDataProvider.SolveTodo(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetEditFormTodo(int id)
        {
            var todoModel = todoDataProvider.GetTodoById(id);
            return View("EditTodoPage", todoModel);
        }

        [HttpPost]
        public ActionResult UpdateTodo(TodoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Todo/EditTodoPage.cshtml", model);
            }

            todoDataProvider.UpdateTodo(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteTodo(int id)
        {
            todoDataProvider.DeleteTodo(id);
            return RedirectToAction("Index", "Home");
        }
    }
}