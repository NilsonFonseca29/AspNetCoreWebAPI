using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    /*
    *Herdar do entity Framework dotnet *add package Microsoft.*EntityFrameworkCore.SqlServer
    */
    public class SmartContext : DbContext 
    {
        //pega o que passou da base e inicializa no SmartContext tbm ir no startup.cs e deixar o click em cima do que está passando
        public SmartContext(DbContextOptions<SmartContext> options) : base(options) 
        {
            
        }
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Professor> Professores { get; set; }
        
        public DbSet<Disciplina> Disciplinas { get; set; }
        
        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }

        //Quando estiver criando o banco de dados chama o OnModelCreating
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<AlunoDisciplina>()
            .HasKey(AD => new {AD.AlunoId, AD.DisciplinaId});//Métodos de muitos para muitos entre aluno e disciplina(AlunoDisciplina)
       
        builder.Entity<Professor>()
                .HasData(new List<Professor>(){
                    new Professor(1, "Laura"),
                    new Professor(2, "Roberta"),
                    new Professor(3, "Ronaldo"),
                    new Professor(4, "Rodrigo"),
                    new Professor(5, "Alexandra"),
                });
            
            builder.Entity<Disciplina>()
                .HasData(new List<Disciplina>{
                    new Disciplina(1, "Matemática", 1),
                    new Disciplina(2, "Física", 2),
                    new Disciplina(3, "Português", 3),
                    new Disciplina(4, "Inglês", 4),
                    new Disciplina(5, "Programação", 5)
                });
            
            builder.Entity<Aluno>()
                .HasData(new List<Aluno>(){
                    new Aluno(1, "Marta", "Kent", "33225555"),
                    new Aluno(2, "Paula", "Isabela", "3354288"),
                    new Aluno(3, "Laura", "Antonia", "55668899"),
                    new Aluno(4, "Luiza", "Maria", "6565659"),
                    new Aluno(5, "Lucas", "Machado", "565685415"),
                    new Aluno(6, "Pedro", "Alvares", "456454545"),
                    new Aluno(7, "Paulo", "José", "9874512")
                });

            builder.Entity<AlunoDisciplina>()
                .HasData(new List<AlunoDisciplina>() {
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 5 }
                });
        }
    }
}