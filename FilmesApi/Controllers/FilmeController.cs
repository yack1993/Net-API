using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeService _filmeService;


        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult AdionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto read = _filmeService.AdicionarFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = read.Id }, read);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> read = _filmeService.RecuperarFilmes(classificacaoEtaria);
            if (read != null) return Ok(read);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            ReadFilmeDto read = _filmeService.RecuperaFilmePorId(id);

           if(read != null)
            {
                return Ok(read);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result result = _filmeService.AtualizaFilme(id, filmeDto);
            if(result.IsFailed)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverFilme(int id)
        {
            Result result = _filmeService.RemoverFilme(id);

            if(result != null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
