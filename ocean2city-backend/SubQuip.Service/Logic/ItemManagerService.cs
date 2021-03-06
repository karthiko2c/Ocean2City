﻿using MongoDB.Bson;
using MongoDB.Driver;
using Ocean2City.Business.Interfaces;
using Ocean2City.Common.CommonData;
using Ocean2City.Common.Enums;
using Ocean2City.Common.Extensions;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;
using Ocean2City.ViewModel.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Ocean2City.Business.Logic
{
    public class ItemManagerService : IItemManagerService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ClaimsPrincipal _principal;

        public ItemManagerService(IItemRepository itemRepository, IPrincipal principal)
        {
            _itemRepository = itemRepository;
            _principal = principal as ClaimsPrincipal;
        }

        /// <summary>
        /// Get All Items.
        /// </summary>
        /// <returns></returns>
        public IResult GetAllItems()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var itemLists = new List<ItemViewModel>();
                var items = _itemRepository.Query.ToList();
                if (items != null && items.Any())
                {
                    result.Body = itemLists.MapFromModel<Item, ItemViewModel>(items);
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
        /// Get Specific Item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult GetItemById(string id)
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
                    ItemViewModel itemViewModel = null;
                    var item = _itemRepository.GetOne(t => t.ItemId == ObjectId.Parse(id));
                    if (item != null)
                    {
                        itemViewModel = new ItemViewModel();
                        itemViewModel.MapFromModel(item);
                        itemViewModel.ImagePath = FileHelper.GetBase64StringForImage(item.ImagePath);
                        result.Body = itemViewModel;
                    }
                    else
                    {
                        result.Message = ItemNotification.NoItem;
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
        /// Get Item based on category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult GetItemByCategory(string id)
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
                    List<ItemViewModel> itemList = new List<ItemViewModel>();
                    var items = _itemRepository.Query.Where(t => t.Category == ObjectId.Parse(id)).ToList();
                    if (items != null && items.Any())
                    {
                        result.Body = items.Select(x =>
                        {
                            var item = new ItemViewModel();
                            item.MapFromModel(x);
                            item.ImagePath = FileHelper.GetBase64StringForImage(x.ImagePath);
                            return item;
                        }).ToList();
                    }
                    else
                    {
                        result.Message = CommonErrorMessages.NoResultFound;
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
        /// Add a Item
        /// </summary>
        /// <param name="itemViewModel"></param>
        /// <returns></returns>
        public IResult AddItem(ItemViewModel itemViewModel)
        {
            itemViewModel.ItemId = null;
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                Item item = null;
                if (itemViewModel != null)
                {
                    item = new Item();
                    item.MapFromViewModel(itemViewModel, (ClaimsIdentity)_principal.Identity);
                    _itemRepository.InsertOne(item);
                    result.Body = itemViewModel.ItemName;
                    result.Message = ItemNotification.Added;
                }
                else
                {
                    result.Message = ItemNotification.ItemNotProvided;
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
        /// Update Item
        /// </summary>
        /// <param name="itemViewModel"></param>
        /// <returns></returns>
        public IResult UpdateItem(ItemViewModel itemViewModel)
        {
            var result = new Result
            {
                Operation = Operation.Update,
                Status = Status.Success
            };
            try
            {
                var updateDefinition = Builders<Item>.Update
                    .Set(x => x.ItemName, itemViewModel.ItemName)
                    .Set(x => x.Image, itemViewModel.Image)
                    .Set(x => x.IsAvailable, itemViewModel.IsAvailable)
                    .Set(x => x.Recipe, itemViewModel.Recipe)
                    .Set(x => x.PriceWithClean, itemViewModel.PriceWithClean)
                    .Set(x => x.PriceWithoutClean, itemViewModel.PriceWithoutClean)
                    .Set(x => x.AliasName, itemViewModel.AliasName)
                    .Set(x => x.Description, itemViewModel.Description)
                    .Set(x => x.ModifiedDate, DateTime.Now);
                _itemRepository.UpdateOne(t => t.ItemId == ObjectId.Parse(itemViewModel.ItemId), updateDefinition);

                result.Message = ItemNotification.Updated;
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
