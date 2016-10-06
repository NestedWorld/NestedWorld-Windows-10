using NestedWorld.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace NestedWorld.Classes.Request
{
    public enum RequestType
    {
        GET,
        POST,
        DELETE,
        PUT,
    }
    public class HttpRequest
    {
        private string BaseAddress = "http://eip-api-dev.kokakiwi.net/v1";
        protected Uri uri;
        protected string url;
        protected HttpWebRequest request;
        protected HttpStringContent content;
        private JObject obj;
        protected Dictionary<string, string> collection;
        private RequestType type;

        public HttpRequest(string url, RequestType type)
        {
            this.url = BaseAddress + url;
            this.type = type;
            collection = new Dictionary<string, string>();

        }

        public async Task<JObject> GetJsonAsync()
        {
            Log.Info(type.ToString(), uri);
            using (var client = new System.Net.Http.HttpClient())
            {
                string jsonString = "";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.network.Token);
                if (type == RequestType.GET)
                {
                    jsonString = await client.GetStringAsync(uri);
                }
                else if (type == RequestType.POST)
                {
                    CreateJsonContent();
                    System.Net.Http.HttpResponseMessage response =
                        await client.PostAsync(uri, new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                else if (type == RequestType.DELETE)
                {
                    System.Net.Http.HttpResponseMessage response = await client.DeleteAsync(uri);
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                else if (type == RequestType.PUT)
                {
                    CreateJsonContent();
                    System.Net.Http.HttpResponseMessage response =
                       await client.PutAsync(uri, new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                Log.Info("jsonstring", jsonString);
                return JObject.Parse(jsonString);
            }
        }

        protected void CreateJsonContent()
        {
            obj = new JObject();
            if (collection.Count == 0)
                return;
            foreach (KeyValuePair<string, string> param in collection)
            {
                obj.Add(param.Key, param.Value);
            }
            Log.Info("JObject", obj);
        }

        internal static string GetURLParam(Dictionary<string, string> Params)
        {
            string ret = "?";
            foreach (KeyValuePair<string, string> param in Params)
            {
                ret += param.Key + "=" + param.Value + "&";
            }
            ret.Remove(ret.Length - 1);
            return ret;
        }
    }
}
