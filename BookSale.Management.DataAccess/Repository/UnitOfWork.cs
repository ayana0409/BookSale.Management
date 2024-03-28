using BookSale.Managament.Domain.Abtract;
using BookSale.Management.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IGenreRepository? _genreRepository;
        private IBookRepository? _bookRepository;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_applicationDbContext);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_applicationDbContext);

        public async Task SaveChangeAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_applicationDbContext != null)
            {
                _applicationDbContext.Dispose();
            }
        }
    }
}
