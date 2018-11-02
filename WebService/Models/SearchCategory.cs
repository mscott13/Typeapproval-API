using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class SearchCategory
    {
        public SearchCategory(string title, string url, string description, string category)
        {
            this.title = title;
            this.url = url;
            this.description = description;
            this.category = category;
        }

        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string category { get; set; }
    }
}