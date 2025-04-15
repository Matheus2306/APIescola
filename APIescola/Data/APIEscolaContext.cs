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

        // Método sobrescrito para configurar o modelo de dados usando o ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama o método base para garantir que as configurações padrão do IdentityDbContext sejam aplicadas
            base.OnModelCreating(modelBuilder);
            // Configurações personalizadas do modelo podem ser adicionadas aqui
        }
    }
}
