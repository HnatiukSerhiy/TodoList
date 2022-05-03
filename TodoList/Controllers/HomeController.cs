using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using TodoList.DataAccess;
using TodoList.interfaces;


namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoDataProvider todoSqlProcedure;

        private readonly ICategoryDataProvider categorySqlProcedure;

        public HomeController(ITodoDataProvider todoProcedure, ICategoryDataProvider categoryProcedure)
        {
            this.todoSqlProcedure = todoProcedure;
            this.categorySqlProcedure = categoryProcedure;
        }
    
        [HttpGet]
        public ActionResult Index(int? categoryId)
        {
            var completeList = todoSqlProcedure.GetCompleteTodo(categoryId);
            var unCompleteList = todoSqlProcedure.GetUnCompleteTodo(categoryId);
            var categoryList = categorySqlProcedure.GetCategoryList();

            HomeModel model = new HomeModel
            {
                CompleteTodoList = completeList,
                UnCompleteTodoList = unCompleteList,
                CategoryList = categoryList
            };

            return View(model);
        }
    }
}