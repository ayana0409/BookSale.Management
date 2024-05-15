using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.User;
using BookSale.Management.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserAddressDTO>> GetUserAddressForSite(string userId)
        {
            var address = await _unitOfWork.UserAddressRepository.GetUserAddress(userId);

            return _mapper.Map<IEnumerable<UserAddressDTO>>(address);
        }

        public async Task<int> SaveAsync(UserAddressDTO userAddressDTO)
        {
            try
            {
                var address = _mapper.Map<UserAddress>(userAddressDTO);

                await _unitOfWork.UserAddressRepository.Save(address);

                await _unitOfWork.SaveChangeAsync();

                return address.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
