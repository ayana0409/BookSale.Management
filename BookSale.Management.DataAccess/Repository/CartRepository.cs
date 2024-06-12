using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using Microsoft.IdentityModel.Tokens;

namespace BookSale.Management.DataAccess.Repository
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        public async Task SaveAsync(Cart cart)
        {
            if (cart.Id == 0)
            {
                await CreateAsync(cart);
            }
            else
            {
                Update(cart);
            }
        }
    }
}
