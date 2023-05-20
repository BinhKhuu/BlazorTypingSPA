using Common.Web.Models;
using Common.Web.Observables;
using MudBlazor;
using System;
using static Common.Web.Observables.ThemeSettingsObservable;

namespace TypingSPA.Web.Services
{
    public class ThemeService
    {
        public ThemeSettingsObservable SettingsObservable { get; set; } 

        public MudTheme Theme { get; set; }
        public ThemeService() { 
            Theme = new MudTheme();
            SettingsObservable = new ThemeSettingsObservable();
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
