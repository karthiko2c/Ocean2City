using Microsoft.Extensions.Configuration;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;

namespace Ocean2City.Data.Logic
{
    public class ItemRepository: Repository<Item>, IItemRepository
    {
        public IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the ItemRepository
    /// </summary>
    /// <param name="configuration"></param>
    public ItemRepository(IConfiguration configuration) : base(configuration, "item")
        {
        _configuration = configuration;
    }
}
}
