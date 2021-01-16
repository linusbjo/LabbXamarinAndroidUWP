using LabbXamarinAndroidUWP.Models;
using System;
using Xamarin.Forms;

namespace LabbXamarinAndroidUWP
{
    public partial class MainPage : ContentPage
    {
        // If I get selected item in itemTapped, it would only select the first item in the list regardless.
        // This variable solved my problem and gets the selected item
        private data _event = new data();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.CrimeEvents;

        }

        // Opens details page
        async void onListViewTapped(object sender, SelectedItemChangedEventArgs e)
        {
            // This modal comes from Microsoft docs
            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/modal
            // https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/listview/?tabs=vswin&tutorial-step=3

            try
            {
                if (e.SelectedItem != null)
                {
                    // Converting selectedItem into data object
                    var detailPage = new DetailPage();

                    // BindingContext for the new page. Sending the 
                    detailPage.BindingContext = _event;
                    await Navigation.PushModalAsync(detailPage);
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Error - could not display data", error.Message.ToString(), "Close");
            }
        }

        // This gets the correct selected item
        async void onListViewSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                _event = e.SelectedItem as data;
            }
            catch (Exception error)
            {
                await DisplayAlert("Error - could not display data", error.Message.ToString(), "Close");
            }
        }
    }
}
