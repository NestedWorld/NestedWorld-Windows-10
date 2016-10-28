using NestedWorldHttp.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace NestedWorldHttp
{
    public enum RequestType
    {
        GET,
        POST,
        DELETE,
        PUT,
    }

    public class HttpResult
    {
        public string content;
        public System.Net.HttpStatusCode code;
        public JObject Object { get { return JObject.Parse(content); } set { } }
        public JArray Array { get { return JArray.Parse(content); } set { } }


        static public HttpResult NewResult(string content, System.Net.HttpStatusCode code)
        {
            return new HttpResult()
            {
                content = content,
                code = code
            };
        }

    }
    public class HttpRequest
    {
        //private string BaseAddress = "https://api.nestedworld.com/v1";
        private string BaseAddress = "https://api-dev.nestedworld.com/v1";
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

        public async Task<HttpResult> GetJsonAsync()
        {
            string Token = Utils.Composite.Instance("session")["token"] as string;

            Log.Info(type.ToString(), uri);
            System.Net.Http.HttpResponseMessage response = null;
            using (var client = new System.Net.Http.HttpClient())
            {
                string jsonString = "";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                if (type == RequestType.GET)
                {
                    response = await client.GetAsync(uri);
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                else if (type == RequestType.POST)
                {
                    CreateJsonContent();
                    response =
                        await client.PostAsync(uri, new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                else if (type == RequestType.DELETE)
                {
                    response = await client.DeleteAsync(uri);
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                else if (type == RequestType.PUT)
                {
                    CreateJsonContent();
                    response =
                        await client.PutAsync(uri, new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
                    Log.Info("Code", response.StatusCode);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                Log.Info("jsonstring", jsonString);
                return HttpResult.NewResult(jsonString, response.StatusCode);
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
