using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AmigoPaperWorkProcessSystem.Core
{
    public class WebUtility
    {
        #region AuthHeader
        private static HttpClient makeAuthHeader()
        {
            // Encode credentials
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(Utility.Id + ":" + Utility.Password);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            return client;
        }
        #endregion

        #region GetMethod
        public static DataTable Get(string url, out Meta MetaData)
        {
            HttpClient client = makeAuthHeader();
            var response = client.GetAsync(url);

            //call methods
            string content = response.Result.Content.ReadAsStringAsync().Result;

            dynamic result = JsonConvert.DeserializeObject(content);

            //log error message
            if (result.Status == 0)
            {
                Utility.WriteErrorLog(result.Message.ToString(), null, true);
            }
            MetaData = JsonConvert.DeserializeObject<Meta>(JsonConvert.SerializeObject(result.Meta));
            string data = result.Data;
            DataTable dt = Utility.JsonToDt(data);
            return dt;
        }

        public static DataTable Get(string url)
        {
            HttpClient client = makeAuthHeader();
            var response = client.GetAsync(url);

            //call methods
            string content = response.Result.Content.ReadAsStringAsync().Result;

            dynamic result = JsonConvert.DeserializeObject(content);

            //log error message
            if (result.Status == 0)
            {
                Utility.WriteErrorLog(result.Message.ToString(), null,true);
            }

            string data = result.Data;
            DataTable dt = Utility.JsonToDt(data);
            return dt;
        }
        #endregion

        #region PostMethod
        public static DataTable Post(string url, DataTable list, out Meta MetaData)
        {
            //convert list to json object
            String json = JsonConvert.SerializeObject(new { List = list });

            //prepare to Post
            json = JsonConvert.SerializeObject(new { List = json });

            //encode content
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = makeAuthHeader();

            var response = client.PostAsync(url, data);

            string content = response.Result.Content.ReadAsStringAsync().Result;

            dynamic result = JsonConvert.DeserializeObject(content);

            //log error message
            if (result.Status == 0)
            {
                Utility.WriteErrorLog(result.Message.ToString(),null, true);
            }

            //prepare return data
            MetaData = JsonConvert.DeserializeObject<Meta>(JsonConvert.SerializeObject(result.Meta));
            string returnData = result.Data;
            DataTable dt = Utility.JsonToDt(returnData);

            return dt;
        }


        public static DataTable Post(string url, DataTable list)
        {
            //convert list to json object
            String json = JsonConvert.SerializeObject(new { List = list });

            //prepare to Post
            json = JsonConvert.SerializeObject(new { List = json });

            //encode content
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = makeAuthHeader();

            var response = client.PostAsync(url, data);

            string content = response.Result.Content.ReadAsStringAsync().Result;

            dynamic result = JsonConvert.DeserializeObject(content);

            //log error message
            if (result.Status == 0)
            {
                Utility.WriteErrorLog(result.Message.ToString(),null, true);
            }

            //prepare return data
            string returnData = result.Data;
            DataTable dt = Utility.JsonToDt(returnData);

            return dt;
        }

        #endregion
    }
}
