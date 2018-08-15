using Ocean2City.Common.CommonData;
using Ocean2City.ViewModel.Item;

namespace Ocean2City.Business.Interfaces
{
    public interface IItemManagerService
    {
        /// <summary>
        /// Get Specific Item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IResult GetItemById(string id);

        /// <summary>
        /// Get All Items.
        /// </summary>
        /// <returns></returns>
        IResult GetAllItems();

        /// <summary>
        /// Add New Item
        /// </summary>
        /// <param name="itemViewModel"></param>
        /// <returns></returns>
        IResult AddItem(ItemViewModel itemViewModel);

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="itemViewModel"></param>
        /// <returns></returns>
        IResult UpdateItem(ItemViewModel itemViewModel);
    }
}
