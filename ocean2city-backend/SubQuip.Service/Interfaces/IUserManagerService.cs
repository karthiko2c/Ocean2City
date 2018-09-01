using Ocean2City.Common.CommonData;
using Ocean2City.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Business.Interfaces
{
    public interface IUserManagerService
    {
        /// <summary>
        /// Get Specific User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IResult GetUserById(string id);

        /// <summary>
        /// Get All Users.
        /// </summary>
        /// <returns></returns>
        IResult GetAllUsers();

        /// <summary>
        /// Save User Details.
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="userAddressViewModel"></param>
        /// <returns></returns>
        IResult SaveUserDetails(UserViewModel userViewModel, UserAddressViewModel userAddressViewModel);

    }
}
