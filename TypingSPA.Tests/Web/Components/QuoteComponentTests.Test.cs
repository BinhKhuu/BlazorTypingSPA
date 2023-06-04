using Bunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingSPA.Web.Services;
using TypingSPA.Web.Components;
using TypingSPA.Web.Constants;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor.Services;

namespace TypingSPA.Tests.Web.Components
{
    public class QuoteComponentTests
    {
        private TestContext ctx { get; set; }
        private BunitJSInterop jsInterop { get; set; }
        private string quoteText = "test";
        public QuoteComponentTests()
        {
            ctx = new TestContext();
            ctx.Services.AddMudServices();
            jsInterop = ctx.JSInterop.SetupModule("/JSModules/LocalStorageAccessor.js");
            ctx.JSInterop.SetupVoid("mudPopover.connect", _ => true);
            var mockLocalStorageService = new Mock<LocalStorageService>(jsInterop.JSRuntime);
            var mockThemeService = new Mock<ThemeService>(mockLocalStorageService.Object);
            ctx.Services.AddSingleton(mockLocalStorageService.Object);
            ctx.Services.AddSingleton(mockThemeService.Object);
        }

        public IRenderedComponent<QuoteComponent> RenderQuoteComponent()
        {
            var comp = ctx.RenderComponent<QuoteComponent>(parameters =>
                parameters
                .Add(p => p.QuoteText, quoteText)
              );

            return comp;
        }

        [Fact]
        public void ItShouldRenderQuoteComponent()
        {
            var comp = RenderQuoteComponent();
            var quoteContainer = comp.Find($"#{WebConstants.QuoteComponentIds.Container}");
            Assert.NotNull(quoteContainer);
        }

        [Fact]
        public void ItShouldSetQuoteTextParamater()
        {
            var comp = RenderQuoteComponent();
            var quoteContainer = comp.Find($"#{WebConstants.QuoteComponentIds.Container}");
            Assert.Equal(quoteContainer.TextContent, quoteText);
        }
    }
}
