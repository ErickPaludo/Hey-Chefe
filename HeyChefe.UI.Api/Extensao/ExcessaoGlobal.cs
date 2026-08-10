using HeyChefe.Application.Exceções;
using HeyChefe.Domain.Validacoes.Categorias;
using HeyChefe.Domain.Validacoes.Codigo;
using HeyChefe.Domain.Validacoes.Cor;
using HeyChefe.Domain.Validacoes.Item;
using HeyChefe.Domain.Validacoes.Mesas;
using HeyChefe.Domain.Validacoes.Pedidos;
using HeyChefe.Domain.Validacoes.Segurança;
using HeyChefe.Domain.Validacoes.Usuarios;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HeyChefe.UI.Api.Excessao
{
    public class ExcessaoGlobal : IExceptionHandler
    {
        private readonly ILogger<ExcessaoGlobal> _logger;

        public ExcessaoGlobal(ILogger<ExcessaoGlobal> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, titulo) = exception switch
            {
                AutenticacaoValidacao => (401, exception.Message),
                UsuariosValidacao => (400, exception.Message),
                MesaValidacao => (400, exception.Message),
                ItemValidacao => (400, exception.Message),
                PedidoValidacao => (400, exception.Message),

                CategoriaValidacao => (400, exception.Message),

                CodigoValidacao => (400, exception.Message),
                CorValidacao => (400, exception.Message),

                ExceptionPermissoes => (403, exception.Message),
                ExceptionNaoEncontrado => (404, exception.Message),
                KeyNotFoundException =>
                    (404, "Recurso não encontrado"),

                InvalidOperationException =>
                    (409, "Conflito de operação"),

                ArgumentException =>
                    (400, "Requisição inválida"),

                _ =>
                    (500, "Erro interno do servidor")
            };

            if (status == 500)
            {
                _logger.LogError(
                    exception,
                    "Erro inesperado: {Mensagem}",
                    exception.Message);
            }
            else
            {
                _logger.LogWarning(
                    "Exceção tratada [{Status}]: {Mensagem}",
                    status,
                    exception.Message);
            }

            // Monta a resposta ProblemDetails
            var problem = new ProblemDetails
            {
                Status = status,
                Title = titulo,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = status;

            await httpContext.Response.WriteAsJsonAsync(
                problem,
                cancellationToken);

            return true;
        }
    }
}
