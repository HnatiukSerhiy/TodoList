using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.interfaces;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoDataProvider sqlProcedure;

        public TodoController(ITodoDataProvider procedure)
        {
            this.sqlProcedure = procedure;
        }   

        [HttpGet]
        public ActionResult GetCreateTodoForm()
        {
            return View("CreateTodo");
        }

        [HttpPost]
        public ActionResult CreateTodo(TodoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Todo/CreateTodo.cshtml", model);
            }

            sqlProcedure.CreateTodo(model); 
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SolveTodo(int id)
        {
            sqlProcedure.SolveTodo(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetEditFormTodo(int id)
        {
            var todoModel = sqlProcedure.EditTodo(id);

            return View("EditTodo", todoModel);
        }

        [HttpPost]
        public ActionResult UpdateTodo(TodoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Todo/EditTodo.cshtml", model);
            }

            sqlProcedure.UpdateTodo(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteTodo(int id)
        {
            sqlProcedure.DeleteTodo(id);
            return RedirectToAction("Index", "Home");
        }
    }
}