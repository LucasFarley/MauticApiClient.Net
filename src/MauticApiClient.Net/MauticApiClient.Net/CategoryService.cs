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

        public async Task<dynamic> GetByIdAsync( string pId )
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try {
                var varResponse = await varClient.GetAsync("categories/" + pId);
                var varJson = await varResponse.Content.ReadAsStringAsync();
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

        public async Task<dynamic> GetListAsync()
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varResponse = await varClient.GetAsync("categories");
                var varJson = await varResponse.Content.ReadAsStringAsync();
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

        public async Task<dynamic> NewAsync(JObject pCategory)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pCategory.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PostAsync("categories/new", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
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

        public async Task<dynamic> EditAsync(string pId, JObject pCategory)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pCategory.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PutAsync("categories/" + pId + "/edit", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
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

        public async Task<dynamic> DeleteAsync(string pId)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varResponse = await varClient.DeleteAsync("categories/" + pId + "/delete");
                var varJson = await varResponse.Content.ReadAsStringAsync();
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
