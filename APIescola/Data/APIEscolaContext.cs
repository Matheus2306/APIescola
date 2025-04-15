using APIescola.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIescola.Data
{
    // Define a classe de contexto do Entity Framework para a aplicação APIescola
    public class APIEscolaContext : IdentityDbContext
    {
        // Construtor que recebe opções de configuração do contexto e passa para a classe base
        public APIEscolaContext(DbContextOptions<APIEscolaContext> options) : base(options)
        {
        }
        public DbSet<Aluno> Alunos { get; set; } // Propriedade DbSet para a entidade Aluno
        public DbSet<Curso> Cursos { get; set; } // Propriedade DbSet para a entidade Curso
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; } // Propriedade DbSet para a entidade Matricula
        // Método sobrescrito para configurar o modelo de dados usando o ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama o método base para garantir que as configurações padrão do IdentityDbContext sejam aplicadas
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().ToTable("Alunos"); // Define o nome da tabela para a entidade Aluno
            modelBuilder.Entity<Curso>().ToTable("Cursos"); // Define o nome da tabela para a entidade Curso
            modelBuilder.Entity<Turma>().ToTable("Turmas"); // Define o nome da tabela para a entidade Turma
            modelBuilder.Entity<Matricula>().ToTable("Matriculas"); // Define o nome da tabela para a entidade Matricula
            // Configurações personalizadas do modelo podem ser adicionadas aqui
        }
    }
}
