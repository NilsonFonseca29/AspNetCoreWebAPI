using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// rota que a url caminhará para acessar a controller
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context){
            _context = context;
        }
        //rota normal api/aluno
       [HttpGet] //Decorator do HTTP
       //Retornará um Ok
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        //rota pesquisada por id api/professor/byid?id=1
        [HttpGet("byId")]
        
           public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p=>p.Id == id);
            if(professor == null) return BadRequest("O professor não foi encontrado!");
            return Ok(professor);
        }

         //rota pesquisada por nome api/professor/byname?nome=Marta&sobrenome=Kent
        [HttpGet("ByName")]
        
        /*
            QueryString, tem que passar a variavel=nomeDaVariavel & variavel=nomeDaVariavel
        */
           public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(p=>p.Nome == nome);
            if(professor == null) return BadRequest("O professor não foi encontrado!");
            return Ok(professor);
        }
        
         [HttpPost]
           public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
           public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p=>p.Id == id);
             if(prof == null) return BadRequest("Professor nao encontrado");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
           public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p=>p.Id == id);
             if(prof == null) return BadRequest("Professore nao encontrado");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
           public IActionResult Delete(int id)
        {
             var professor = _context.Professores.FirstOrDefault(p=>p.Id == id);
             if(professor == null) return BadRequest("Professor nao encontrado");
            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}