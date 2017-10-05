using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MauticApiClient.Net.Model;
using Newtonsoft.Json.Linq;

namespace MauticApiClient.Net
{
    public class CategoryService
    {
        private readonly IHttpClientProvider _httpClientProvider;

        public CategoryService(IHttpClientProvider httpClientProvider){
            _httpClientProvider = httpClientProvider;
        }

        public dynamic GetById( string pId )
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try {
                var varResponse = varClient.GetAsync("categories/" + pId).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.HasStatus("404")) { return null; } // Record Not Found
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in CategoryService->GetById", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public dynamic GetList()
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varResponse = varClient.GetAsync("categories").Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in CategoryService->GetList", varMessageHandler); }
                //Handle Data Returned from Rest API
                var categoriesJObject = JObject.Parse(varJson);
                dynamic categoriesList = categoriesJObject["categories"];
                if (varClient != null) varClient.Dispose();
                return categoriesList;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public dynamic New(JObject pCategory)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pCategory.ToString(), Encoding.UTF8, "application/json");
                var varResponse = varClient.PostAsync("categories/new", content).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in CategoryService->New", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public dynamic Edit(string pId, JObject pCategory)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pCategory.ToString(), Encoding.UTF8, "application/json");
                var varResponse = varClient.PutAsync("categories/" + pId + "/edit", content).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in CategoryService->Edit", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public dynamic Delete(string pId)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varResponse = varClient.DeleteAsync("categories/" + pId + "/delete").Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in CategoryService->Delete", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

    }
}
