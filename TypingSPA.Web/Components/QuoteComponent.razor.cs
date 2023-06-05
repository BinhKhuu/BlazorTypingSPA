using Microsoft.AspNetCore.Components;
using TypingSPA.Web.Constants;

namespace TypingSPA.Web.Components
{
    public partial class QuoteComponent : ComponentBase
    {
        [Parameter]
        public string OriginalQuote { get; set; } = string.Empty;
        [Parameter]
        public string CurrentInputText { get; set; }

        private string CompletedText { get; set; } = string.Empty;
        public string Quote { get; set; } = string.Empty;

        public string FontColourClass { get; set; } = "mud-palette-white";
        private const string errorColour = "mud-tertiary-text";
        private const string successColour = "mud-secondary-text";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Quote = OriginalQuote;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // check if current input is correct

            // move cursour up
            Quote = UpdateCursor();
        }

        // TODO: Write test for this fuction
        private string UpdateCursor()
        {
            CompletedText = CurrentInputText;
            var cLen = CompletedText.Length == 0 ? 0 : CompletedText.Length;
            string inCompletedSection;
            string completedSection = string.Empty;

            if (cLen == 0)
                return WebConstants.QuoteComponentConstants.CaretDelimiter + OriginalQuote;

            if (cLen >= OriginalQuote.Length)
                return OriginalQuote + WebConstants.QuoteComponentConstants.CaretDelimiter;

            if (cLen > 0)
            {
                completedSection = OriginalQuote.Substring(0, cLen);
                inCompletedSection = OriginalQuote.Substring(cLen);
            }
            else
            {
                inCompletedSection = OriginalQuote;
            }

            var newQuote = completedSection + WebConstants.QuoteComponentConstants.CaretDelimiter + inCompletedSection;
            return newQuote;
        }

    }
}
