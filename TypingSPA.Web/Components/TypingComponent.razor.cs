using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace TypingSPA.Web.Components
{
    public partial class TypingComponent : ComponentBase
    {

        public ElementReference HiddenInputRef {get; set;}
        public string CurrentInputText { get; set; } = "";
        public string QuoteText { get; set; } = "test";
        public bool HiddenInputIsFocused { get; set;}
        public virtual async Task FocusHiddenInput()
        {
            await HiddenInputRef.FocusAsync(true);
            HiddenInputIsFocused = true;
        }

        public void OnInputUpdate(KeyboardEventArgs e)
        {
            if (e.Code.StartsWith("Key") || e.Code.StartsWith("Digit"))
            {
                CurrentInputText = String.Concat(CurrentInputText, e.Key);
                return;
            }

            if (e.Code == "Space")
            {
                CurrentInputText = String.Concat(CurrentInputText, " ");
                return;
            }

            if(e.Code == "Backspace")
            {
                if(CurrentInputText.Length > 0) 
                    CurrentInputText = CurrentInputText.Substring(0,CurrentInputText.Length - 1);
                return;
            }

            var punctuationMatcher = new Regex(@"[^\w\s]+");

            if (punctuationMatcher.Match(e.Key).Success)
            {
                CurrentInputText = String.Concat(CurrentInputText, e.Key);
                return;
            }

            // todo: Handle Escape
            if(e.Code == "Escape")
            {
                return;
            }
            // todo: Handle reset shortcut

        }

        public Action OnFoucsOut => () => HiddenInputIsFocused = false;

    }
}
