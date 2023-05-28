using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TypingSPA.Web.Constants
{
    public static class WebConstants
    {
        public static readonly string LandingPageWelcome = "TODO: LandingPage Message";

        public static class ComponentIDs
        {
            public static readonly string GlobalNav = "GlobalNav";
            public static readonly string LandingPage = "LandingPage";
            public static readonly string GlobalFooter = "GlobalFooter";
            public static readonly string MainComponent = "MainComponent";
        }

        public static class LocalStorageSettingConstants
        {
            public static readonly string ThemeSettingName = "TypingSPAThemeSettings";
        }
    }
}
