using loja.services;
using loja.data;
using loja.models;
using loja.settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using System;

namespace loja.routes
{
    public class ServicoEndpoint
    {
        private ServicoService servicoService;

        public ServicoEndpoint(LojaDbContext dbContext)
        {
            servicoService = new ServicoService(dbContext);
        }

        public void Rotas(IEndpointRouteBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            // # - Endpoint/Rota para listar todos os serviços:
            app.MapGet("/servicos", async (HttpContext context) =>
            {
                // if (!ValidateToken.ValidarToken(context, out _)) return;

                var servicos = await servicoService.GetAllServicosAsync();
                return Results.Ok(servicos);
            });

            // # - Endpoint/Rota para obter um serviço por nome:
            app.MapGet("/servicos/{id}", async (int id, HttpContext context) =>
            {
                
                var servico = await servicoService.GetServicoByIdAsync(id);
                if (servico == null)
                {
                    return Results.NotFound($"\nServiço com id '{id}' não encontrado.");
                }
                return Results.Ok(servico);
            });

            // # - Endpoint/Rota para adicionar um serviço ao banco de dados:
            app.MapPost("/servicos", async (Servico servico, HttpContext context) =>
            {
                await servicoService.AddServicoAsync(servico);
                return Results.Created($"/servicos/{servico.Nome}", servico);
            });

            // # - Endpoint/Rota para atualizar um serviço do banco de dados:
            app.MapPut("/servicos/{id}", async (int id, Servico servico, HttpContext context) =>
            {
                await servicoService.UpdateServicoAsync(id, servico);
                return Results.Ok();
            });

            // # - Endpoint/Rota para deletar um serviço do banco de dados por Id:
            app.MapDelete("/servicos/{id}", async (int id, HttpContext context) =>
            {
                await servicoService.DeleteServicoAsync(id);
                return Results.Ok();
            });
        }
    }
}
