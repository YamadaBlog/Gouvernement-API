using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Sukuna.DataAccess.Data;
using Sukuna.DataAccess;           // Pour accéder à la classe Seed
using Sukuna.Business.Interfaces;
using Sukuna.Service;              // Vos services (EvenementService, etc.)
using Microsoft.OpenApi.Models;

namespace Sukuna.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Exécution du Seed si l'argument "seeddata" est passé
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                SeedData(host);
            }

            host.Run();
        }

        private static void SeedData(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var seed = services.GetRequiredService<Seed>();
            seed.SeedDataContext();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        // 1. Configuration CORS
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", builder =>
                            {
                                builder.AllowAnyOrigin()
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                            });
                        });

                        // 2. Configuration JWT
                        var jwtSection = hostContext.Configuration.GetSection("Jwt");
                        var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);
                        services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = jwtSection["Issuer"],
                                ValidAudience = jwtSection["Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(key)
                            };
                        });

                        // 3. Controllers & Swagger
                        services.AddControllers()
                                .AddJsonOptions(opts => { /* config JSON si besoin */ });
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sukuna API", Version = "v1" });
                        });

                        // 4. Seed, AutoMapper, Services, DbContext
                        services.AddTransient<Seed>();
                        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                        services.AddScoped<IEvenementService, EvenementService>();
                        services.AddScoped<IParticipationService, ParticipationService>();
                        services.AddScoped<ICommentaireService, CommentaireService>();
                        services.AddScoped<IModerateurService, ModerateurService>();
                        services.AddScoped<IUtilisateurService, UtilisateurService>();

                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                            options.EnableSensitiveDataLogging();
                        });
                    })
                    .Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseSwagger();
                            app.UseSwaggerUI(c =>
                            {
                                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sukuna API V1");
                            });
                        }

                        app.UseHttpsRedirection();
                        app.UseRouting();

                        // 5. Authentication & Authorization
                        app.UseCors("AllowAll");
                        app.UseAuthentication();    // <-- ajouté
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
