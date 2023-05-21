using Common.Web.Models;
using Microsoft.JSInterop;
using System;
using System.Text.Json;

namespace TypingSPA.Web.Services
{
    public class LocalStorageService : IAsyncDisposable
    {
        private Lazy<IJSObjectReference> JSAccessor = new();
        private readonly IJSRuntime JSRunTime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            JSRunTime = jsRuntime;
        }

        // for testing
        // because JSAccessor imports the module on when needed when testing our mocks will not get invoked.
        // to test call call the Invoke directly from the mock.
        //public LocalStorageService(IJSRuntime jsRuntime, IJSObjectReference jsAccessor)
        //{
        //    JSRunTime = jsRuntime;
        //    JSAccessor = new Lazy<IJSObjectReference>(jsAccessor);
        //}

        private async Task WaitForReference()
        {
            if (JSAccessor.IsValueCreated is false)
            {
                JSAccessor = new(await JSRunTime.InvokeAsync<IJSObjectReference>("import", "/JSModules/LocalStorageAccessor.js"));
            }
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            await WaitForReference();
            var result = await JSAccessor.Value.InvokeAsync<string>("get", key);
            return JsonSerializer.Deserialize<T>(result);
            //return result;
        }
        // issue with converting typed object, when storing just serialize the value was JSON
        public async Task SetValueAsync<T>(string key, T value)
        {
            await WaitForReference();
            var jsonValue = JsonSerializer.Serialize(value);
            await JSAccessor.Value.InvokeVoidAsync("set", key, jsonValue);
        }

        public async Task RemoveAsync(string key)
        {
            await WaitForReference();
            await JSAccessor.Value.InvokeVoidAsync("remove", key);
        }

        public async Task Clear()
        {
            await WaitForReference();
            await JSAccessor.Value.InvokeVoidAsync("clear");
        }

        public async ValueTask DisposeAsync()
        {
            if (JSAccessor.IsValueCreated)
            {
                await JSAccessor.Value.DisposeAsync();
            }
        }
    }
}
