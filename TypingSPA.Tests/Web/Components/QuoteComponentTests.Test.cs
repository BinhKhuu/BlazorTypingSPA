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
                .Add(p => p.OriginalQuote, quoteText)
                .Add(p => p.CurrentInputText, "")
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

        [Fact]
        public void CursorShouldUpdatePosition()
        {
            var comp = ctx.RenderComponent<QuoteComponent>(parameters =>
                parameters
                .Add(p => p.OriginalQuote, quoteText)
                .Add(p => p.CurrentInputText, "")
            );

            comp.SetParametersAndRender(parameters =>
                parameters.Add(p => p.CurrentInputText, "T"));

            var quoteContainer = comp.Find($"#{WebConstants.QuoteComponentIds.Container}");
            var caret = comp.Find($"#{WebConstants.QuoteComponentIds.Caret}");
            Assert.NotNull(caret);
            Assert.Equal(comp.Instance.Quote, comp.Instance.OriginalQuote[0] + "|" + comp.Instance.OriginalQuote.Substring(1));
        }

        [Fact]
        public void CursorShouldBeAtEnd()
        {
            var comp = ctx.RenderComponent<QuoteComponent>(parameters =>
                parameters
                .Add(p => p.OriginalQuote, quoteText)
                .Add(p => p.CurrentInputText, quoteText)
            );

            var quoteContainer = comp.Find($"#{WebConstants.QuoteComponentIds.Container}");
            var caret = comp.Find($"#{WebConstants.QuoteComponentIds.Caret}");
            Assert.NotNull(caret);
            Assert.Equal(comp.Instance.Quote, comp.Instance.OriginalQuote + "|");
        }

        [Fact]
        public void CaretShouldBeAtStart()
        {
            var comp = ctx.RenderComponent<QuoteComponent>(parameters =>
                parameters
                .Add(p => p.OriginalQuote, quoteText)
                .Add(p => p.CurrentInputText, "")
              );

            var quoteContainer = comp.Find($"#{WebConstants.QuoteComponentIds.Container}");
            var caret = comp.Find($"#{WebConstants.QuoteComponentIds.Caret}");
            Assert.NotNull(caret);
            Assert.Equal(comp.Instance.Quote, "|" + comp.Instance.OriginalQuote);
        }
    }
}
