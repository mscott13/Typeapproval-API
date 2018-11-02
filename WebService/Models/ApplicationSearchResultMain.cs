using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ApplicationSearchResultMain
    {
        public ApplicationSearchResultMain(List<SearchCategory> items)
        {
            this.items = items;
            record_count = items.Count;
        }

        public int record_count { get; }
        public List<SearchCategory> items { get; set; }
    }
}