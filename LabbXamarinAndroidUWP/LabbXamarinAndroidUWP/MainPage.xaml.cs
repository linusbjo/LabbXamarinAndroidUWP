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
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.CrimeEvents;
        }

        async void btnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // This modal comes from Microsoft docs
            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/modal

            try
            {
                if (e.SelectedItem != null)
                {
                    // Converting selectedItem into data object
                    data crimeEvent = (data)e.SelectedItem;
                    var detailPage = new DetailPage();

                    // The class data will be sent to the detailsPage. Not sure how it exactly works, but it works like bindingContext
                    detailPage.BindingContext = crimeEvent;
                    
                    await Navigation.PushModalAsync(detailPage);
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Error", error.Message.ToString(), "Close");
            }         
        }
    }
}
