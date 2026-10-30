using Voltex.Infrastructure;

namespace Voltex.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.

            builder.Services.AddControllers();
            // OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Infrastructur Configuration
            builder.Services.InfrastuctureConfiguration(builder.Configuration);

            // Register AutoMapper
            builder.Services.AddAutoMapper(
                ctg => { },
                AppDomain.CurrentDomain.GetAssemblies()
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
