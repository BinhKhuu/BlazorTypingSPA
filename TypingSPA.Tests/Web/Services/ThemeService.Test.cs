using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingSPA.Web.Services;

namespace TypingSPA.Tests.Web.Services
{
    public class ThemeServiceTests
    {
        private ThemeService _themeService { get; set; }

        public ThemeServiceTests() {
            _themeService = new ThemeService();
        }

        [Fact]
        public void ShouldSetDarkModeToTrue()
        {
            bool darkMode = false;
            Action<bool> action = (test) => { darkMode = true; };
            _themeService.AddDarkModeSubscription(action);
            _themeService.IsDarkModeObservable.UpdateDarkMode(true);
            Assert.True(darkMode);
        }
    }
}
