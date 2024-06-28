public class Program{
    public static void Main(string[] args)
    {

        var builder = loja.settings.BuilderDb.GenerateBuilder(args);
        var app = builder.Build();
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<loja.data.LojaDbContext>();

        var auth = new loja.settings.Authentication();
        var usuarioEndpoint = new loja.routes.UsuarioEndpoint(dbContext);
        var ServicoEndpoint = new loja.routes.ServicoEndpoint(dbContext);
      

        auth.TokenAuthentication(app);
        usuarioEndpoint.Rotas(app);
        ServicoEndpoint.Rotas(app);       

        app.Run();
  }
}