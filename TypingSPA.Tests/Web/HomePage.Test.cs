using Bunit;
using Common.Web.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingSPA.Tests.Web
{
    public class HomePage
    {

        

        [Fact]
        public void ShouldLoadPage()
        {
            using var ctx = new TestContext();
            var comp = ctx.RenderComponent<TypingSPA.Web.Pages.Index>();
            var headingElement = comp.Find("#LandingPageWelcome");

            var typingMessage = headingElement.TextContent;
            Assert.Equal(typingMessage, WebConstants.LandingPageWelcome);

        }
    }
}
