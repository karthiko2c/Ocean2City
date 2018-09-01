using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.ViewModel.User
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string MobNo { get; set; }

        public string MailId { get; set; }

        public List<UserAddressViewModel> UserAddress { get; set; }
    }

    public class UserDetailForOrder
    {
        public string UserId { get; set; }

        public string AddressId { get; set; }

        public string MobNo { get; set; }

        public string MailId { get; set; }
    }
}
