using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.ViewModel.Item
{
    public class ItemViewModel
    {
        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string AliasName { get; set; }

        public string Description { get; set; }

        public string Recipe { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }

        public bool IsAvailable { get; set; }

        public string Category { get; set; }

    }
}
