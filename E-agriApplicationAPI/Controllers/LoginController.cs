using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System;

namespace EcommerceApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
      

        public LoginController(ILoginServices loginServices)
        {
            this._loginServices = loginServices;
           
        }
        [HttpPost("Login")]
        public async  Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            bool validUser = await _loginServices.IsAuthenticated(loginViewModel);
            if (validUser)
            {
                return StatusCode(200,"Login Successfull");
            }
            else
            {
                return BadRequest(501);
            }
        }
        [HttpGet,Route("VerifyOTP")]
        public async Task< IActionResult> VerifyOTP(string otp)
        {
          var isvalidOTP= await _loginServices.verifyOTP(otp);
            if (isvalidOTP)
            {
                return StatusCode(200,"OTP verified");
            }
            else
                return Ok("Invalid OTP");
        }
        [HttpPost, Route("Add")]
        public async Task<IActionResult> Add(SignUpViewModel signUpViewModel)
        {
              var userAdded= await _loginServices.AddUser(signUpViewModel);
                if (userAdded)
                {
                    return StatusCode(200,"User Added");
                }
           else

                return BadRequest("Failed to add user");
        }

        [HttpGet,Route("GetAllUser")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _loginServices.GetAllUser();
            return Ok(users);
        }
        [HttpGet,Route("GetToken")]
        public async Task<ActionResult<string>> GetToken(string Username)
        {
           return await _loginServices.GetToken(Username);

        }
        [HttpDelete,Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string Username) 
        {

            await _loginServices.RemoveUser(Username);
            return StatusCode(200, "User Removed");
        }
        [HttpGet,Route("GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string Username)
        {
          var User= await _loginServices.GetUsersByUserName(Username);
            return Ok(User);
        }

        [HttpPut,Route("EditUser")]
        public async Task< IActionResult> UpdateUserByUserName(SignUpViewModel signUpViewModel)
        {
           
           await _loginServices.UpdateUser(signUpViewModel);
            return StatusCode(200, "User Details Updated sucessfully");
        }
        

    }
}
