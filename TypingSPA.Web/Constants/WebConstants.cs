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
            public static readonly string TypingComponent = "TypingComponent";
        }

        public static class LocalStorageSettingConstants
        {
            public static readonly string ThemeSettingName = "TypingSPAThemeSettings";
        }

        public static class QuoteComponentIds
        {
            public static readonly string Container = "QuoteContainer";
            public static readonly string Caret = "Caret";
        }

        public static class QuoteComponentConstants
        {
            public static readonly char CaretDelimiter = '|';
            public static readonly string ErrorClass = "mud-tertiary-text";
            public static readonly string SuccessClass = "mud-secondary-text";
            public static readonly string NoInputClass = "mud-palette-white";
        }

        public static class TypingComponentIds
        {
            public static readonly string HiddenInput = "HiddenInput";
            public static readonly string QuoteDisplay = "QuoteDisplay";
        }

    }
}
