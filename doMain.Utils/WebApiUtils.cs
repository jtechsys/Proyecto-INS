using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace doMain.Utils
{
    public class WebApiUtils
    {

        public static async Task<string> GetAsync(string baseUrl, string controller, string action, string pars)
        {

            string apiUrl = baseUrl + "/api/" + controller + "/" + action + "?" + pars;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return data;

                    }


                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }

        public static string Get(string baseUrl, string controller, string action, string pars)
        {
            return AsyncUtils.RunSync(new Func<Task<string>>(async () => await GetAsync(baseUrl, controller, action, pars)));
        }





        public static void Post<T>(string baseUrl, string controller, string action, T objectParameter)
        {

            string apiUrl = baseUrl + "/api/" + controller + "/" + action ;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsJsonAsync(apiUrl, objectParameter).Result;
            var result = response;

        }
    }
}
