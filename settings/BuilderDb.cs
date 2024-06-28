using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using loja.data;
using System;

namespace loja.settings
{
    public class BuilderDb
    {
        public static WebApplicationBuilder GenerateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var mysqlVersion = new MySqlServerVersion(new Version(8, 0, 26));

            void AddMySql<TContext>(IServiceCollection services, MySqlServerVersion mysqlVersion) where TContext : DbContext
            {
                services.AddDbContext<TContext>(options =>
                    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), mysqlVersion));
            }
            AddMySql<LojaDbContext>(builder.Services, mysqlVersion);

            return builder;
        }
    }
}
