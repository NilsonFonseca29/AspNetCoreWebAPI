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
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        // Aqui dentro da controller aluno eu referencio a tabela la no contexto, pois nao preciso mais criar aqui.. ele fica ouvindo tudo que tem no contexto(banco de dados)
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            // o uso do underline significa que quando instanciar uma controler AlunoController vai passar como parametro o contexto (SmartContext - services.AddDbContext)
        }

        //rota normal api/aluno
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));//Aqui mapeia o alunoController e alunoDto
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister(){
            return Ok(new AlunoRegistrarDto());//Teste para ver se o RegisterAlunoDto está passando no Postman
        }

        //rota pesquisada por id api/aluno/id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O alune não foi encontrado!");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado");
            //Antes de criar o Repositpory: _context.Add(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();     
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {

            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);
            _repo.Update(aluno);

            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não atualizado");
            //Antes de criar o Repositpory: _context.Update(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não atualizado");
            //Antes de criar o Repositpory: _context.Update(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno nao encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não deletado");
            //Antes de criar o Repositpory: _context.Delete(aluno);
            //Antes de criar o Repositpory: _context.SaveChanges();
        }
    }
}