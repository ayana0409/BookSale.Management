using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public async Task SaveAsync(Order order)
        {
            await base.Create(order);
        }
    }
}
