using Bunit;
using Common.Web.Constants;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TypingSPA.Tests.Web
{
    public class HomePage
    {

        

        [Fact]
        public void ShouldLoadPage()
        {
            using var ctx = new TestContext();
            var comp = ctx.RenderComponent<TypingSPA.Web.Pages.Index>();
            var headingElement = comp.Find($"#{WebConstants.ComponentIDs.LandingPage}");
            
            var typingMessage = headingElement.TextContent;
            Assert.Equal(typingMessage, WebConstants.LandingPageWelcome);

        }

        [Fact]
        public void ShouldLoadGlobalNav()
        {
            using var ctx = new TestContext();
            var comp = ctx.RenderComponent<TypingSPA.Web.Shared.MainLayout>();
            var headingElement = comp.Find($"#{WebConstants.ComponentIDs.GlobalNav}");

            var exists = headingElement != null;
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
