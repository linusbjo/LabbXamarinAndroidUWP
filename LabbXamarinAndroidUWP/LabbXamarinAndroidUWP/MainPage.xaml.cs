using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LabbXamarinAndroidUWP.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace LabbXamarinAndroidUWP
{
    public partial class MainPage : ContentPage
    {

        private data _event = new data();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.CrimeEvents;
        }

        // Opens details page
        async void itemTapped(object sender, SelectedItemChangedEventArgs e)
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
                await DisplayAlert("Error", error.Message.ToString(), "Close");
            }         
        }

        // This gets the correct selected item
        async void itemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                _event = e.SelectedItem as data;
            }
            catch (Exception error)
            {
                await DisplayAlert("Error", error.Message.ToString(), "Close");
            }        
        }
    }
}
