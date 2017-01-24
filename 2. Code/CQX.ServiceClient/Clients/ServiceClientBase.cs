using CQMFramework.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CQX.ServiceClient.Clients
{
    public abstract class ServiceClientBase
    {
        public static readonly HttpClient client = new HttpClient();
        static ServiceClientBase()
        {
            client.BaseAddress = new Uri(ConfigHelper.ServiceUrl);
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> GetAsync<T>(string path, params object[] pathArgs)
        {
            path = string.Format(path, pathArgs);
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return await Task.FromResult(default(T));
        }
        protected T Get<T>(string path, params object[] pathArgs)
        {
            path = string.Format(path, pathArgs);
            return Task.Run(async () =>
            {
                var response = await client.GetAsync(path).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
                }
                return await Task.FromResult(default(T));
            }).Result;
        }

        protected async Task<T> PostAsync<T>(object requestModel, string path, params object[] pathArgs)
        {
            path = string.Format(path, pathArgs);
            var response = await client.PostAsJsonAsync(path, requestModel).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
            }
            return await Task.FromResult(default(T));
        }
        protected T Post<T>(object requestModel, string path, params object[] pathArgs)
        {
            path = string.Format(path, pathArgs);
            return Task.Run(async () =>
            {
                var response = await client.PostAsJsonAsync(path, requestModel).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
                }
                return await Task.FromResult(default(T));
            }).Result;


        }
    }
}
