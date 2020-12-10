using System;
using System.Collections.Generic;
using System.Text;

namespace LabbXamarinAndroidUWP.Models
{
    public class BookRoot
    {
        public Book[] books { get; set; }
    }
    
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public Shelf Shelf { get; set; }
        public Author[] Authors { get; set; }
    }

    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
