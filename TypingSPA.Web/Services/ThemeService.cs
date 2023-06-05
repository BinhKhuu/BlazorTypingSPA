using TypingSPA.Web.Models;
using TypingSPA.Web.Observables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Text.Json;
using static TypingSPA.Web.Constants.WebConstants;
using static TypingSPA.Web.Observables.ThemeSettingsObservable;

namespace TypingSPA.Web.Services
{
    public class ThemeService
    {
        public ThemeSettingsObservable SettingsObservable { get; set; } 
        private LocalStorageService LocalStorage { get; set; }

        public ThemeService(LocalStorageService localStorage) { 
            LocalStorage = localStorage;
            SettingsObservable = new ThemeSettingsObservable();
        }

        public async Task LoadSettingsFromLocalStorage()
        {
            try
            {
                var themeSettings = await LocalStorage.GetValueAsync<ThemeSettings>(LocalStorageSettingConstants.ThemeSettingName);
                if (themeSettings == null)
                {
                    await SaveLocalStorageDefaultThemeSettings(); //save default values
                }
                else
                {
                    SettingsObservable.Settings = themeSettings;
                }

            }
            catch (Exception ex)
            {
                await SaveLocalStorageDefaultThemeSettings(); //save default values
            }

        }

        public async Task SaveLocalStorageThemeSettings()
        {
            await LocalStorage.SetValueAsync(LocalStorageSettingConstants.ThemeSettingName, SettingsObservable.Settings);
        }

        /// <summary>
        /// control settings subscription to scoped service.
        /// </summary>
        /// <param name="OnThemeSettingsChange"></param>
        /// <returns></returns>
        public IDisposable AddThemeSubscription(Action<ThemeSettings> OnThemeSettingsChange)
        {
            var themeSettingsSubject = new ThemeSettingSubject(OnThemeSettingsChange);
            return SettingsObservable.Subscribe(themeSettingsSubject);
        }

        public async Task SaveLocalStorageDefaultThemeSettings()
        {
            var defaultTheme = new ThemeSettings()
            {
                IsDarkMode = false,
                DefaultScrollBar = false,
                LightTheme = new ThemePalette()
                {
                    Primary = Colors.Yellow.Darken3,
                    Secondary = Colors.Green.Accent4,
                    Tertiary = Colors.Red.Accent4,
                    AppbarBackground = Colors.Red.Default,
                },
                DarkTheme = new ThemePalette()
                {
                    Primary = Colors.Grey.Darken4,
                    Secondary = Colors.Green.Accent4,
                    Tertiary = Colors.Red.Accent4,
                    AppbarBackground = Colors.Red.Default,
                }
            };
            SettingsObservable.Settings = defaultTheme;
            await LocalStorage.SetValueAsync(LocalStorageSettingConstants.ThemeSettingName, defaultTheme);
            
        }

    }
}
