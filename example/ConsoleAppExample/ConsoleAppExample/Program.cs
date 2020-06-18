using MauticApiClient.Net;
using System;
using System.Net;

namespace ConsoleAppExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var varUrl = "https://m.solidit.net/api/";
            var varUsername = "monitor_service";
            var varPassword = "#12398&^$";
            WebProxy varWebProxy = null;
            //Define Mautic Connection Parameters
            var httpProvider = new HttpClientProvider(varUrl, varUsername, varPassword, varWebProxy);
            //Teste Mautic Api Services 
            try {
                //Perform Category Object Handle - Examples
                //var varCategoryExample = new clsCategoryExample();
                //varCategoryExample.funcExecute(httpProvider);
                //Perform Contact Object Handle -Examples
                var varContactExample = new clsContactExample();
                varContactExample.funcExecute(httpProvider);
            } catch (AggregateException varExceptionList) {
                foreach(dynamic varException in varExceptionList.InnerExceptions){
                    funcWriteExceptions(varException);
                    if (varException.GetType() == typeof(MauticApiException)){
                        var varMauticApiException = (MauticApiException)varException;
                        foreach (var varErrorItem in varMauticApiException.GetMessages()) { 
                            Console.WriteLine("Error {0} : {1} : {2}", varErrorItem.code, varErrorItem.type, varErrorItem.message);
                        }
                    }
                }
            }
            //Wait the next action
            Console.Read();
        }
        private static void funcWriteExceptions(Exception _prtException){
            if (_prtException == null) { return; }
            Console.WriteLine("Error {0} - {1}", _prtException.GetType().ToString(), _prtException.Message);
            funcWriteExceptions(_prtException.InnerException);
        }
    }
}
