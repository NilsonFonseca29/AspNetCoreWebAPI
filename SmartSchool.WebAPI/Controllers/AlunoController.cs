using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// rota que a url caminhará para acessar a controller
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;
        public readonly IRepository _repo;

        // Aqui dentro da controller aluno eu referencio a tabela la no contexto, pois nao preciso mais criar aqui.. ele fica ouvindo tudo que tem no contexto(banco de dados)
        public AlunoController(IRepository repo)
        {
            _repo = repo;
            // o uso do underline significa que quando instanciar uma controler AlunoController vai passar como parametro o contexto (SmartContext - services.AddDbContext)
        }

        //rota normal api/aluno
        [HttpGet]
        public IActionResult Get()
        {
           var result = _repo.GetAllAlunos(true);
           return Ok(result);
        }

        //rota pesquisada por id api/aluno/id
        [HttpGet("byId/{id}")]

        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O alune não foi encontrado!");
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado");
            //Antes de criar o Repositpory: _context.Add(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();     
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {

            var alu = _repo.GetAlunoById(id);
            if(_repo.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado");
            //Antes de criar o Repositpory: _context.Update(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);
            if(_repo.SaveChanges()){
                return Ok(alu);
            }
            return BadRequest("Aluno não cadastrado");
            //Antes de criar o Repositpory: _context.Update(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno nao encontrado");
            
            _repo.Delete(alu);
            if(_repo.SaveChanges()){
                return Ok(alu);
            }
            return BadRequest("Aluno não cadastrado");
            //Antes de criar o Repositpory: _context.Delete(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();
        }
    }
}