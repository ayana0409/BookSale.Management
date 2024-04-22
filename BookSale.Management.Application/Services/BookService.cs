using AutoMapper;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDatatable<BookDTO>> GetBookByPagination(RequestDatatable request)
        {
            int totalRecord = 0;
            IEnumerable<BookDTO> books;

            (books, totalRecord) = await _unitOfWork.BookRepository
                                .GetBookByPanigation<BookDTO>(request.SkipItems, request.PageSize, request.Keyword);



            return new ResponseDatatable<BookDTO>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = totalRecord,
                Data = books
            };
        }
    }
}
