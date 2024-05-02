using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Managament.Domain.Enums;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.ViewModal;
using BookSale.Management.DataAccess.Repository;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly ICommonService _commonService;
        private readonly IImageService _imageService;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, ICommonService commonService, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commonService = commonService;
            _imageService = imageService;
        }

        public async Task<ResponseDatatable<BookDTO>> GetBookByPagination(RequestDatatable request)
        {
            int totalRecord = 0;
            IEnumerable<BookDTO> books;
            Func<BookDTO, object>? orderBy = null;

            (books, totalRecord) = await _unitOfWork.BookRepository
                                .GetBookByPanigation<BookDTO>(request.SkipItems, request.PageSize, request.Keyword);

            switch (request.OrderColunm)
            {
                case 1:
                    orderBy = b => b.GenreName;
                    break;
                case 2:
                    orderBy = b => b.Code;
                    break;
                case 3:
                    orderBy = b => b.Title;
                    break;
                case 4:
                    orderBy = b => b.Available;
                    break;
                case 5:
                    orderBy = b => b.Price;
                    break;
                case 6:
                    orderBy = b => b.Publisher;
                    break;
                case 7:
                    orderBy = b => b.Author;
                    break;
                case 8:
                    orderBy = b => b.CreatedOn;
                    break;
                default:
                    break;
            }

            if (orderBy != null)
            {
                if (request.OrderType == "asc")
                {
                    books = books.OrderBy(orderBy);
                }
                else if (request.OrderType == "desc")
                {
                    books = books.OrderByDescending(orderBy);
                }
            }

            return new ResponseDatatable<BookDTO>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = totalRecord,
                Data = books
            };
        }
        public async Task<BookViewModal> GetById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);

            return _mapper.Map<BookViewModal>(book);
        }

        public async Task<ResponseModel> SaveAsync(BookViewModal bookVM)
        {
            Book book = _mapper.Map<Book>(bookVM);

            if (book.Id == 0)
            {
                book.IsActive = true;
                book.Code = bookVM.Code;
            }

            book.CreatedOn = DateTime.Now;
            var result = await _unitOfWork.BookRepository.Save(book);

            await _unitOfWork.SaveChangeAsync();

            var exisBook = await _unitOfWork.BookRepository.GetByCode(bookVM.Code);

            if (result && bookVM.Image is not null && exisBook is not null)
            {
                await _imageService.SaveImage(new List<IFormFile> { bookVM.Image }, "images/book", $"{exisBook.Id}.png");
            }

            var actionType = bookVM.Id == 0 ? ActionType.Insert : ActionType.Update;
            var successMessage = $"{(book.Id == 0 ? "Insert" : "Update")} successful.";
            var failureMessage = $"{(book.Id == 0 ? "Insert" : "Update")} failed.";

            return new ResponseModel
            {
                Action = actionType,
                Message = result ? successMessage : failureMessage,
                Status = result,
            };
        }

        public async Task<string> GenerateCodeAsync(int number = 10)
        {
            string code;

            while (true)
            {
                code = _commonService.GenerateRandomCode(number);
                var bookCode = await _unitOfWork.BookRepository.GetByCode(code);

                if (bookCode is null)
                    break;
            }

            return code;
        }

        public async Task<ResponseModel> DeleteAsync(int id)
        {
            Book? book = await _unitOfWork.BookRepository.GetById(id);
            var result = false;

            if (book is not null)
            {
                book.IsActive = false;
                result = await _unitOfWork.BookRepository.Save(book);
                await _unitOfWork.SaveChangeAsync();
            }

            return new ResponseModel
            {
                Action = ActionType.Delete,
                Message = result ? "Delete successful" : "Delete failed",
                Status = result,
            };
        }

        public async Task<BookForSiteDTO> GetBookForSiteAsync(int genreId, int pageIndex, int pageSize = 12)
        {
            int totalRecord;
            IEnumerable<Book> books;

            (books, totalRecord) = await _unitOfWork.BookRepository.GetBookForSite(genreId, pageIndex, pageSize);

            var bookDTOs = _mapper.Map<IEnumerable<BookDTO>>(books);

            int currentDisplayingItem = totalRecord - (pageIndex * pageSize) <= 0 ? totalRecord : pageIndex * pageSize;

            bool isDisableBtn = totalRecord - (pageIndex * pageSize) <= 0 ? true : false;

            double prosessingValue = (pageIndex * pageSize * 100) / totalRecord;

            return new BookForSiteDTO
            {
                Books = bookDTOs,
                TotalRecord = totalRecord,
                CurrentRecord = currentDisplayingItem,
                IsDisable = isDisableBtn,
                ProressingValue = prosessingValue
            };

        }
    }
}
