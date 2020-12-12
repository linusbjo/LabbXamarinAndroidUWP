using System;
using System.Collections.Generic;
using System.Text;

namespace LabbXamarinAndroidUWP.Models
{

    // Everything below is made from JSON and I do not dare to change any of the names, except the root class
    public class CrimeEventRoot
    {
        public Links links { get; set; }
        public data[] data { get; set; }
    }
    public class Links
    {
        public int current_page { get; set; }
        public string first_page_url { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public string last_page_url { get; set; }
        public string next_page_url { get; set; }
        public string path { get; set; }
        public int per_page { get; set; }
        public object prev_page_url { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }

    public class data
    {
        public int id { get; set; }
        public DateTime pubdate_iso8601 { get; set; }
        public string pubdate_unix { get; set; }
        public string title_type { get; set; }
        public string title_location { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string content_formatted { get; set; }
        public string content_teaser { get; set; }
        public string location_string { get; set; }
        public string date_human { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public string viewport_northeast_lat { get; set; }
        public string viewport_northeast_lng { get; set; }
        public string viewport_southwest_lat { get; set; }
        public string viewport_southwest_lng { get; set; }
        public string administrative_area_level_1 { get; set; }
        public object administrative_area_level_2 { get; set; }
        public string image { get; set; }
        public string external_source_link { get; set; }
        public string permalink { get; set; }
    }
}
