using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthenticationClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = new Task(HTTP_GET);
            t.Start();
            Console.ReadLine();
        }

        static async void HTTP_GET()
        {
            var TARGETURL = "webServiceUrl";
            HttpClientHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
                UseProxy = false,
            };
            Console.WriteLine("GET: + " + TARGETURL);
            HttpClient client = new HttpClient(handler);
            var byteArray = Encoding.ASCII.GetBytes("username:password");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpResponseMessage response = await client.GetAsync(TARGETURL);
            HttpContent content = response.Content;
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);
            string result = await content.ReadAsStringAsync();
            if (result != null &&
            result.Length >= 50)
            {
                Console.WriteLine(result.Substring(0, 50) + "...");
            }
        }
    }
}