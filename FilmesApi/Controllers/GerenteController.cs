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
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private FilmeContext _contex;
        private IMapper _mapper;

        public GerenteController(FilmeContext context, IMapper mapper)
        {
            _contex = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _contex.Gerentes.Add(gerente);
            _contex.SaveChanges();
            return CreatedAtAction(nameof(RecuperarGerentePorId), new { Id = gerente.Id}, gerente);
        }

        [HttpGet]
        public IActionResult RecuperarGerentePorId(int id)
        {
            Gerente gerente = _contex.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            
            if(gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

                return Ok(gerenteDto);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGerente(int id)
        {
            Gerente gerente = _contex.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if(gerente == null)
            {
                return NotFound();
            }

            _contex.Remove(gerente);
            _contex.SaveChanges();
            return NoContent();
        }
    }
}
