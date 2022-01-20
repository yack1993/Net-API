using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LoginUsuario(LoginRequest login)
        {
            Result result = _loginService.LogaUusario(login);

            if (result.IsFailed)
                return Unauthorized(result.Errors);

            return Ok(result.Successes); //https://jwt.io/, analisa os dados do token


        }
    }
}
