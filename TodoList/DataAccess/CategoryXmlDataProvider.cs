using System.Xml;
using TodoList.Models;
using TodoList.interfaces;

namespace TodoList.DataAccess
{
    public class CategoryXmlDataProvider : ICategoryDataProvider
    {
        private readonly string categoryXmlPath = @"D:\VS projects2022\TodoList\TodoList\App_data\Xml_storage\Categories.xml";

        private readonly CategoryBuilder categoryBuilder = new CategoryBuilder();

        private readonly XmlDocument xmlDocument = new XmlDocument();
        
        public IEnumerable<CategoryModel> GetCategoryList()
        {
            xmlDocument.Load(categoryXmlPath);

            var categoryXmlList = xmlDocument.SelectNodes("Categories/Category");
            var categoryModelList = new List<CategoryModel>();

            if (categoryXmlList != null)
            {
                foreach (XmlNode categoryXml in categoryXmlList)
                {
                    categoryModelList.Add(categoryBuilder.Build(categoryXml));
                }
            }

            return categoryModelList;
        }

        public CategoryModel CreateCategory(CategoryModel categoryModel)
        {
            xmlDocument.Load(categoryXmlPath);

            XmlNode parentNode = xmlDocument.SelectSingleNode("Categories");
            XmlNode categoryNode = xmlDocument.CreateElement("Category");
            XmlNode categoryIdNode = xmlDocument.CreateElement("Id");
            XmlNode categoryNameNode = xmlDocument.CreateElement("Name");

            int maxId = 0;

            foreach (XmlNode idNode in xmlDocument.SelectNodes("Categories/Category/Id"))
            {
                int id = Convert.ToInt32(idNode.InnerText);

                if (id > maxId)
                {
                    maxId = id;
                }
            }

            ++maxId;

            categoryIdNode.InnerText = maxId.ToString();
            categoryNameNode.InnerText = categoryModel.Name;

            categoryNode.AppendChild(categoryIdNode);
            categoryNode.AppendChild(categoryNameNode);

            parentNode.AppendChild(categoryNode);

            xmlDocument.Save(categoryXmlPath);

            return categoryModel;
        }

        public CategoryModel GetCategoryById(int id)
        {
            xmlDocument.Load(categoryXmlPath);

            XmlNode categoryXmlNode = xmlDocument.SelectSingleNode($"Categories/Category[Id={id}]");

            return categoryBuilder.Build(categoryXmlNode);
        }

        public CategoryModel UpdateCategory(CategoryModel categoryModel)
        {
            xmlDocument.Load(categoryXmlPath);

            XmlNode categoryNode = xmlDocument.SelectSingleNode($"Categories/Category[Id={categoryModel.Id}]");

            categoryNode["Name"].InnerText = categoryModel.Name;

            xmlDocument.Save(categoryXmlPath);

            return categoryModel;
        }

        public int DeleteCategory(int id)
        {
            xmlDocument.Load(categoryXmlPath);

            XmlNode categoryNode = xmlDocument.SelectSingleNode($"Categories/Category[Id={id}]");
            XmlNode parentNode = categoryNode.ParentNode;
           
            parentNode.RemoveChild(categoryNode);

            xmlDocument.Save(categoryXmlPath);

            return id;
        }
    }
}
