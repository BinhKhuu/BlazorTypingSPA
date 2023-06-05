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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Quote = OriginalQuote;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Quote = UpdateCursor();
            ValidateInput();
            // check if current input is correct

        }

        private string UpdateCursor()
        {
            var cLen = CurrentInputText.Length == 0 ? 0 : CurrentInputText.Length;
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

        private void ValidateInput()
        {
            int currentPosition = CurrentInputText.Length;
            if (currentPosition > OriginalQuote.Length) return;
            if (currentPosition == 0)
            {
                CompletedText = string.Empty;
                return;
            }

            char? currentInput = CurrentInputText[currentPosition - 1];
            char? currentProgress = OriginalQuote[currentPosition - 1];

            if(currentPosition < CompletedText.Length)
            {
                CompletedText = CompletedText.Substring(0,currentPosition);
                return;
            }

            if(currentInput == currentProgress)
            {
                // success
                CompletedText += "1";
            }
            else
            {
                // error
                CompletedText += "0";
            }
        }
    }
}
