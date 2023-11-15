using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Repositories
{
    public interface IAddressRepository
    {
        Task Add(Address address);
        Task Delete(Address address);
        Task Update(Address address);
        Task<List<Address>> GetAll();
        Task<List<Address>> GetById(int Id);
        Task<Address> GetAddressById(int id);

    }
    public class AddressRepository:IAddressRepository
    {
        public readonly IUnitOfWork _unitOfWork;
        public AddressRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
       public async Task Add(Address address)
        {
            _unitOfWork.Add(address);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(Address address)
        {
            _unitOfWork.Remove(address);
            await _unitOfWork.CommitAsync();
        }
        public async Task Update(Address address)
        {
            _unitOfWork.Update(address);
            await _unitOfWork.CommitAsync();
        }
       public async Task<List<Address>> GetAll()
        {
            return await _unitOfWork.GetEntities<Address>().ToListAsync();
        }
       public  async Task<List<Address>> GetById(int id)
        {
            return await _unitOfWork.GetEntities<Address>().Where(p => p.UserId == id).ToListAsync();
        }
        public async Task<Address> GetAddressById(int id)
        {
            return await _unitOfWork.GetEntities<Address>().Where(p=>p.Id==id).AsNoTracking().FirstOrDefaultAsync();
        }


    }
}
