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

        public dynamic GetById( string pId )
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try {
                var varResponse = varClient.GetAsync("contacts/" + pId).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.HasStatus("404")) { return null; } // Record Not Found
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in ContactService->GetById", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public dynamic GetList(string pSearchParameter)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varSearchString = "search=" + pSearchParameter;
                varSearchString = WebUtility.HtmlEncode(varSearchString);
                var varResponse = varClient.GetAsync("contacts?" + varSearchString).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in ContactService->GetList", varMessageHandler); }
                //Handle Data Returned from Rest API
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

        public dynamic New(JObject pContact)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pContact.ToString(), Encoding.UTF8, "application/json");
                var varResponse = varClient.PostAsync("contacts/new", content).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in ContactService->New", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public dynamic Edit(string pId, JObject pContact)
        {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pContact.ToString(), Encoding.UTF8, "application/json");
                var varResponse = varClient.PutAsync("contacts/" + pId + "/edit", content).Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in ContactService->Edit", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
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
                var varResponse = varClient.DeleteAsync("contacts/" + pId + "/delete").Result;
                var varJson = varResponse.Content.ReadAsStringAsync().Result;
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) { throw new MauticApiException("Exception in ContactService->Delete", varMessageHandler); }
                //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

    }
}
