using TodoList.Models;
using System.Xml;

namespace TodoList.DataAccess
{
    public class TodoBuilder
    {
        public TodoModel Buid(XmlNode node)
        {
            string categoryXmlPath = @"D:\VS projects2022\TodoList\TodoList\App_data\Xml_storage\Categories.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(categoryXmlPath);

            string id = node["CategoryId"].InnerText;

            XmlNode? categoryNode = doc.SelectSingleNode($"Categories/Category[Id='{id}']");
            XmlNode? categoryNameNode = categoryNode != null ? categoryNode.SelectSingleNode("Name") : null;

            TodoModel model = new TodoModel
            {
                Id = Convert.ToInt32(node["Id"].InnerText),
                Description = node["Description"].InnerText,
                Deadline = node["Deadline"].InnerText != "" ? Convert.ToDateTime(node["Deadline"].InnerText) : null,
                DoneTime = node["DoneTime"].InnerText != "" ? Convert.ToDateTime(node["DoneTime"].InnerText) : null,
                CategoryId = node["CategoryId"].InnerText != "" ? Convert.ToInt32(node["CategoryId"].InnerText) : null,
                CategoryName = categoryNameNode != null ? categoryNameNode.InnerText : null,
            };

            return model;
        }
    }
}
