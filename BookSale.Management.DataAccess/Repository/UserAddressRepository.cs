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
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserAddressRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<UserAddress>> GetUserAddress(string userId)
        {
            return await _applicationDbContext.UserAddress.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
