using LabbXamarinAndroidUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LabbXamarinAndroidUWP.ViewModels
{
    public class CrimeEventViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<data> CrimeEventList { get; set; } = new ObservableCollection<data>();
        public bool isLoadingAPI { get; set; } // When false, don't show loading indicator
        public bool showContent { get; set; } // When true, show API data 
        internal async Task LoadData()
        {
            string apiURL = @"http://brottsplatskartan.se/api/events/";

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(apiURL)); // Calling api

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // This one requires the Root class and cannot be directly made into a list
                    CrimeEventRoot CrimeRoot = JsonConvert.DeserializeObject<CrimeEventRoot>(content);

                    // Data is an array / list, so it is seperated and put into my list
                    foreach (var item in CrimeRoot.data)
                    {
                        CrimeEventList.Add(item);
                    }
                    StopActivityInidcator();
                    RaisePropertyChanged(nameof(CrimeEventList));
                }
                else
                {
                    //TODO: Error logic

                    LoadTestData();
                    StopActivityInidcator();
                }
            }
            catch (Exception error)
            {
                //TODO: Error logic
                StopActivityInidcator();
            }
        }

        // Show content, remove loading screen and tell Xamarin the properties are changed
        protected void StopActivityInidcator() 
        {
            isLoadingAPI = false;
            RaisePropertyChanged(nameof(isLoadingAPI));

            showContent = true;
            RaisePropertyChanged(nameof(showContent));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void LoadTestData()
        {
            List<data> datalist = new List<data>();
            datalist.Add(new data { location_string = "Göteborg", title_type = "Dråp", content_teaser = "Man för dråp", id = 1});
            datalist.Add(new data { location_string = "Stenugnsund", title_type = "Trafikkontroll", content_teaser = "Magnus körde fort",id = 2 });
            datalist.Add(new data { location_string = "Trollhättan", title_type = "Inbrott", content_teaser = "Man bröt sig in hos Mahnus", id = 3 });

            foreach (var item in datalist)
            {
                CrimeEventList.Add(item);
            }
        }
    }
}
