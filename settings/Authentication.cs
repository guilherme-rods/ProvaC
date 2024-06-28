using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace loja.settings
{
    public class Authentication
    {
        string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes("gregregregregregregregregregregre");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void TokenAuthentication(WebApplication? app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.MapPost("/login", async (HttpContext contex) =>
        {
            using var reader = new StreamReader(contex.Request.Body);
            var body = await reader.ReadToEndAsync();

            var json = JsonDocument.Parse(body);
            var email = json.RootElement.GetProperty("email").GetString();
            var senha = json.RootElement.GetProperty("senha").GetString();
            
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<loja.data.LojaDbContext>();
                var usuario = dbContext.Usuarios.SingleOrDefault(u => u.Email == email);
                
                

            if (usuario != null && usuario.Senha == senha && usuario.Email == email )
            {
                var token = GenerateToken();
                return token;
            }
            }
            return "senha invalida";
        });
        }

    }
}