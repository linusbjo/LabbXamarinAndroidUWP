using LabbXamarinAndroidUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace LabbXamarinAndroidUWP.ViewModels
{
    public class CrimeEventViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<data> CrimeEventList { get; set; } = new ObservableCollection<data>();

        internal async System.Threading.Tasks.Task LoadData()
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

                    RaisePropertyChanged("CrimeEventList");
                }
                else
                {
                    //TODO: Error logic
                }
            }
            catch (Exception error)
            {
                //TODO: Error logic
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
