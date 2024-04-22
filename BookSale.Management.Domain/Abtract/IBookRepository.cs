using BookSale.Managament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managament.Domain.Abtract
{
    public interface IBookRepository
    {
        Task<(IEnumerable<T>, int)> GetBookByPanigation<T>(int pageIndex, int pageSize, string keyword);
    }
}
