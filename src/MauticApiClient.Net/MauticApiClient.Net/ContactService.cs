using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MauticApiClient.Net.Model;
using Newtonsoft.Json.Linq;

namespace MauticApiClient.Net {
    public class ContactService {
        private readonly IHttpClientProvider _httpClientProvider;

        public ContactService(IHttpClientProvider httpClientProvider){
            _httpClientProvider = httpClientProvider;
        }

        public async Task<dynamic> GetByIdAsync( string pId ) {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try {
                var varResponse = await varClient.GetAsync("contacts/" + pId);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.HasStatus("404")) { return null; } // Record Not Found
                if (varMessageHandler.Data.Count > 0) {
                    var varMessage = "Exception in ContactService->GetByIdAsync : " + varJson;
                    throw new MauticApiException(varMessage, varMessageHandler);
                } //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> GetListAsync(string pSearchParameter) {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varSearchString = "search=" + pSearchParameter;
                varSearchString = WebUtility.HtmlEncode(varSearchString);
                var varResponse = await varClient.GetAsync("contacts?" + varSearchString);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) {
                    var varMessage = "Exception in ContactService->GetListAsync : " + varJson;
                    throw new MauticApiException(varMessage, varMessageHandler);
                } //Handle Data Returned from Rest API
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

        public async Task<dynamic> NewAsync(JObject pContact) {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pContact.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PostAsync("contacts/new", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) {
                    var varMessage = "Exception in ContactService->NewAsync : " + varJson;
                    throw new MauticApiException(varMessage, varMessageHandler);
                } //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> EditAsync(string pId, JObject pContact)  {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var content = new StringContent(pContact.ToString(), Encoding.UTF8, "application/json");
                var varResponse = await varClient.PutAsync("contacts/" + pId + "/edit", content);
                var varJson = await varResponse.Content.ReadAsStringAsync();
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) {
                    var varMessage = "Exception in ContactService->EditAsync : " + varJson;
                    throw new MauticApiException(varMessage, varMessageHandler);
                } //Handle Data Returned from Rest API
                JObject contactsJObject = JObject.Parse(varJson);
                dynamic contactItem = contactsJObject["contact"];
                return contactItem;
            } catch {
                throw;
            } finally {
                if (varClient != null) varClient.Dispose();
            }
        }

        public async Task<dynamic> DeleteAsync(string pId) {
            var varClient = _httpClientProvider.GetHttpClient();
            var varMessageHandler = new MessageHandler();
            try{
                var varResponse = await varClient.DeleteAsync("contacts/" + pId + "/delete");
                var varJson = await varResponse.Content.ReadAsStringAsync();
                //Handle Responde Message List
                varMessageHandler.RecoveryReturn(varJson);
                if (varMessageHandler.Data.Count > 0) {
                    var varMessage = "Exception in ContactService->DeleteAsync : " + varJson;
                    throw new MauticApiException(varMessage, varMessageHandler);
                } //Handle Data Returned from Rest API
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
