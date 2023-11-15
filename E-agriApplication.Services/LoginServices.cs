using AutoMapper;
using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Mappings;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using EcommerceApplication.Services;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Services
{
    public interface ILoginServices
    {

       Task<bool> IsAuthenticated(LoginViewModel loginmodel);
        Task<bool> AddUser(SignUpViewModel signUpViewModel);
        Task RemoveUser(string UserName);
        Task UpdateUser(SignUpViewModel signUpViewModel);
        Task<List<Users>> GetAllUser();
        Task<Users> GetUsersByUserName(string UserName);
        Task<Users> GetUsersByEmail(string Email);
        bool CheckPassword(string Password, string ConfirmPassword);
        Task< bool> verifyOTP(string otp);
        Task<string> GetToken(string Username);


    }
    public class LoginServices : ILoginServices
    {

        public readonly ILoginRepository _loginRepository;
        public readonly ILoginMapper _mapper;

        private readonly ITokenHandler tokenHandler;
        private readonly IEmailServices emailServices;
        static string OTP;



        public LoginServices(ILoginRepository loginRepository, ILoginMapper mapper, ITokenHandler tokenHandler,IEmailServices emailServices)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
            this.tokenHandler = tokenHandler;
            this.emailServices = emailServices;
        }
        public async Task<bool> IsAuthenticated(LoginViewModel loginmodel)
        {
            Users validUser = await _loginRepository.GetUserByName(loginmodel.Username);

            if (loginmodel.Password == validUser.Password)
            {
                tokenHandler.CreateToken(validUser);
              //emailServices.SendEmailMessage(validUser.Email, validUser.Username);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> GetToken(string Username)
        {
            var validUser=await _loginRepository.GetUserByName(Username);
           var token= tokenHandler.CreateToken(validUser);
            return token;
        }

      
        public async Task<bool> verifyOTP(string otp)
        {
           
            if(OTP.Equals(otp))
                return true;
            else 
                return false;
        }
       
        public async Task<bool> AddUser(SignUpViewModel signUpViewModel)
        {
            var existingUser=await _loginRepository.GetUserByName(signUpViewModel.Username);
            var passwordCheck =CheckPassword(signUpViewModel.Password, signUpViewModel.ConfirmPassword);
            if (existingUser ==null && passwordCheck)
            {
                var user = _mapper.MapSignUpViewModelToUser(signUpViewModel);
                await _loginRepository.Add(user);
                OTP = GenerateOTP();
                emailServices.SendOTP(user.Email, user.Username, OTP);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckPassword(string Password, string ConfirmPassword)
        {
            if(Password.Equals(ConfirmPassword))
                return true;
            else return false;
        }
        public async Task RemoveUser(string Username)
        {
            var user =await _loginRepository.GetUserByName(Username);
         await   _loginRepository.Remove(user);
        }
        public  async Task UpdateUser(SignUpViewModel signUpViewModel)
        {
            var user =await _loginRepository.GetUserByName(signUpViewModel.Username);
            user.Password = signUpViewModel.Password;
            user.Username = signUpViewModel.Username;
            user.PhoneNumber = signUpViewModel.PhoneNumber;
            user.Email = signUpViewModel.Email;

           await _loginRepository.Update(user);
        }

        public async Task<List<Users>> GetAllUser()
        {
            var users = await _loginRepository.GetAllUser();
            return users;
        }
        public async Task<Users> GetUsersByUserName(string UserName)
        {
            var user = await _loginRepository.GetUserByName(UserName);
            return  user;
        }
        public async Task<Users> GetUsersByEmail(string Email)
        {
            var user = _loginRepository.GetUserByEmail(Email);
            return await user;

        }
        public String GenerateOTP()
        {
            Random generator = new Random();
            return generator.Next(100000, 999999).ToString();

        }

    }
}
