using Microsoft.AspNetCore.Mvc;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// rota que a url caminhará para acessar a controller
    public class ProfessorController : ControllerBase
    {
        public ProfessorController(){}
       [HttpGet] //Decorator do HTTP
       //Retornará um Ok
        public IActionResult Get()
        {
            return Ok("Professores: Marta, Paula, Lucas, Rafa");
        }
    }
}