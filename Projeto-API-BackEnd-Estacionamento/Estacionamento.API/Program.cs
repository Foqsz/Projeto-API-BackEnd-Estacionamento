using Microsoft.EntityFrameworkCore;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner

builder.Services.AddControllers();

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer(); // Para explorar os endpoints na documenta��o
builder.Services.AddSwaggerGen(); // Gera a documenta��o Swagger

// Configura��o da conex�o com o banco de dados
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EstacionamentoDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Registrando os servi�os de reposit�rio e servi�o
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<IVeiculosRepository, VeiculosRepository>();

builder.Services.AddScoped<IEmpresasService, EmpresasService>();
builder.Services.AddScoped<IVeiculosService, VeiculosService>();
builder.Services.AddScoped<IMovimentacaoService, MovimentacaoService>();

// Configura��o do AutoMapper
builder.Services.AddAutoMapper(typeof(EmpresaDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(VeiculosDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(MovimentacaoEstacionamentoDTOMappingProfile));

var app = builder.Build();

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Ativa o Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
