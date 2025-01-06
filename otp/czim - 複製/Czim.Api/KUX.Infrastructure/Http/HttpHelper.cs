using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Infrastructure.Http;

/// <summary>
/// 静态Http请求
/// </summary>
public static class HttpHelper
{
    /// <summary>
    /// 发送http请求消息
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="httpMethod"></param>
    /// <param name="url"></param>
    /// <param name="httpContent">请求内容（json字符串）</param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static async Task<string> HttpSendAsync(this IHttpClientFactory httpClientFactory, HttpMethod httpMethod, string url, string httpContent,
        Dictionary<string, string> headers = null)
    {
        var _httpClient = httpClientFactory.CreateClient();
        if (headers?.Count > 0)
        {
            foreach (var head in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(head.Key, head.Value);
            }
        }
        using (var request = new HttpRequestMessage())
        {
            request.Method = httpMethod;
            request.RequestUri = new Uri(url);
            request.Content = new StringContent(httpContent, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                using (var httpResponseMessage = await _httpClient.SendAsync(request))
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await httpResponseMessage.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }

    /// <summary>
    /// 发送http请求消息
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="httpMethod"></param>
    /// <param name="url"></param>
    /// <param name="httpContent">请求内容（json字符串）</param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static async Task<(int, string)> HttpSendStatusAsync(this IHttpClientFactory httpClientFactory, HttpMethod httpMethod, string url, string httpContent,
        Dictionary<string, string> headers = null)
    {
        var _httpClient = httpClientFactory.CreateClient();
        if (headers?.Count > 0)
        {
            foreach (var head in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(head.Key, head.Value);
            }
        }
        using (var request = new HttpRequestMessage())
        {
            request.Method = httpMethod;
            request.RequestUri = new Uri(url);
            request.Content = new StringContent(httpContent, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            int _httpStatus = 0;
            string result = string.Empty;
            try
            {
                using (var httpResponseMessage = await _httpClient.SendAsync(request))
                {
                    _httpStatus = (int)httpResponseMessage.StatusCode;
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = await httpResponseMessage.Content.ReadAsStringAsync();
                        // return (200,result);
                    }
                }
            }
            catch (Exception ex)
            { 
                _httpStatus = 406;
                result = "";
            }
            return (_httpStatus, result);
        }
    }
}