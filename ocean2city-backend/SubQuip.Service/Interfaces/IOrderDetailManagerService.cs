using Ocean2City.Common.CommonData;
using Ocean2City.ViewModel.CartItem;
using Ocean2City.ViewModel.Order;
using Ocean2City.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocean2City.Business.Interfaces
{
    public interface IOrderDetailManagerService
    {
        /// <summary>
        /// Get Specific OrderDetails.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IResult GetOrderDetailsById(string id);

        /// <summary>
        /// Get All OrderDetails.
        /// </summary>
        /// <returns></returns>
        IResult GetAllOrderDetails();
        
        /// <summary>
        /// Save Order Details.
        /// </summary>
        /// <param name="orderDetailViewModel"></param>
        /// <param name="cartItemList"></param>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        IResult SaveOrderDetails(OrderDetailViewModel orderDetailViewModel, List<CartItemViewModel> cartItemList, UserDetailForOrder userDetail);
    }
}
