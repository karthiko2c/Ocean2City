using MongoDB.Bson;
using MongoDB.Driver;
using Ocean2City.Business.Interfaces;
using Ocean2City.Common.CommonData;
using Ocean2City.Common.Enums;
using Ocean2City.Common.Extensions;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;
using Ocean2City.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ocean2City.Business.Logic
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserRepository _userRepository;
        public UserManagerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get All Users.
        /// </summary>
        /// <returns></returns>
        public IResult GetAllUsers()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var usersList = new List<UserViewModel>();
                var users = _userRepository.Query.ToList();
                if (users != null && users.Any())
                {
                    result.Body = usersList.MapFromModel<User, UserViewModel>(users);
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
        /// Get Specific user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult GetUserById(string id)
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
                    UserViewModel userViewModel = null;
                    var user = _userRepository.GetOne(t => t.UserId == ObjectId.Parse(id));
                    if (user != null)
                    {
                        userViewModel = new UserViewModel();
                        userViewModel.MapFromModel(user);
                        if (user.UserAddress == null && user.UserAddress.Any())
                        {
                            var addresses = new List<UserAddressViewModel>();
                            userViewModel.UserAddress = addresses.MapFromModel<UserAddress, UserAddressViewModel>(user.UserAddress);
                        }
                        result.Body = userViewModel;
                    }
                    else
                    {
                        result.Message = UserNotification.NoUser;
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
        /// Save User Details
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="userAddressViewModel"></param>
        /// <returns></returns>
        public IResult SaveUserDetails(UserViewModel userViewModel, UserAddressViewModel userAddressViewModel)
        {
            userViewModel.UserId = null;
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                UserDetailForOrder userDetail = null;
                User user = null;
                UserAddress userAddress = null;
                if (userViewModel != null)
                {
                    user = _userRepository.GetOne(x => x.MailId.Equals(userViewModel.MailId) || x.MobNo.Equals(userViewModel.MobNo));
                    if (user == null)
                    {
                        user = new User();
                        user.MapFromViewModel(userViewModel);
                        user.UserAddress = new List<UserAddress>();
                        _userRepository.InsertOne(user);
                    }
                    else
                    {
                        var updateDefinition = Builders<User>.Update
                            .Set(x => x.Name, userViewModel.Name)
                            .Set(x => x.MobNo, userViewModel.MobNo)
                            .Set(x => x.MailId, userViewModel.MailId);
                        _userRepository.UpdateOne(t => t.UserId == user.UserId, updateDefinition);
                    }

                    userDetail.UserId = user.UserId.ToString();
                    userDetail.MailId = user.MailId;
                    userDetail.MobNo = user.MobNo;
        

                    if (userAddressViewModel != null)
                    {
                        if (string.IsNullOrEmpty(userAddressViewModel.AddressId))
                        {
                            userAddress = new UserAddress();
                            userAddress.MapFromViewModel(userAddressViewModel);
                            userAddress.AddressId = ObjectId.GenerateNewId();
                            userDetail.AddressId = userAddress.AddressId.ToString();
                            var updateDefinition = Builders<User>.Update.AddToSet(x => x.UserAddress, userAddress);
                            _userRepository.UpdateOne(t => t.UserId == user.UserId, updateDefinition);
                        }
                        else
                        {
                            userDetail.AddressId = userAddressViewModel.AddressId.ToString();
                        }
                    }
                    result.Body = userDetail;
                    result.Message = UserNotification.Saved;
                }
                else
                {
                    result.Message = UserNotification.UserDetailsNotProvided;
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
