using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutService _lougoutService;

        public LogoutController(LogoutService lougoutService)
        {
            _lougoutService = lougoutService;
        }

        [HttpPost]
        public IActionResult DeslogaUsuario()
        {
            Result result = _lougoutService.DeslogaUsuario();
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }
    }
}
