using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private CadastroService _cadastraService;

        public UsuariosController(CadastroService cadastroService)
        {
            _cadastraService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastroUusario(CreateUsuarioDto createUsuario)
        {
            Result result = _cadastraService.CadastraUsuario(createUsuario);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest contaRequest)
        {
            Result result = _cadastraService.AtivaContaUsuario(contaRequest);
            if (result.IsFailed)
            {
                return StatusCode(500);
            }
            return Ok(result.Successes);
        }
    }
}
