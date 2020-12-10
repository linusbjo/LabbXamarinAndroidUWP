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
        public bool isLoadingAPI { get; set; }
        public bool showContent { get; set; }
        internal async Task LoadData()
        {
            //string apiURL = @"http://brottsplatskartan.se/api/events/";
            string apiURL = @"http://193.10.202.70/BookApi/api/BooksSimple";
            
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(apiURL)); // Calling api

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // This one requires the Root class and cannot be directly made into a list
                    CrimeEventRoot CrimeRoot = JsonConvert.DeserializeObject<CrimeEventRoot>(content);

                    //TODO: debug
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
                    StopActivityInidcator();
                }
            }
            catch (Exception error)
            {
                StopActivityInidcator();
                string errormsg = error.Message;
                //TODO: Error logic

            }
        }

        //internal async Task AssignDefaultValues() 
        //{
        //    isLoadingAPI = true;
        //    showContent = false;
        //}

        protected void StopActivityInidcator() 
        {
            showContent = true;
            isLoadingAPI = false;

            RaisePropertyChanged(nameof(isLoadingAPI));
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
        
    }
}
