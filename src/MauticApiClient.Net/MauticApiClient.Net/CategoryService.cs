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

        public async Task<dynamic> GetById( string pId )
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try {
                var varResponse = await varClient.GetAsync("categories/" + pId);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in CategoryService->GetById", varErrorHandle);
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> GetList()
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try{
                var varResponse = await varClient.GetAsync("categories");
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in CategoryService->GetList", varErrorHandle);
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

        public async Task<dynamic> New(JObject pCategory)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try{
                var content = new StringContent(pCategory.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PostAsync("categories/new", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in CategoryService->New", varErrorHandle);
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> Edit(string pId, JObject pCategory)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try
            {
                var content = new StringContent(pCategory.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PutAsync("categories/" + pId + "/edit", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in CategoryService->Edit", varErrorHandle);
                JObject categoriesJObject = JObject.Parse(varJson);
                dynamic categoryItem = categoriesJObject["category"];
                return categoryItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task Delete(string pId)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try{
                var varResponse = await varClient.DeleteAsync("categories/" + pId + "/delete");
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in CategoryService->Delete", varErrorHandle);
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

    }
}
