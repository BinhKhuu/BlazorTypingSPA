using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace TypingSPA.Web.Components
{
    public partial class TypingComponent : ComponentBase
    {

        public ElementReference HiddenInputRef {get; set;}
        public string TestText { get; set;}
        public bool HiddenIsFocus { get; set;}
        public virtual async Task FocusHiddenInput()
        {
            await HiddenInputRef.FocusAsync(true);
            HiddenIsFocus = true;
        }

        public void OnInputUpdate(KeyboardEventArgs e)
        {
            // todo: Alpha numeric space and punctuation characters add to screen

            // todo: Handle backspace

            // todo: Handle Escape

            // todo: Handle reset shortcut
            TestText = String.Concat(TestText, e.Key);
        }

    }
}
