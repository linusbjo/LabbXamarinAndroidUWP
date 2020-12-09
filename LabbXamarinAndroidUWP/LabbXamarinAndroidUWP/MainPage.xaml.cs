using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LabbXamarinAndroidUWP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.CrimeEvents.CrimeEventList;
        }

        private async void btn_UpdateAPI(object sender, EventArgs e)
        {
            await label.RelRotateTo(360, 1000);
            EmployeeView.ItemsSource = App.CrimeEvents.CrimeEventList;
        }
    }
}
