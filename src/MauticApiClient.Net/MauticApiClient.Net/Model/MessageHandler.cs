
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MauticApiClient.Net.Model
{
    public class MessageHandler
    {
        public MessageHandler(){
            Data = new List<Message>();
        }
        public bool RecoveryReturn(string pContent)
        {
            bool varResult = false;
            try{
                Data.Clear();
                JObject varErrorsJObject = JObject.Parse(pContent);
                foreach (var varErrorItem in varErrorsJObject.Root.SelectToken("errors").Children()){
                    Data.Add(JsonConvert.DeserializeObject<Message>(varErrorItem.ToString()));
                }
            } catch {
                varResult = false;
            } finally {
                if (Data.Count > 0) { varResult = true; }
                else { varResult =  false; }
            }
            return varResult;
        }
        public bool RecoveryReturn(HttpResponseMessage pResponse)
        {
            var varJson = pResponse.Content.ToString();
            return RecoveryReturn(varJson);
        }
        public List<Message> Data { get; set; }

        public bool HasStatus(string pStatusCode){
            return Data.Exists(r => r.code.Equals(pStatusCode));
        }
    }
}
