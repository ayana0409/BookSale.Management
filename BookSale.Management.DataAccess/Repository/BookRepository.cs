using BookSale.Managament.Domain.Abtract;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
