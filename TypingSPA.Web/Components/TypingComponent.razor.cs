using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace TypingSPA.Web.Components
{
    public partial class TypingComponent : ComponentBase
    {

        public ElementReference HiddenInputRef {get; set;}
        public string TestText { get; set;}
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
                TestText = String.Concat(TestText, e.Key);
                return;
            }

            if (e.Code == "Space")
            {
                TestText = String.Concat(TestText, " ");
                return;
            }

            if(e.Code == "Backspace")
            {
                if(TestText.Length > 0) 
                    TestText = TestText.Substring(0,TestText.Length - 1);
                return;
            }

            var punctuationMatcher = new Regex(@"[^\w\s]+");

            if (punctuationMatcher.Match(e.Key).Success)
            {
                TestText = String.Concat(TestText, e.Key);
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
