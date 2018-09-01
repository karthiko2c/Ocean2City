using Microsoft.Extensions.Configuration;
using Ocean2City.Data.Interfaces;
using Ocean2City.Entity.Models;

namespace Ocean2City.Data.Logic
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the OrderDetailRepository
        /// </summary>
        /// <param name="configuration"></param>
        public OrderDetailRepository(IConfiguration configuration) : base(configuration, "order")
        {
            _configuration = configuration;
        }
    }
}
