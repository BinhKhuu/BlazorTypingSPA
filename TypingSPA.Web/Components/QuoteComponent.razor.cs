using Microsoft.AspNetCore.Components;

namespace TypingSPA.Web.Components
{
    public partial class QuoteComponent : ComponentBase
    {
        [Parameter]
        public string QuoteText { get; set; }

        public string FontColourClass { get; set; } = "mud-palette-white";
        private const string errorColour = "mud-tertiary-text";
        private const string successColour = "mud-secondary-text";


    }
}
