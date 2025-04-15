using APIescola.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner de inje��o de depend�ncia.
builder.Services.AddDbContext<APIEscolaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Adiciona o servi�o de CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// Adiciona o servi�o de autentica��o de usu�rio com Identity
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false; // N�o requer confirma��o de email para login
    options.SignIn.RequireConfirmedAccount = false; // N�o requer confirma��o de conta para login
    options.User.RequireUniqueEmail = true; // Requer email �nico para cada usu�rio
    options.Password.RequireNonAlphanumeric = false; // N�o requer caracteres n�o alfanum�ricos na senha
    options.Password.RequireLowercase = false; // N�o requer letras min�sculas na senha
    options.Password.RequireUppercase = false; // N�o requer letras mai�sculas na senha
    options.Password.RequireDigit = false; // N�o requer d�gitos na senha
    options.Password.RequiredLength = 4; // Define o comprimento m�nimo da senha como 4 caracteres
})
.AddRoles<IdentityRole>() // Adiciona suporte a roles (pap�is) de usu�rio
.AddEntityFrameworkStores<APIEscolaContext>() // Usa o contexto APIEscolaContext para armazenar dados de identidade
.AddDefaultTokenProviders(); // Adiciona provedores de token padr�o para opera��es de seguran�a

// Configura o Swagger com Autentica��o JWT Bearer
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
// Configura o Swagger para documenta��o da API
builder.Services.AddEndpointsApiExplorer();

// Adiciona os servi�os de autentica��o e autoriza��o
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP.
app.UseSwagger(); // Habilita o Swagger
app.UseSwaggerUI(); // Habilita a interface do usu�rio do Swagger

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS

app.UseCors("AllowAll");
app.UseAuthentication(); // habilita a authentica��o
app.UseAuthorization(); // habilita a autoriza��o

app.UseAuthorization(); // Adiciona middleware de autoriza��o

app.MapControllers(); // Mapeia os controladores para os endpoints

app.MapGroup("/Usuario").MapIdentityApi<IdentityUser>(); //mapeia o grupo de endpoint de autentica��o

app.Run(); // Executa o aplicativo
