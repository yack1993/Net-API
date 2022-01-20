using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("controller")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;
        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarSessao(CreateSessaoDto create)
        {
            Sessao sessao = _mapper.Map<Sessao>(create);
            _context.Add(sessao);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecupearSessaoPorId), new {Id = sessao.Id}, sessao);

        }

        [HttpGet("{id}")]
        public IActionResult RecupearSessaoPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if(sessao != null)
            {
                ReadSessaoDto readSessao = _mapper.Map<ReadSessaoDto>(sessao);

                return Ok(sessao);
            }

            return NotFound();
        }
    }
}
