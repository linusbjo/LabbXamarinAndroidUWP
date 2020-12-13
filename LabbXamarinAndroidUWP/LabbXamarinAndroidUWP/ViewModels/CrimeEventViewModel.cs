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
                //if(false)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // This one requires the Root class and cannot be directly made into a list
                    CrimeEventRoot CrimeRoot = JsonConvert.DeserializeObject<CrimeEventRoot>(content);

                    // Data is an array / list, so it is seperated and put into my list
                    foreach (var item in CrimeRoot.data)
                    {
                        // Content contains html tags which shows up in Xamarin
                        item.content = RemoveTagsHTML(item.content);
                       
                        // If content does not contain alot of text, I just type what is normally said
                        if (item.content_teaser == null || item.content_teaser.Length < 3)
                        {
                            item.content_teaser = "Händelsesidan uppdateras inte mer i dag."; // Content teaser is sometimes empty, I add the description explaining why
                        }

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

        // Test data if API fails
        protected void LoadTestData()
        {
            // Just a bunch of test data to see the design
            List<data> datalist = new List<data>();
            datalist.Add(new data { location_string = "Göteborg", title_type = "Dråp", content_teaser = "Gripen för dråp", id = 1, content = "Gripen för dråp", date_human = "2 timmar sedan" });
            datalist.Add(new data { location_string = "Stenugnsund", title_type = "Trafikkontroll", content_teaser = "Anders körde fort",id = 2, content = "Anders körde fort", date_human = "2 timmar sedan" });
            datalist.Add(new data { location_string = "Trollhättan", title_type = "Inbrott", content_teaser = "Man bröt sig in hos Jonas", id = 3, content = "Man bröt sig in hos Jonas", date_human = "2 timmar sedan" });

            foreach (var item in datalist)
            {
                CrimeEventList.Add(item);
            }
        }
        protected string RemoveTagsHTML(string content)
        {
            // Using regex to remove the HTML tags
            // https://stackoverflow.com/questions/18153998/how-do-i-remove-all-html-tags-from-a-string-without-knowing-which-tags-are-in-it

            try
            {
                return System.Text.RegularExpressions.Regex.Replace(content, "<.*?>", String.Empty);
            }
            catch 
            {
                // return content if regex failed, even if it contains the tags
                return content;
            }
           
        }
    }
}
