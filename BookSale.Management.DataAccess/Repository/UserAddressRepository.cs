using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class UserAddressRepository : BaseRepository<UserAddress>, IUserAddressRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserAddressRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
        }
        public async Task<IEnumerable<UserAddress>> GetAllAddressByUserAsync(string userId)
        {
            return await GetAllAsync(x => x.UserId == userId && x.IsActive);
        }

        public async Task SaveAsync(UserAddress userAddress)
        {
            if (userAddress.Id == 0)
            {
                await CreateAsync(userAddress);
            }
            else
            {
                Update(userAddress);
            }
        }
    }
}
