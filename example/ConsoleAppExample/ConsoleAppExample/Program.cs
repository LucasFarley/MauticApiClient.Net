using MauticApiClient.Net;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleAppExample
{
    class Program
    {
        static void Main(string[] args)
        {
    
            var username = "mautic_userid";
            var password = "mautic_password";
            var url = "https://XXXXXX/api/";
            var httpProvider = new HttpClientProvider(url, username, password);
            //Teste Mautic Api Services 
            try {
                ////Perform Category Object Handle - Examples
                //var varCategoryExample = new clsCategoryExample();
                //varCategoryExample.funcExecute(httpProvider);
                //Perform Contact Object Handle - Examples
                var varContactExample = new clsContactExample();
                varContactExample.funcExecute(httpProvider);
            } catch (AggregateException varExceptionList) {
                foreach(dynamic varException in varExceptionList.InnerExceptions){
                    Console.WriteLine("Error {0}", varException.Message);
                    if (varException.GetType() == typeof(MauticApiException)){
                        var varMauticApiException = (MauticApiException)varException;
                        foreach (var varErrorItem in varMauticApiException.GetErrors()) { 
                            Console.WriteLine("Error {0} : {1} : {2}", varErrorItem.code, varErrorItem.type, varErrorItem.message);
                        }
                    }
                }
            }
            //Wait the next action
            Console.Read();
        }
    }
}
