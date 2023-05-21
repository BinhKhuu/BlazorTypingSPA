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
        public MudTheme Theme { get; set; }
        private LocalStorageService LocalStorage { get; set; }

        public ThemeService(LocalStorageService localStorage) { 
            LocalStorage = localStorage;
            Theme = new MudTheme();
            SettingsObservable = new ThemeSettingsObservable();
        }

        public async Task LoadSettingsFromLocalStorage()
        {
            var themeSettings = await LocalStorage.GetValueAsync<ThemeSettings>(LocalStorageSettingConstants.ThemeSettingName);
            if (themeSettings == null)
            {
                SaveLocalStorageThemeSettings();
            }
            else
            {
                SettingsObservable.Settings = themeSettings;
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

    }
}
