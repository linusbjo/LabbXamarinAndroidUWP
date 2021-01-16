using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabbXamarinAndroidUWP
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/label#hyperlinks
        //TODO: This is currently not working
        public System.Windows.Input.ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public DetailPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void onClickedCloseModal(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}