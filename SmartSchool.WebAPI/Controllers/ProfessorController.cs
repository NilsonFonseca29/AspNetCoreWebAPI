using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// rota que a url caminhará para acessar a controller
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;          
        }
        //rota normal api/aluno
        [HttpGet] //Decorator do HTTP
                  //Retornará um Ok
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister(){
            return Ok(new ProfessorRegistrarDto());//Teste para ver se o RegisterAlunoDto está passando no Postman
        }
        
        //rota pesquisada por id api/professor/byid?id=1
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O professor não foi encontrado!");
            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
            //Antes de criar o Repositpory: _context.Add(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();     
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id,false);
            if (professor == null) return BadRequest("Professor nao encontrado");

            _mapper.Map(model, professor);
            _repo.Update(professor);

            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor nao encontrado");

            _repo.Delete(professor);
            if(_repo.SaveChanges())
            {            
            return Ok(professor);
            }
        return BadRequest("Professor não deletado");
        }
    }
}