using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// rota que a url caminhará para acessar a controller
    public class AlunoController : ControllerBase
    {
        //Referencia para ser guiado, onde colocaremos os news(objetos)
        public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno(){
                Id = 1,
                Nome = "Olorum",
                Sobrenome = "Deus",
                Telefone = "123456789"
            },
            new Aluno(){
                Id = 2,
                Nome = "Nilson",
                Sobrenome = "Fonseca",
                Telefone = "987654321"
            },
            new Aluno(){
                Id = 3,
                Nome = "Dj",
                Sobrenome = "Silva",
                Telefone = "987456321"
            }
        };
        public AlunoController(){}
               
          

        //rota normal api/aluno
       [HttpGet] 
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        //rota pesquisada por id api/aluno/id
        [HttpGet("byId")]
        
           public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a=>a.Id == id);
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
            var aluno = Alunos.FirstOrDefault(a=>
            a.Nome.Contains(nome) && a.Sobrenome.Contains(Sobrenome));
            if(aluno ==null) return BadRequest("O aluno não foi encontrado!");
            return Ok(aluno);
        }

    
         [HttpPost]
           public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
           public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
           public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
           public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}