using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.ViewModel.CartItem
{
    public class CartItemViewModel
    {
        public string CartId { get; set; }

        public string ItemId { get; set; }

        public int ItemQuantity { get; set; }

        public bool IsCleaned { get; set; }

        public double Price { get; set; }
    }
}
