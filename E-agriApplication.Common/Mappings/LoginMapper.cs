using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Common.Mappings
{
    public interface ILoginMapper
    {
        Users MapSignUpViewModelToUser(SignUpViewModel signUpViewModel);
    }

    public class LoginMapper : ILoginMapper
    {
        public Users MapSignUpViewModelToUser(SignUpViewModel signUpViewModel)
        {
            return new Users
            {
                Username = signUpViewModel.Username,
                Password = signUpViewModel.Password,
                Email = signUpViewModel.Email,
                PhoneNumber = signUpViewModel.PhoneNumber,
            };
        }
    }


}
