using loja.services;
using loja.data;
using loja.models;
using loja.settings;

namespace loja.routes{

    public class UsuarioEndpoint
    {
        private UsuarioService usuarioService;

        public UsuarioEndpoint(LojaDbContext dbContext)
        {
            usuarioService = new UsuarioService(dbContext);
        }
        //### --- Método para acessar as rotas de requisição: -------------------------------------
        public void Rotas(WebApplication? app)
        {
            ArgumentNullException.ThrowIfNull(app);

            //## -- Endpoints de Usuarios: ----------------------------------------------------------------------
            // # - Endpoint/Rota pra listar todos os usuarios:
            app.MapGet("/usuarios", async (HttpContext context) =>
            {

                var usuarios = await usuarioService.GetAllUsuariosAsync();
                return Results.Ok(usuarios);
            });

            // # - Endpoint/Rota pra listar um usuario a parti de sua Id:
            app.MapGet("/usuarios/{id}", async (int id, HttpContext context) =>
            {
            
                var usuario = await usuarioService.GetUsuarioByIdAsync(id);
                if (usuario == null)
                {
                    return Results.NotFound($"\nUsuário com ID {id} não encontrado.");
                }
                return Results.Ok(usuario);
            });

            // # - Endpoint/Rota pra adicionar um usuario ao banco de dados:
            app.MapPost("/usuarios", async (Usuario usuario, HttpContext context) =>
            {
                
                await usuarioService.AddUsuarioAsync(usuario);
                return Results.Created($"/usuario/{usuario.Id}", usuario);
            });

            // # - Endpoint/Rota pra atualizar um usuario do banco de dados:
            app.MapPut("/usuarios/{id}", async (int id, Usuario usuario, HttpContext context) =>
            {
                
                await usuarioService.UpdateUsuarioAsync(id, usuario);
                return Results.Ok();
            });

            // # - Endpoint/Rota pra deletar um usuario do banco de dados a partir de sua Id:
            app.MapDelete("/usuarios/{id}", async (int id, HttpContext context) =>
            {

                await usuarioService.DeleteUsuarioAsync(id);
                return Results.Ok();
            });
        }
    }
}