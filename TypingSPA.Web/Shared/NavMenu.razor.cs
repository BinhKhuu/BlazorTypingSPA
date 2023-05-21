using TypingSPA.Web.Models;
using Microsoft.AspNetCore.Components;
using TypingSPA.Web.Services;

namespace TypingSPA.Web.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        private ThemeService ThemeService { get; set; }

        public void ToggleDarkMode()
        {
            ThemeSettings settings = ThemeService.SettingsObservable.Settings;
            settings.IsDarkMode = !settings.IsDarkMode;
            ThemeService.SettingsObservable.UpdateThemeSettings(settings);
        }
    }
}
