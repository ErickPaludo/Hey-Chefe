using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Interfaces.Repositorios.Segurança;
using HeyChefe.Infra.Data;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Segurança;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeyChefe.Infra.IoC
{
    public static class InjectInfraestructure
    {
        public static void ConfigurarInjecaoInfraestrutura(this IServiceCollection services, IConfiguration configure)
        {
            services.AddDbContext<AppDbContext>(op => op.UseSqlServer(configure.GetConnectionString("SqlServer"), b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))); //variavel b diz aonde gerar as migrations, pois o contexto esta em outro projeto
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAutenticacoesRepositorio, AutenticacoesRepositorio>();
        }
    }
}
