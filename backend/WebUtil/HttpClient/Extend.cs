﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Protocols;
using Web.Protocols.Exception;

namespace WebUtil.HttpClient
{
    public static class Extend
    {
        public static void SetDefaultHeader(this System.Net.Http.HttpClient client, HttpRequestMessage request, string userId)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add(Constants.HeaderKeys.InternalServer, System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
            if (userId != null)
            {
                request.Headers.Add(WebUtil.Constants.HeaderKeys.AuthorizedUserId, userId);
            }
        }

        private static async Task CheckError(this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var responseHeader = JsonConvert.DeserializeObject<ResponseHeader>(await httpResponseMessage.Content.ReadAsStringAsync());
                if (responseHeader != null)
                {
                    throw new LogicException(responseHeader.ResultCode);
                }
                else
                {
                    throw new LogicException(Web.Code.ResultCode.HttpError, httpResponseMessage.StatusCode);
                }
            }
        }

        public static async Task<T> Request<T>(this IHttpClientFactory httpClientFactory,
            HttpMethod httpMethod,
            string url,
            string userId = null,
            Action<HttpRequestMessage> preAction = null) where T : ResponseHeader
        {
            using var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(httpMethod, url);
            client.SetDefaultHeader(request, userId);
            preAction?.Invoke(request);
            var response = await client.SendAsync(request);
            await CheckError(response);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<T> RequestJson<T>(this IHttpClientFactory httpClientFactory,
                HttpMethod httpMethod,
                string url,
                string body,
                string userId = null) where T : ResponseHeader
        {
            using var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(httpMethod, url);
            client.SetDefaultHeader(request, userId);
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            await CheckError(response);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }


        public static async Task<T> RequestJson<T>(this IHttpClientFactory httpClientFactory,
            HttpMethod httpMethod,
            string url,
            object body,
            string userId = null) where T : ResponseHeader
        {
            return await httpClientFactory.RequestJson<T>(httpMethod, url, JsonConvert.SerializeObject(body), userId);
        }

    }
}