using AutoMapper;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest =>dest.Nome, //o Dto
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")//pegando e mandando pro dto
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                ); //Toda vez que trabalhei com aluno, chamarei o AlunoDto para mostrar nas chamadas
            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno,AlunoRegistrarDto>().ReverseMap();

            //Professor   
            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest =>dest.Nome, //o Dto
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")//pegando e mandando pro dto
                );      
            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor,ProfessorRegistrarDto>().ReverseMap();

        }
    }
}