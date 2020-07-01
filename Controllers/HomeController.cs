using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using APIConsumption.Models;
//using APIConsumption.APIHandlerManager;
using Newtonsoft.Json;
using System.Net.Http;



namespace APIConsumption.Controllers
{
    public class HomeController : Controller
    {

        // Here Httpclient is Property of controller, not action method.
        // since it can be used by multiple action methods. 
        HttpClient httpClient;
        static string URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "FepPmvoKkbosZSNCgSAUJmt8kltghv7apD6Ua5gk";

        public IActionResult Index()
        {
            // Defining httpclient object & Initializing. 
            httpClient = new HttpClient();

            // Additonal parameter client sends will be cleared
            httpClient.DefaultRequestHeaders.Accept.Clear();

            // Defining API Key Pair. 
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);


            // To tell Server we can handle json. 
            httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            // Creating URL of the Park where Object need to connect. 
            // Creating Base Address. 

            string National_Park_API_Path = URL + "/parks?limit=20";
            string parksdata = "";
            Park parks = null; // Creating object Parks (From Model).  

           

            httpClient.BaseAddress = new Uri(National_Park_API_Path);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(National_Park_API_Path).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    // Asynchronous for wait till get result, common for remote operation                    
                    parksdata = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!parksdata.Equals(""))
                {
                    // Deserialize parksdata string into the Parks Object. 
                    parks = JsonConvert.DeserializeObject<Park>(parksdata);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(parks);


        }

        public IActionResult Parks()
        {
            return View();
        }
    }
}