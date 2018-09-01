using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocean2City.Common.CommonData;
using Ocean2City.ViewModel.Order;
using Ocean2City.Common.Enums;
using Ocean2City.Business.Interfaces;
using Ocean2City.ViewModel.User;
using Ocean2City.Business.Logic;

namespace Ocean2City.WebApi.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Order/[Action]")]
    public class OrderController : Controller
    {
        private readonly IUserManagerService _userManager;
        private readonly OrderDetailManagerService _orderDetailManager;

        /// <summary>
        /// Initializes a new instance of the OrderController
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="orderDetailManager"></param>
        public OrderController(IUserManagerService userManager, OrderDetailManagerService orderDetailManager)
        {
            _userManager = userManager;
            _orderDetailManager = orderDetailManager;
        }

        /// <summary>
        /// Save Order Details.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public IResult ConfirmedOrder([FromBody]Order order)
        {
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                var userDetail = _userManager.SaveUserDetails(order.UserViewModel, order.UserAddressViewModel);
                if(userDetail != null)
                {
                    var orderDetails = _orderDetailManager.SaveOrderDetails(order.OrderDetailViewModel, order.CartItemList, userDetail.Body.userDetail);
                }

            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }
    }
}