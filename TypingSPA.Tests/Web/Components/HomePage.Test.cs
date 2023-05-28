using Bunit;
using TypingSPA.Web.Constants;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TypingSPA.Web.Services;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TypingSPA.Tests.Web.Components
{
    public class HomePageTests
    {
        public TestContext ctx { get; set; }
        public BunitJSInterop jsInterop { get; set; }

        public HomePageTests() {
            ctx = new TestContext();
            ctx.Services.AddMudServices();
            jsInterop = ctx.JSInterop.SetupModule("/JSModules/LocalStorageAccessor.js");
            ctx.JSInterop.SetupVoid("mudPopover.connect", _ => true);
            var mockLocalStorageService = new Mock<LocalStorageService>(jsInterop.JSRuntime);
            var mockThemeService = new Mock<ThemeService>(mockLocalStorageService.Object);
            ctx.Services.AddSingleton(mockLocalStorageService.Object);
            ctx.Services.AddSingleton(mockThemeService.Object);
        }

        [Fact]
        public void ShouldLoadPage()
        {
            var comp = ctx.RenderComponent<TypingSPA.Web.Pages.Index>();
            var headingElement = comp.Find($"#{WebConstants.ComponentIDs.LandingPage}");
            var typingMessage = headingElement.TextContent;
            Assert.Equal(typingMessage, WebConstants.LandingPageWelcome);

        }

        [Fact]
        public void ShouldLoadMainLayout()
        {
            
            var comp = ctx.RenderComponent<TypingSPA.Web.Shared.MainLayout>();
            var headingElement = comp.Find($"#{WebConstants.ComponentIDs.GlobalNav}");
            var mainElement = comp.Find($"#{WebConstants.ComponentIDs.MainComponent}");
            var footerElement = comp.Find($"#{WebConstants.ComponentIDs.GlobalFooter}");
            var exists = headingElement != null && mainElement != null && footerElement != null;
            Assert.True(exists);
        }

        /// <summary>
        /// sample inject test code
        /// </summary>
        public void teststuff()
        {
            var mockDialogService = new Mock<IDialogService>();
            var mockSnackBarService = new Mock<ISnackbar>();
            var mockMudPopoverProviderService = new Mock<IMudPopoverService>();
            using var ctx = new TestContext();
            ctx.Services.AddSingleton(mockDialogService.Object);
            ctx.Services.AddSingleton(mockSnackBarService.Object);
            ctx.Services.AddSingleton(mockMudPopoverProviderService.Object);
        }
    }
}
