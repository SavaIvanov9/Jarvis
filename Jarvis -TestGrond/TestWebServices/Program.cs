using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace TestWebServices
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51396/")
            };

            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("Accept-Client: \"Fourth-Monitor\""));

            HttpResponseMessage response =
                client.GetAsync("api/clients/subscribe?date=2016-03-10&callback={URI}").Result;
            
            if (response.IsSuccessStatusCode)
            {
                //var products = response.Content
                //    .ReadAsync<IEnumerable<Port>>().Result;
                //foreach (var p in products)
                //{
                //    Console.WriteLine("{0,4} {1,-20} {2}",
                //        p.Id, p.Title, p.CreatedOn);
                //}

                var products = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(products);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

            //----------------------------------------------------------

            WebRequest();

            //----------------------------------------------------------

            //var resp = GetResponse("http://localhost:51396/api");
            //Console.WriteLine(resp);

            //--------------------------------------------------------

            //WebRequest req = WebRequest.Create(@"http://localhost:51396/api/api/clients/subscribe?date=2016-03-10&callback={URI}");

            //req.Method = "GET";

            //HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            //if (resp.StatusCode == HttpStatusCode.OK)
            //{
            //    using (Stream respStream = resp.GetResponseStream())
            //    {
            //        StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
            //        Console.WriteLine(reader.ReadToEnd());
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
            //}
            //Console.Read();

            //----------------------------------------------

            //CreateWebRequest("http://localhost:51396/api/api/clients/subscribe?date=2016-03-10&callback={URI}");

        }

        //private static HttpWebRequest CreateWebRequest(string endPoint)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(endPoint);

        //    request.Method = "GET";
        //    request.ContentLength = 0;
        //    request.ContentType = "text/json";
        //    //request.Headers["Accept-Client: \"Fourth-Monitor\""] = "2016-03-10";

        //    return request;
        //}

        //public static string GetResponse(string endPoint)
        //{
        //    HttpWebRequest request = CreateWebRequest(endPoint);

        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    {
        //        var responseValue = string.Empty;

        //        if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
        //            throw new ApplicationException(message);
        //        }

        //        // grab the response  
        //        using (var responseStream = response.GetResponseStream())
        //        {
        //            using (var reader = new StreamReader(responseStream))
        //            {
        //                responseValue = reader.ReadToEnd();
        //            }
        //        }

        //        return responseValue;
        //    }
        //}

        private static void WebRequest()
        {
            const string WEBSERVICE_URL = "http://localhost:51396/api/clients/subscribe?date=2016-03-10&callback={URI}";
            try
            {
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    //webRequest.Headers.Add("Authorization", "Basic bGF3c2912XBANzg5ITppc2ltCzEF");
                    webRequest.Headers.Add(@"Accept-Client", "Fourth-Monitor");
                    //webRequest.Headers.Add("Fourth-Monitor", "http://localhost:51396/api/clients/subscribe?date=2016-03-10&callback={URI}");

                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
