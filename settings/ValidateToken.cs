    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

namespace loja.settings
{
    public struct ValidateToken
    {
        public static TokenValidationParameters GetValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes("gregregregregregregregregregregre");
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        public static bool ValidarToken(HttpContext context, out SecurityToken validatedToken)
        {
            validatedToken = null;
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.WriteAsync("Adicione um token").Wait();
                return false;
            }

            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, GetValidationParameters(), out validatedToken);
                return true;
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.WriteAsync("Token inválido").Wait();
                return false;
            }
        }
    }
}