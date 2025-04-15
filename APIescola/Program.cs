using APIescola.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner de injeção de dependência.
builder.Services.AddDbContext<APIEscolaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Adiciona o serviço de CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// Adiciona o serviço de autenticação de usuário com Identity
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false; // Não requer confirmação de email para login
    options.SignIn.RequireConfirmedAccount = false; // Não requer confirmação de conta para login
    options.User.RequireUniqueEmail = true; // Requer email único para cada usuário
    options.Password.RequireNonAlphanumeric = false; // Não requer caracteres não alfanuméricos na senha
    options.Password.RequireLowercase = false; // Não requer letras minúsculas na senha
    options.Password.RequireUppercase = false; // Não requer letras maiúsculas na senha
    options.Password.RequireDigit = false; // Não requer dígitos na senha
    options.Password.RequiredLength = 4; // Define o comprimento mínimo da senha como 4 caracteres
})
.AddRoles<IdentityRole>() // Adiciona suporte a roles (papéis) de usuário
.AddEntityFrameworkStores<APIEscolaContext>() // Usa o contexto APIEscolaContext para armazenar dados de identidade
.AddDefaultTokenProviders(); // Adiciona provedores de token padrão para operações de segurança

// Configura o Swagger com Autenticação JWT Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "APIEscola", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
// Configura o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();

// Adiciona os serviços de autenticação e autorização
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
app.UseSwagger(); // Habilita o Swagger
app.UseSwaggerUI(); // Habilita a interface do usuário do Swagger

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS

app.UseCors("AllowAll");
app.UseAuthentication(); // habilita a authenticação
app.UseAuthorization(); // habilita a autorização

app.UseAuthorization(); // Adiciona middleware de autorização

app.MapControllers(); // Mapeia os controladores para os endpoints

app.MapGroup("/Usuario").MapIdentityApi<IdentityUser>(); //mapeia o grupo de endpoint de autenticação

app.Run(); // Executa o aplicativo
