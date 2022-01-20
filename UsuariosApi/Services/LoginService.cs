using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signManager = signInManager;
            _tokenService = tokenService;
        }


        public Result LogaUusario(LoginRequest login)
        {
            var result = _signManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (result.Result.Succeeded)
            {
                var identyUser = _signManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == login.UserName.ToUpper());

                Token token = _tokenService.CreateToken(identyUser);
                return Result.Ok().WithSuccess(token.Value);
            } 
                

            return Result.Fail("Não foi possivel fazer o login");

        }
    }
}
