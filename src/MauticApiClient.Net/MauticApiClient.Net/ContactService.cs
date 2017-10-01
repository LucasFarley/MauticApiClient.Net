using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MauticApiClient.Net.Model;
using Newtonsoft.Json.Linq;

namespace MauticApiClient.Net
{
    public class ContactService
    {
        private readonly IHttpClientProvider _httpClientProvider;

        public ContactService(IHttpClientProvider httpClientProvider){
            _httpClientProvider = httpClientProvider;
        }

        public async Task<dynamic> GetById( string pId )
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try {
                var varResponse = await varClient.GetAsync("contacts/" + pId);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in ContactService->GetById", varErrorHandle);
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> GetList(string pSearchParameter)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try{
                var varSearchString = "search=" + pSearchParameter;
                varSearchString = WebUtility.HtmlEncode(varSearchString);
                var varResponse = await varClient.GetAsync("contacts?" + varSearchString);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in ContactService->GetList", varErrorHandle);
                var contactsJObject = JObject.Parse(varJson);
                dynamic contactsList = contactsJObject["contacts"];
                if (varClient != null) varClient.Dispose();
                return contactsList;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> New(JObject pContact)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try{
                var content = new StringContent(pContact.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PostAsync("contacts/new", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in ContactService->New", varErrorHandle);
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> Edit(string pId, JObject pContact)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varErrorHandle = new Errorhandler();
            try
            {
                var content = new StringContent(pContact.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PutAsync("contacts/" + pId + "/edit", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in ContactService->Edit", varErrorHandle);
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
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
                var varResponse = await varClient.DeleteAsync("contacts/" + pId + "/delete");
                var varJson = await varResponse.Content.ReadAsStringAsync();
                if (varErrorHandle.TryToParse(varJson))
                    throw new MauticApiException("Exception in ContactService->Delete", varErrorHandle);
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

    }
}
