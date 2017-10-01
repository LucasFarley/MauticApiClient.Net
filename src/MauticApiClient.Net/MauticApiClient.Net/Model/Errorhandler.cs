
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MauticApiClient.Net.Model
{
    public class Errorhandler
    {
        public Errorhandler(){
            Data = new List<Error>();
        }
        public bool TryToParse(string pContent)
        {
            bool varResult = false;
            try{
                Data.Clear();
                JObject varErrorsJObject = JObject.Parse(pContent);
                foreach (var varErrorItem in varErrorsJObject.Root.SelectToken("errors").Children()){
                    Data.Add(JsonConvert.DeserializeObject<Error>(varErrorItem.ToString()));
                }
            } catch {
                varResult = false;
            } finally {
                if (Data.Count > 0) { varResult = true; }
                else { varResult =  false; }
            }
            return varResult;
        }
        public bool TryToParse(HttpResponseMessage pResponse)
        {
            var varJson = pResponse.Content.ToString();
            return TryToParse(varJson);
        }
        public List<Error> Data { get; set; }
    }
}
