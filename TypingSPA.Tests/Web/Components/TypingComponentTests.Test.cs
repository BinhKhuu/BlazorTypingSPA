using AngleSharp.Dom;
using AngleSharpWrappers;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingSPA.Web.Components;
using TypingSPA.Web.Constants;
using TypingSPA.Web.Services;

namespace TypingSPA.Tests.Web.Components
{
    public class TypingComponentTests
    {
        private TestContext ctx { get; set; }
        private BunitJSInterop jsInterop { get; set; }


        public TypingComponentTests() {
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
        public void ItShouldRenderTypingComponent()
        {
            var comp = ctx.RenderComponent<TypingSPA.Web.Components.TypingComponent>();
            var typingComponent = comp.Find($"#{WebConstants.ComponentIDs.TypingComponent}");
            var hiddenInput = comp.Find($"#{WebConstants.TypingComponentIds.HiddenInput}");
            var quoteDisplay = comp.Find($"#{WebConstants.TypingComponentIds.QuoteDisplay}");
            Assert.NotNull( typingComponent );
            Assert.NotNull(hiddenInput);
            Assert.NotNull(quoteDisplay);
        }

        [Fact]
        public async Task ClickShouldFocusHiddenInput()
        {
            var comp = ctx.RenderComponent<TypingSPA.Web.Components.TypingComponent>();
            var typingComponent = comp.Find($"#{WebConstants.ComponentIDs.TypingComponent}");
            await typingComponent.ClickAsync(new MouseEventArgs());
            // not IsFocus Attribute is not working
            Assert.True(comp.Instance.HiddenInputIsFocused);
        }

        // no on mouseenter has to be a custom even triggered https://bunit.dev/docs/interaction/trigger-event-handlers.html?tabs=csharp
        //[Fact]
        //public async Task MouseEnterShouldFocusHiddenInput()
        //{
        //    var comp = ctx.RenderComponent<TypingSPA.Web.Components.TypingComponent>();
        //    var typingComponent = comp.Find($"#{WebConstants.ComponentIDs.TypingComponent}");
        //    // there is no onmouseenter has to be custom
        //    // not IsFocus Attribute is not working
        //    Assert.True(comp.Instance.HiddenIsFocus);
        //}

        // follw this for unit testing guide https://github.com/MudBlazor/MudBlazor/blob/dev/src/MudBlazor.UnitTests/Components/SelectTests.cs
        [Fact]
        public async Task VerifyFocusEventCalled()
        {

            var mockFocus = new Mock<TypingComponent>();
            mockFocus.Setup(m => m.FocusHiddenInput())
                .Verifiable();

            ctx.ComponentFactories.Add<TypingComponent>(mockFocus.Object);
            var comp = ctx.RenderComponent<TypingComponent>();
            await comp.InvokeAsync(() => comp.Instance.FocusHiddenInput());
            mockFocus.Verify();
            Assert.True(true);
        }

        [Fact]
        public void TypingAlphaNumbericAndPunctuationShouldUpdateTextComponent()
        {
            var comp = ctx.RenderComponent<TypingSPA.Web.Components.TypingComponent>();
            var hiddenInput = comp.Find($"#{WebConstants.TypingComponentIds.HiddenInput}");
            //hiddenInput.TriggerEvent("OnInputUpdate", new KeyboardEventArgs
            //{
            //    Key = "A"
            //});
            hiddenInput.KeyDown(new KeyboardEventArgs
            {
                Key = "a",
                Code = "KeyA"
            });
            Assert.True(comp.Instance.CurrentInputText == "a");
        }
        [Fact]
        public void TypingSpecialKeysShouldIgnore()
        {
            var comp = ctx.RenderComponent<TypingSPA.Web.Components.TypingComponent>();
            var hiddenInput = comp.Find($"#{WebConstants.TypingComponentIds.HiddenInput}");
            hiddenInput.KeyDown(new KeyboardEventArgs
            {
                Key = "shift",
                Code = "leftshift"
            });
            Assert.True(string.IsNullOrEmpty(comp.Instance.CurrentInputText));
        }

    }
}
