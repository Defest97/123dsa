using DemoClients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace WebClientDemo
{
    internal class Program
    {
        static Uri _host = new Uri("http://localhost:5095");
        static IEnumerable<Organization> GetOrganizations(int skip=0, int take = 50)
        {
            var url = new Uri(_host, $"api/organizations?take={take}&skip={skip}");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("accept", "*/*");
            try
            {
                string s = webClient.DownloadString(url);
                var result = JsonConvert.DeserializeObject<QueryResult<Organization>>(s);
                return result.Items;
            }catch (Exception ex) {
                throw new Exception();
            }
                
            
        }

        static Organization PostOrganization(Organization organization)
        {
            var url = new Uri(_host, $"api/organizations");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("accept", "*/*");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json"); 
            try
            {
                string data = JsonConvert.SerializeObject(organization);
                string s = webClient.UploadString(url,data);
                var result = JsonConvert.DeserializeObject<Organization>(s);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }

        static void DeleteOrganization(long id)
        {
            var url = new Uri(_host, $"api/organizations/{id}");
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            request.Accept = "*/*";
            try
            {
                var responce = request.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error delete object from service : {ex.ToString()}");
            }

        }



        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            var r = GetOrganizations();
            var postResult = PostOrganization(new Organization { Name = "WebClient Org", FullName = "WebClient Organization demo" });
            DeleteOrganization(postResult.Id);
            
            Console.WriteLine(r.Count());
        }
    }
}
