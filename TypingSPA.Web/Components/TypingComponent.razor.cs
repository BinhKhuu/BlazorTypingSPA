using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace TypingSPA.Web.Components
{
    public partial class TypingComponent : ComponentBase
    {

        public ElementReference HiddenInputRef {get; set;}
        public string UserTypingText { get; set;}
        public string QuoteText { get; set; } = "The Quick Brown fox jumped over the lazy cow";
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
                UserTypingText = String.Concat(UserTypingText, e.Key);
                return;
            }

            if (e.Code == "Space")
            {
                UserTypingText = String.Concat(UserTypingText, " ");
                return;
            }

            if(e.Code == "Backspace")
            {
                if(UserTypingText.Length > 0) 
                    UserTypingText = UserTypingText.Substring(0,UserTypingText.Length - 1);
                return;
            }

            var punctuationMatcher = new Regex(@"[^\w\s]+");

            if (punctuationMatcher.Match(e.Key).Success)
            {
                UserTypingText = String.Concat(UserTypingText, e.Key);
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
