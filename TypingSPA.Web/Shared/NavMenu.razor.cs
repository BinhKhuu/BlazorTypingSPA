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
            ThemeService.IsDarkModeObservable.UpdateDarkMode(!ThemeService.IsDarkModeObservable.IsDarkMode);
        }
    }
}
