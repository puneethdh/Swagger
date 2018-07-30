using Newtonsoft.Json;
using SwaggerTweets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Text.RegularExpressions;

namespace SwaggerTweets.Controllers
{
    public class TweetsController : Controller
    {
        // GET: Tweets
        string Baseurl = "https://badapi.iqvia.io//";
        //Declare Start Date and Endate in string format 
        public string startDate = "01-01-2016";
        public const string endDate = "01-01-2017";

        [HandleError]
        public async Task<ActionResult> Index( int? page)
        {
                       
            IEnumerable<Tweets> TweetsInfo = new List<Tweets>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                    //Sending request to find web api REST service resource Swaggeer using HttpClient  
                    
                    HttpResponseMessage Res = await client.GetAsync("api/v1/Tweets?startDate=" + startDate + "&endDate=" + endDate+"&skiptoken=Limit=TRUE");


                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var  TweetsResponse = Res.Content.ReadAsStringAsync().Result;
                        //var result = TweetsResponse.AsQueryable().Distinct().ToList().ToString();
                        //Deserializing the response recieved from web api and storing into the Tweets list  
                        TweetsInfo = JsonConvert.DeserializeObject<List<Tweets>>(TweetsResponse).Distinct();
                   
                    foreach (var a in TweetsInfo)
                    {

                        a.Link = Methodtrim(a.text);
                    }

                   
                }
                    //returning the Tweets list to view  
                    return View(TweetsInfo.ToPagedList(page ?? 1, 10));
                
                
            }
        }
        [HandleError]
        public string Methodtrim(string text)
        {

            try
            {
                if (text.Contains("https://"))
                {
                    int startindex = text.IndexOf("https://");
                    int legth = text.Length;
                    string result1 = text.Substring(startindex, legth - startindex);

                    return result1;
                }
                else
                {
                    return "";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       

      
    }
}
