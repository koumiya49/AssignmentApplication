using EcommerceApplication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceApplication.Common.Data;
using EcommerceApplication.Common.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Repositories
{
    public interface ILoginRepository
    {
        Task Add(Users user);

        Task Remove(Users user);
        Task Update(Users user);
        Task<List<Users>> GetAllUser();
       
       Task< Users>GetUserByName(string userName);
       Task< Users> GetUserByEmail(string email);

    }
    public class LoginRepository:ILoginRepository
    {


        public readonly IUnitOfWork _unitOfWork;
       
        public LoginRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }
        public async Task Add(Users user)
        {
          _unitOfWork.Add(user);
          await _unitOfWork.CommitAsync();
        }
        public async Task Remove(Users user)
        {
            
            _unitOfWork.Remove(user);
            await _unitOfWork.CommitAsync();

        }
        public async Task Update(Users user)
        {
        
            _unitOfWork.Update(user);
            await _unitOfWork.CommitAsync();
        }
      
        public async Task<List<Users>> GetAllUser()
        {
            return  await _unitOfWork.GetEntities<Users>().ToListAsync();
        }
        
        public async Task<Users> GetUserByName(string userName)
        {
          return await _unitOfWork.GetEntities<Users>().Where(user => user.Username == userName).AsNoTracking().FirstOrDefaultAsync();
            
        }
        public async  Task<Users> GetUserByEmail(string email)
        {
            return await _unitOfWork.GetEntities<Users>().Where(user => user.Email == email).AsNoTracking().FirstOrDefaultAsync();
        }


    }
}
