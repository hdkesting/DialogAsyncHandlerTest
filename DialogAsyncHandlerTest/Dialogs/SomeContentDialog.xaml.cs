using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DialogAsyncHandlerTest.Dialogs
{
    public sealed partial class SomeContentDialog : ContentDialog
    {
        public SomeContentDialog()
        {
            this.InitializeComponent();
        }

        public string Result { get; set; } = "Initial";

        /// <summary>
        /// Try and use an async handler, but failing. The dialog exits on the first real await (but the code keeps running).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void ContentDialog_ButtonClick_Wrong(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Result = "Still Getting";
            var res = await GetSomethingAsync();
            this.Result = res;
        }

        /// <summary>
        /// The correct way of using an async handler here: a deferral.
        /// </summary>
        /// <remarks>
        /// Because the handler needs a void return type, we need another way to signal completion. 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void ContentDialog_ButtonClick_Correct(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var defer = args.GetDeferral();

            this.Result = "Still Getting";
            var res = await GetSomethingAsync();
            this.Result = res;

            // finally mark the action as Complete
            defer.Complete();
        }

        private async Task<string> GetSomethingAsync()
        {
            // simulate some long-running async work
            await Task.Delay(1000);
            return "Got it";
        }
    }
}
