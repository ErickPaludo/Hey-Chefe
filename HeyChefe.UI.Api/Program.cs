using HeyChefe.Infra.IoC;
using HeyChefe.UI.Api.Excessao;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigurarInjecaoSwagger(builder.Configuration);
builder.Services.ConfigurarInjecaoPassword(builder.Configuration);
builder.Services.ConfigurarInjecaoAutenticaoJWT(builder.Configuration);
builder.Services.ConfigurarInjecaoInfraestrutura(builder.Configuration);
builder.Services.ConfigurarInjecaoServicos();
builder.Services.ConfigurarInjecaoBibliotecas();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Host.UseSerilog();

builder.Services.AddExceptionHandler<ExcessaoGlobal>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
