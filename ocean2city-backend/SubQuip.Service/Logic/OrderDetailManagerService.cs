using MongoDB.Bson;
using MongoDB.Driver;
using Ocean2City.Business.Interfaces;
using Ocean2City.Common.CommonData;
using Ocean2City.Common.Enums;
using Ocean2City.Common.Extensions;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;
using Ocean2City.ViewModel.CartItem;
using Ocean2City.ViewModel.Order;
using Ocean2City.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ocean2City.Business.Logic
{
    public class OrderDetailManagerService : IOrderDetailManagerService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailManagerService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        /// <summary>
        /// Get All OrderDetails.
        /// </summary>
        /// <returns></returns>
        public IResult GetAllOrderDetails()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var ordersList = new List<OrderDetailViewModel>();
                var orders = _orderDetailRepository.Query.ToList();
                if (orders != null && orders.Any())
                {
                    result.Body = ordersList.MapFromModel<OrderDetail, OrderDetailViewModel>(orders);
                }
                else
                {
                    result.Message = CommonErrorMessages.NoResultFound;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        /// <summary>
        /// Get Specific OrderDetails.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult GetOrderDetailsById(string id)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    OrderDetailViewModel orderDetailViewModel = null;
                    var orderDetail = _orderDetailRepository.GetOne(t => t.OrderId == ObjectId.Parse(id));
                    if (orderDetail != null)
                    {
                        orderDetailViewModel = new OrderDetailViewModel();
                        orderDetailViewModel.MapFromModel(orderDetail);
                        if (orderDetail.CartItems == null && orderDetail.CartItems.Any())
                        {
                            var cartItemList = new List<CartItem>();
                            orderDetailViewModel.cartItems = cartItemList.MapFromModel<CartItem, CartItemViewModel>(orderDetail.CartItems);
                        }
                        result.Body = orderDetailViewModel;
                    }
                    else
                    {
                        result.Message = OrderNotification.NoOrder;
                    }
                }
                else
                {
                    result.Status = Status.Fail;
                    result.Message = CommonErrorMessages.NoIdentifierProvided;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        /// <summary>
        /// Save Order Details.
        /// </summary>
        /// <param name="orderDetailViewModel"></param>
        /// <param name="cartItemList"></param>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public IResult SaveOrderDetails(OrderDetailViewModel orderDetailViewModel, List<CartItemViewModel> cartItemList, UserDetailForOrder userDetail)
        {
            orderDetailViewModel.OrderId = null;
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                OrderDetail orderDetail = null;
                if (orderDetailViewModel != null)
                {
                    orderDetail = new OrderDetail();
                    orderDetail.MapFromViewModel(orderDetailViewModel);
                    if (userDetail != null)
                    {
                        orderDetail.UserId = ObjectId.Parse(userDetail.UserId);
                        orderDetail.AddressId = ObjectId.Parse(userDetail.AddressId);
                    }
                    else
                    {
                        result.Message = UserNotification.NoUser;
                    }
                    orderDetail.CartItems = new List<CartItem>();
                    _orderDetailRepository.InsertOne(orderDetail);

                    if (cartItemList != null && cartItemList.Any())
                    {
                        var cartItems = new List<CartItem>();
                        cartItemList.ForEach(x =>
                        {
                            var cartItem = new CartItem();
                            cartItem.MapFromViewModel(x);
                            cartItems.Add(cartItem);
                        });
                        var updateDefinition = Builders<OrderDetail>.Update.AddToSetEach(x => x.CartItems, cartItems);
                        _orderDetailRepository.UpdateOne(t => t.OrderId == orderDetail.OrderId, updateDefinition);
                    }
                    else
                    {
                        result.Message = OrderNotification.NoCartItems;
                    }
                    orderDetailViewModel.MapFromModel(orderDetail);
                    orderDetailViewModel.cartItems = cartItemList;
                    result.Body = orderDetailViewModel;
                    result.Message = OrderNotification.Saved;
                }
                else
                {
                    result.Message = OrderNotification.OrderDetailsNotProvided;
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
