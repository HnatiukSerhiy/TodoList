using TodoList.Models;
using System.Xml;

namespace TodoList.DataAccess
{
    public class CategoryBuilder
    {
        public CategoryModel Build(XmlNode categoryNode)
        {
            CategoryModel model = new CategoryModel
            {
                Id = Convert.ToInt32(categoryNode["Id"].InnerText),
                Name = categoryNode["Name"].InnerText,
            };

            return model;
        }
    }
}
