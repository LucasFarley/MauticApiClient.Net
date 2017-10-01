using System;
using System.Net;
using System.Text.RegularExpressions;
using MauticApiClient.Net;
using Newtonsoft.Json.Linq;

namespace ConsoleAppExample
{
    public class clsContactExample
    {
        public void funcExecute(HttpClientProvider pProvider)
        {
            //Create Object of ContactService
            var contactService = new ContactService(pProvider);
            string varContactID, varContactFirstName, varContactEmail, varSearchParameter;

            //Get Contact List
            Console.WriteLine("----------------- Contact List -----------------");
            var contactSearch = new JObject();
            Console.Write("Search Parameter : "); varSearchParameter = Console.ReadLine();
            if (!string.IsNullOrEmpty(varSearchParameter)){
                var contactsList = contactService.GetList(varSearchParameter).Result;
                foreach (var contact in contactsList){
                    foreach (var varField in contact.First["fields"]["all"]){
                        Console.WriteLine("Field {0} = {1}", varField.Name, varField.Value);
                    }
                }
            }

            //Get Contact ID
            Console.WriteLine("---------------- Get Contact ------------------");
            Console.Write("Contact ID : "); varContactID = Console.ReadLine();
            if (!string.IsNullOrEmpty(varContactID)){
                var contactItem = contactService.GetById(varContactID).Result;
                foreach (var varField in contactItem["fields"]["all"]){
                    Console.WriteLine("Field {0} = {1}", varField.Name, varField.Value);
                }
            }

            //Edit Contact
            Console.WriteLine("----------------- Edit Contact -----------------");
            //Get the Contact to be Edited
            Console.Write("Contact to be edited : "); varContactID = Console.ReadLine();
            if (!string.IsNullOrEmpty(varContactID)){
                var contactSelected = contactService.GetById(varContactID).Result;
                var varContatFields = (JObject)contactSelected["fields"]["all"];
                if (varContatFields.Count > 0) {
                    //Edit the Contact Selected
                    Console.Write("Contact FirstName : "); varContactFirstName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(varContactFirstName)) {
                        varContatFields.Remove("id");
                        varContatFields["firstname"] = varContactFirstName;
                        var contactItem = contactService.Edit(varContactID, varContatFields).Result;
                        foreach (var varField in contactItem["fields"]["all"]){
                            Console.WriteLine("Field {0} = {1}", varField.Name, varField.Value);
                        }
                    }
                }
            }

            //Add Contact
            Console.WriteLine("----------------- New Contact -----------------");
            var contactNew = new JObject();
            Console.Write("Contact FirstName : "); varContactFirstName = Console.ReadLine();
            contactNew.Add("firstname", varContactFirstName);
            Console.Write("Contact E-mail : "); varContactEmail = Console.ReadLine();
            contactNew.Add("email", varContactEmail);
            contactNew.Add("ipAddress", funcGetExternalIp());
            if (!string.IsNullOrEmpty(varContactFirstName) && !string.IsNullOrEmpty(varContactEmail)){
                var contactItem = contactService.New(contactNew).Result;
                foreach (var varField in contactItem["fields"]["all"]){
                    Console.WriteLine("Field {0} = {1}", varField.Name, varField.Value);
                }
            }

            //Delete Contact
            Console.WriteLine("---------------- Delete Contact ----------------");
            Console.Write("Contact ID : "); varContactID = Console.ReadLine();
            if (!string.IsNullOrEmpty(varContactID)){
                var varResultDelete = contactService.Delete(varContactID);
                Console.WriteLine("Contact Deleted {0}", varContactID);
            }
        }
        private string funcGetExternalIp(IWebProxy pWebProxy = null)
        {
            try{
                string varExternalIP = string.Empty;
                var varWebClient = new System.Net.WebClient();
                varWebClient.Proxy = pWebProxy;
                varExternalIP = varWebClient.DownloadString("http://checkip.dyndns.org/");
                var varRegularExp = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                varExternalIP = varRegularExp.Matches(varExternalIP)[0].ToString();
                return varExternalIP;
            } catch {
                return "";
            }
        }
    }
}
