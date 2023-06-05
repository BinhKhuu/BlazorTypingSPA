using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingSPA.Web.Models
{
    public class ThemeSettings
    {
        public bool IsDarkMode { get; set; } = false;
        public bool DefaultScrollBar { get; set; } = false;

        public ThemePalette? LightTheme { get; set; } = new ThemePalette()
        {
            Primary = Colors.Blue.Default,
            Secondary = Colors.Green.Accent4,
            Tertiary = Colors.Red.Default,
            AppbarBackground = Colors.Red.Default,
        };
        public ThemePalette? DarkTheme { get; set; } = new ThemePalette()
        {
            Primary = Colors.Blue.Default,
            Secondary = Colors.Green.Accent4,
            Tertiary = Colors.Red.Default,
            AppbarBackground = Colors.Red.Default,
        };

    }
}
