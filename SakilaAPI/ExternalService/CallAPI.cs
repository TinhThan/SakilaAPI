using System.Text;
using System;
using SakilaAPI.Core.Middlewares;
using Newtonsoft.Json;

namespace SakilaAPI.ExternalService
{
    public interface ICallAPI
    {
        string BuildParram(int[] ids);
        string GetFullLink(string api, string slug, string[] valueOfFormat);
        void SetHeaders(Dictionary<string, string> headers);
        void ClearHeaders();
        void SetContentType(string pContentType);
        Task<HttpResponseMessage> CallAPIGet(string url);
        Task<TModel> Result<TModel>(HttpResponseMessage resultRequest, TModel listDefault);
    }

    public class CallAPI : ICallAPI
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CallAPI> _logger;

        public CallAPI(HttpClient httpClient, IConfiguration configuration, ILogger<CallAPI> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public void SetHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
        public void ClearHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Clear();
        }
        public void SetContentType(string pContentType)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(pContentType));
        }

        public string BuildParram(int[] ids)
        {
            if (ids == null) return string.Empty;
            StringBuilder queryParm = new();
            foreach (var item in ids)
            {
                if (item < 0)
                {
                    continue;
                }
                if (queryParm.Length > 0)
                    queryParm.Append('&');
                queryParm.Append($"{nameof(ids)}={item}");
            }
            return queryParm.ToString();
        }

        public string GetFullLink(string api, string slug, string[] valueOfFormat)
        {
            if (CurretnOption.Endpoints.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return string.Format(CurretnOption.Endpoints[api] + slug, valueOfFormat);
            }
        }

        public async Task<HttpResponseMessage> CallAPIGet(string url)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                responseMessage = await _httpClient.GetAsync(url);
            }
            catch (Exception ex)
            {
                responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                responseMessage.Content = new StringContent(ex.ToString(), Encoding.UTF8, "application/json");
            }

            return responseMessage;
        }

        public async Task<TModel> Result<TModel>(HttpResponseMessage resultRequest, TModel listDefault)
        {
            if (resultRequest.IsSuccessStatusCode)
            {
                try
                {
                    string apiResponse = await resultRequest.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        return JsonConvert.DeserializeObject<TModel>(apiResponse);
                    }
                    else
                    {
                        _logger.LogWarning("Response Data Empty!");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("message", ex.Message);
                }
                return listDefault;
            }
            else
            {
                var content = await resultRequest.Content.ReadAsStringAsync();
                _logger.LogError("{content}", content);
                return listDefault;
            }
        }
    }
}
