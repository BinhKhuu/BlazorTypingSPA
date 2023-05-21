using Bunit;
using TypingSPA.Web.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TypingSPA.Web.Services;
using static TypingSPA.Web.Constants.WebConstants;

namespace TypingSPA.Tests.Web.Services
{
    public class LocalStorageServiceTests
    {
        public TestContext ctx { get; set; }
        public BunitJSInterop jsInterop { get; set; }

        public LocalStorageServiceTests()
        {
            ctx = new TestContext();
            jsInterop = ctx.JSInterop.SetupModule("/JSModules/LocalStorageAccessor.js");
        }

        [Fact]
        public async Task JSLocallStorageSetCalled()
        {
            // Arrange
            
            var settings = new ThemeSettings();
            var module = jsInterop;
            
            var plannedInvocation = module.SetupVoid("set", _ => true);

            //var mockJSRunTime = ctx.JSInterop.JSRuntime;
            // test code when using IJSObjectReference
            //var test = await mockJSRunTime.InvokeAsync<IJSObjectReference>("import", "/JSModules/LocalStorageAccessor.js");
            //var localStorageService = new LocalStorageService(mockJSRunTime, (IJSObjectReference)plannedInvocation);
            //await localStorageService.SetValueAsync(LocalStorageSettingConstants.ThemeSettingName, settings);
            // Act
            //var localStorageService = new LocalStorageService(module.JSRuntime);
            // don't wait will hang https://bunit.dev/docs/test-doubles/emulating-ijsruntime#setting-up-invocations

            module.JSRuntime.InvokeVoidAsync("set", LocalStorageSettingConstants.ThemeSettingName, "test");
            
            plannedInvocation.SetVoidResult();
            // Assert
            var invoked = plannedInvocation.Invocations.Count > 0;
            Assert.True(invoked);
        }

        [Fact]
        public async Task JSLocalStorageGetCalled()
        {
            var settings = new ThemeSettings();
            var plannedInvocation = jsInterop.Setup<string>("get", _ => true);
            jsInterop.JSRuntime.InvokeAsync<string>("get", LocalStorageSettingConstants.ThemeSettingName);
            var invoked = plannedInvocation.VerifyInvoke("get"); // throws error if not invoked
            Assert.True(invoked != null);
        }

        [Fact]
        public async Task JSLocalStorageRemoveCalled()
        {
            var settings = new ThemeSettings();
            var plannedInvocation = jsInterop.SetupVoid("remove", _ => true);
            jsInterop.JSRuntime.InvokeVoidAsync("remove", LocalStorageSettingConstants.ThemeSettingName);
            var invoked = plannedInvocation.VerifyInvoke("remove"); // throws error if not invoked
            Assert.True(invoked != null);
        }

        [Fact]
        public async Task JSLocalStorageClearCalled()
        {
            var settings = new ThemeSettings();
            var plannedInvocation = jsInterop.SetupVoid("clear", _ => true);
            jsInterop.JSRuntime.InvokeVoidAsync("clear", LocalStorageSettingConstants.ThemeSettingName);
            var invoked = plannedInvocation.VerifyInvoke("clear"); // throws error if not invoked
            Assert.True(invoked != null);
        }
    }
}
