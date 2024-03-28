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
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
