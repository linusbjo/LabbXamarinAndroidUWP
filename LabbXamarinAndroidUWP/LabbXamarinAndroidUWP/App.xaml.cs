using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LabbXamarinAndroidUWP.ViewModels;

namespace LabbXamarinAndroidUWP
{
    public partial class App : Application
    {

        static public CrimeEventViewModel CrimeEvents { get; set; } = new CrimeEventViewModel();
        public App()
        {
            InitializeComponent();
            App.CrimeEvents.isLoadingAPI = true; //TODO: find better spot for it
            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            await CrimeEvents.LoadData();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
