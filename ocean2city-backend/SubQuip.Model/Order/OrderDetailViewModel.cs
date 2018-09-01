using Ocean2City.ViewModel.CartItem;
using Ocean2City.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.ViewModel.Order
{
    public class OrderDetailViewModel
    {
        public string OrderId { get; set; }

        public string UserId { get; set; }

        public string AddressId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryTiming { get; set; }
        
        public bool IsDelivered { get; set; }
        
        public double TotalAmount { get; set; }

        public List<CartItemViewModel> cartItems { get; set; }
    }

    public class Order
    {
        public UserViewModel UserViewModel { get; set; }

        public UserAddressViewModel UserAddressViewModel { get; set; }

        public List<CartItemViewModel> CartItemList { get; set; }

        public OrderDetailViewModel OrderDetailViewModel { get; set; }
    }
}
