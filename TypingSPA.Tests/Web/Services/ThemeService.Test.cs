﻿using Bunit;
using Common.Web.Models;
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
            var themeSettings = new ThemeSettings();
            Action<ThemeSettings> action = (setting) => { themeSettings.IsDarkMode = true; };
            _themeService.AddThemeSubscription(action);
            _themeService.SettingsObservable.UpdateThemeSettings(themeSettings);
            Assert.True(themeSettings.IsDarkMode);
        }
    }
}
