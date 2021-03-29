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

        // Aqui dentro da controller aluno eu referencio a tabela la no contexto, pois nao preciso mais criar aqui.. ele fica ouvindo tudo que tem no contexto(banco de dados)
        public AlunoController(SmartContext context)
        {
            // o uso do underline significa que quando instanciar uma controler AlunoController vai passar como parametro o contexto (SmartContext - services.AddDbContext)
            _context = context;
        }

        //rota normal api/aluno
        [HttpGet] 
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        //rota pesquisada por id api/aluno/id
        [HttpGet("byId")]
        
           public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a=>a.Id == id);
            if(aluno ==null) return BadRequest("O alune não foi encontrado!");
            return Ok(aluno);
        }

        //rota pesquisada por nome api/aluno/nome
        [HttpGet("ByName")]
        
        /*
            QueryString, tem que passar a variavel=nomeDaVariavel & variavel=nomeDaVariavel
        */
           public IActionResult GetByName(string nome, string Sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a=>
            a.Nome.Contains(nome) && a.Sobrenome.Contains(Sobrenome));
            if(aluno ==null) return BadRequest("O aluno não foi encontrado!");
            return Ok(aluno);
        }

    
         [HttpPost]
           public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
           public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a=>a.Id == id);
             if(alu == null) return BadRequest("Aluno nao encontrado");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
           public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a=>a.Id == id);
             if(alu == null) return BadRequest("Aluno nao encontrado");
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
           public IActionResult Delete(int id)
        {
             var aluno = _context.Alunos.FirstOrDefault(a=>a.Id == id);
             if(aluno == null) return BadRequest("Aluno nao encontrado");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok();
        }
    }
}