using System;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DialogAsyncHandlerTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Dialogs.SomeContentDialog();
            this.Result.Text = dlg.Result;
            var res = await dlg.ShowAsync();
            this.Result.Text = $"Button clicked: {res}; Result: {dlg.Result}";

            await Task.Delay(1100);
            this.Result.Text += $"; Result2: {dlg.Result}";
        }
    }
}
