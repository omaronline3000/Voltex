using System.Diagnostics;
using System.Text.Json.Serialization;
using Voltex.Core.Services;
using Voltex.Infrastructure;
using Voltex.Infrastructure.Services;

namespace Voltex.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            /* Add services to the container.*/

            builder.Services.AddControllers();
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                //});


            // OpenAPI Documentation 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Infrastructur Configuration
            builder.Services.InfrastuctureConfiguration(builder.Configuration);

            // Register AutoMapper
            builder.Services.AddAutoMapper(
                cfg => { },
                AppDomain.CurrentDomain.GetAssemblies()
                );


            // Register ProblemDetails Service
            builder.Services.AddProblemDetails();

            // Register ImageService
            builder.Services.AddSingleton<IImageManagementService, ImageManagementService>();

            var app = builder.Build();

            /* Configure the HTTP request pipeline. */

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(config =>
            {
                    config.Run(async contex =>
                    {
                        contex.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        contex.Response.ContentType = "application/json";

                        await contex.Response.WriteAsJsonAsync(new
                        {
                            status = StatusCodes.Status500InternalServerError,
                            message = $"Internal Error, Plese Contact the Developer {builder.Configuration["Developer Mail"]}.",
                            traceid = contex.TraceIdentifier
                        });
                    });
            });

            app.UseStatusCodePages();

            app.UseAuthorization();


           app.MapControllers();

           app.Run();
            }
    }
    }
