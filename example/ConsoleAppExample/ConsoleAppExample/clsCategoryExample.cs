using System;
using MauticApiClient.Net;
using Newtonsoft.Json.Linq;

namespace ConsoleAppExample
{
    public class clsCategoryExample
    {
        public void funcExecute(HttpClientProvider pProvider)
        {
            //Create Object of CategoryService
            var categoryService = new CategoryService(pProvider);
            string varCategoryID, varCategoryTitle, varCategoryBundle;

            //Get Category List
            Console.WriteLine("----------------- Category List -----------------");
            var categoriesList = categoryService.GetList().Result;
            foreach (var category in categoriesList)
                Console.WriteLine("Category {0}# {1} {2}", category.id, category.title, category.bundle);

            //Get Category ID
            Console.WriteLine("---------------- Get Category ------------------");
            Console.Write("Category ID : "); varCategoryID = Console.ReadLine();
            if (!string.IsNullOrEmpty(varCategoryID)){
                var categoryItem = categoryService.GetById(varCategoryID).Result;
                Console.WriteLine("Category {0}# {1} {2}", categoryItem.id, categoryItem.title, categoryItem.bundle);
            }

            //Edit Category
            Console.WriteLine("----------------- Edit Category -----------------");
            var categoryEdit = new JObject();
            Console.Write("Category Title : "); varCategoryTitle = Console.ReadLine();
            categoryEdit.Add("title", varCategoryTitle);
            Console.Write("Category Bundle : "); varCategoryBundle = Console.ReadLine();
            categoryEdit.Add("bundle", varCategoryBundle);
            if(!string.IsNullOrEmpty(varCategoryTitle) && !string.IsNullOrEmpty(varCategoryBundle)){
                var categoryItem = categoryService.Edit(varCategoryID, categoryEdit).Result;
                Console.WriteLine("Category {0}# {1} {2}", categoryItem.id, categoryItem.title, categoryItem.bundle);
            }

            //Add Category
            Console.WriteLine("----------------- New Category -----------------");
            var categoryNew = new JObject();
            Console.Write("Category Title : "); varCategoryTitle = Console.ReadLine();
            categoryNew.Add("title", varCategoryTitle);
            Console.Write("Category Bundle : "); varCategoryBundle = Console.ReadLine();
            categoryNew.Add("bundle", varCategoryBundle);
            if (!string.IsNullOrEmpty(varCategoryTitle) && !string.IsNullOrEmpty(varCategoryBundle)){
                var categoryItem = categoryService.New(categoryNew).Result;
                Console.WriteLine("Category {0}# {1} {2}", categoryItem.id, categoryItem.title, categoryItem.bundle);
            }

            //Delete Category
            Console.WriteLine("---------------- Delete Category ----------------");
            Console.Write("Category ID : "); varCategoryID = Console.ReadLine();
            if (!string.IsNullOrEmpty(varCategoryID)){
                var varResultDelete = categoryService.Delete(varCategoryID);
                Console.WriteLine("Category Deleted {0}", varCategoryID);
            }
        }
    }
}
