using TodoList.interfaces;
using TodoList.Models;
using System.Xml;

namespace TodoList.DataAccess
{
    public class TodoXmlDataProvider : ITodoDataProvider
    {
        private readonly string todoXmlPath = @"D:\VS projects2022\TodoList\TodoList\App_data\Xml_storage\TodoList.xml";

        private readonly TodoBuilder todoBuilder = new TodoBuilder();

        private readonly XmlDocument xmlDocument = new XmlDocument();
    
        public IEnumerable<TodoModel> GetCompleteTodo(int? id)
        {
            xmlDocument.Load(todoXmlPath);

            List<TodoModel> todoModelsList = new List<TodoModel>();

            string idCondition = id != null ? $" and CategoryId='{id}'" : "";

            var xmlTodoList = xmlDocument.SelectNodes($"TodoList/Todo[IsDone='True'{idCondition}]");

            if (xmlTodoList != null)
            {
                foreach (XmlNode todoNode in xmlTodoList)
                {
                    todoModelsList.Add(todoBuilder.Buid(todoNode));
                }
            }

            return  todoModelsList.OrderBy(todo => todo.Deadline);
        }

        public IEnumerable<TodoModel> GetUnCompleteTodo(int? id)
        {
            xmlDocument.Load(todoXmlPath);

            List<TodoModel> todoModelsList = new List<TodoModel>();

            string idCondition = id != null ? $" and CategoryId='{id}'" : "";

            var xmlTodoList = xmlDocument.SelectNodes($"TodoList/Todo[IsDone='False'{idCondition}]");

            if (xmlTodoList != null)
            {
                foreach (XmlNode todoNode in xmlTodoList)
                {
                    todoModelsList.Add(todoBuilder.Buid(todoNode));
                }
            }

            return todoModelsList.OrderBy(todo => !todo.Deadline.HasValue).ThenBy(todo => todo.Deadline);
        }
        
        public TodoModel CreateTodo(TodoModel todoModel)
        {
            xmlDocument.Load(todoXmlPath);

            XmlNode parentNode = xmlDocument.SelectSingleNode("TodoList");
            XmlNode todoNode = xmlDocument.CreateElement("Todo");

            XmlNode idNode = xmlDocument.CreateElement("Id");
            XmlNode descpriptionNode = xmlDocument.CreateElement("Description");
            XmlNode isDoneNode = xmlDocument.CreateElement("IsDone");
            XmlNode deadlineNode = xmlDocument.CreateElement("Deadline");
            XmlNode doneTimeNode = xmlDocument.CreateElement("DoneTime");
            XmlNode categoryIdNode = xmlDocument.CreateElement("CategoryId");

            int maxId = 0;

            var xmlTodoList = xmlDocument.SelectNodes("TodoList/Todo");

            if (xmlTodoList != null)
            {
                foreach (XmlNode todo in xmlTodoList)
                {
                    int id = todo["Id"] != null ? Convert.ToInt32(todo["Id"].InnerText) : 0;

                    if (id > maxId)
                    {
                        maxId = id;
                    }

                }
            }

            ++maxId;

            string? deadlineDate = todoModel.Deadline.HasValue ? $"{todoModel.Deadline.Value.Year}-{todoModel.Deadline.Value.Month}-{todoModel.Deadline.Value.Day}" : null;

            idNode.InnerText = maxId.ToString();
            descpriptionNode.InnerText = todoModel.Description;
            isDoneNode.InnerText = todoModel.IsDone.ToString();
            deadlineNode.InnerText = todoModel.Deadline.HasValue ? deadlineDate : "";
            categoryIdNode.InnerText = todoModel.CategoryId.HasValue ? todoModel.CategoryId.ToString() : "";

            todoNode.AppendChild(idNode);
            todoNode.AppendChild(descpriptionNode);
            todoNode.AppendChild(isDoneNode);
            todoNode.AppendChild(deadlineNode);
            todoNode.AppendChild(doneTimeNode);
            todoNode.AppendChild(categoryIdNode);

            parentNode.AppendChild(todoNode);

            xmlDocument.Save(todoXmlPath);

            return todoModel;
        }

        public int SolveTodo(int id)
        {
            xmlDocument.Load(todoXmlPath);

            XmlNode todoNode = xmlDocument.SelectSingleNode($"TodoList/Todo[Id='{id}']");

            todoNode["IsDone"].InnerText = "True";
            todoNode["DoneTime"].InnerText = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}";

            xmlDocument.Save(todoXmlPath);

            return id;
        }

        public TodoModel GetTodoById(int id)
        {
            xmlDocument.Load(todoXmlPath);

            XmlNode? todoNode = xmlDocument.SelectSingleNode($"TodoList/Todo[Id='{id}']");

            return todoBuilder.Buid(todoNode);
        }

        public TodoModel UpdateTodo(TodoModel todoModel)
        {
            xmlDocument.Load(todoXmlPath);

            XmlNode todoNode = xmlDocument.SelectSingleNode($"TodoList/Todo[Id='{todoModel.Id}']");

            todoNode["Description"].InnerText = todoModel.Description;
            todoNode["Deadline"].InnerText = todoModel.Deadline.ToString();
            todoNode["CategoryId"].InnerText = todoModel.CategoryId.ToString();
            
            xmlDocument.Save(todoXmlPath);
        
            return todoModel;
        }

        public int DeleteTodo(int id)
        {
            xmlDocument.Load(todoXmlPath);

            XmlNode todoNode = xmlDocument.SelectSingleNode($"TodoList/Todo[Id='{id}']");
            XmlNode parentNode = todoNode.ParentNode;

            parentNode.RemoveChild(todoNode);

            xmlDocument.Save(todoXmlPath);

            return id;
        }
    }
}
