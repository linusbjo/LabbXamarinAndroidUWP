﻿using LabbXamarinAndroidUWP.ViewModels;
using Xamarin.Forms;

namespace LabbXamarinAndroidUWP
{
    public partial class App : Application
    {

        static public CrimeEventViewModel CrimeEvents { get; set; } = new CrimeEventViewModel();
        public App()
        {
            InitializeComponent();

            //TODO: Better method for this???
            App.CrimeEvents.isLoadingAPI = true;
            App.CrimeEvents.showContent = false;

            NavigationPage page = new NavigationPage(new MainPage());
            MainPage = page;
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
