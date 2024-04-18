using BookSale.Managament.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managament.Domain.Abtract
{
    public interface IGenreRepository
    {
        Task<bool> AddAsync(Genre genre);
        Task<IEnumerable<Genre>> GetAllGenre();
        Task<Genre> GetById(int id);
        Task<bool> UpdateAsync(Genre genre);
    }
}
