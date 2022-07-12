using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpClient ApiClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync(requestUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string requestUri, Dictionary<string, string> headers = null)
        {
            string result = "";
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    foreach (var item in headers)
                    {
                        if (item.Key.ToLower() == "authorization")
                        {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", item.Value);
                        }
                        else
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }

                    result = await httpClient.GetStringAsync(requestUri);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <param name="skipCertValidationCheck"></param>
        /// <returns></returns>
        public async Task<string> PostAsync<T>(string requestUri, T request, Dictionary<string, string> headers = null, bool skipCertValidationCheck = false) where T : class
        {
            try
            {
                string result = "";
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    if (skipCertValidationCheck)
                    {
                        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    }

                    using (HttpClient httpClient = new HttpClient(httpClientHandler))
                    {
                        //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        if (headers != null)
                        {
                            foreach (var item in headers)
                            {
                                if (item.Key.ToLower() == "authorization")
                                {
                                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", item.Value);
                                }
                                else
                                {
                                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                                }
                            }
                        }

                        result = await httpClient.PostAsync(requestUri, new StringContent(request.ToJsonString())
                        {
                            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                        }).Result.Content.ReadAsStringAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"PostAsync Exception, {ex.ToJsonString()}");
            }
        }

        private void AddHeaders(Dictionary<string, string> headers)
        {
            ApiClient.DefaultRequestHeaders.Add("Accept", "application/json");

            if (headers == null)
                return;

            foreach (var item in headers)
            {
                if (item.Key.ToLower() == "authorization")
                {
                    ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", item.Value);
                }
                else
                {
                    ApiClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
        }
    }
}
