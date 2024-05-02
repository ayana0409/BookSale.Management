using BookSale.Managament.Domain.Abtract;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;

namespace BookSale.Management.DataAccess.Repository {

    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllActiveGenre()
        {
            return await _applicationDbContext.Genre.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return  await GetSigleAsync(x => x.Id == id);
        }

        public async Task<bool> AddAsync(Genre genre)
        {
            try
            {
                await _applicationDbContext.Genre.AddAsync(genre);

                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Genre genre)
        {
            try
            {
                var existGenre = _applicationDbContext.Genre.Find(genre.Id);
                if (existGenre != null)
                {
                    existGenre.Name = genre.Name;
                    existGenre.IsActive = genre.IsActive;
                    await _applicationDbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public async Task<Genre> FindById(int id)
        {
            return await _applicationDbContext.Genre.FirstAsync(x => x.Id == id);
        }

        public new IQueryable<Genre> Table => base.Table;
    }
}
