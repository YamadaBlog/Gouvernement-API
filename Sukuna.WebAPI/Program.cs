using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Sukuna.DataAccess.Data;
using Sukuna.DataAccess;           // Pour acc�der � la classe Seed
using Sukuna.Business.Interfaces;
using Sukuna.Service;              // Assure-toi que tes services (EvenementService, etc.) soient dans ce namespace
using Microsoft.OpenApi.Models;

namespace Sukuna.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Ex�cution du Seed si l'argument "seeddata" est pass�
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                SeedData(host);
            }

            host.Run();
        }

        private static void SeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seed = services.GetRequiredService<Seed>();
                seed.SeedDataContext();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        // Configuration CORS
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowLocalhost3000",
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                                });
                        });

                        services.AddControllers();
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sukuna API", Version = "v1" });
                        });

                        // Enregistrement de la seed
                        services.AddTransient<Seed>();
                        // Enregistrement d'AutoMapper pour scanner tous les assemblies
                        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                        // Enregistrement des services de la couche Business
                        services.AddScoped<IEvenementService, EvenementService>();
                        services.AddScoped<IParticipationService, ParticipationService>();
                        services.AddScoped<ICommentaireService, CommentaireService>();
                        services.AddScoped<IModerateurService, ModerateurService>();
                        services.AddScoped<IUtilisateurService, UtilisateurService>();

                        // Enregistrement du DataContext via AddDbContext
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
                        app.UseCors("AllowLocalhost3000");
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
