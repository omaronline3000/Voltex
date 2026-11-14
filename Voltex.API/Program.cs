using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Voltex.API.Middlewares;
using Voltex.Core.Entities;
using Voltex.Core.Services;
using Voltex.Infrastructure;
using Voltex.Infrastructure.Data;
using Voltex.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Voltex.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // ========================================================> Add services to the container.


            // Custom Services Registeration
            builder.Services.InfrastuctureConfiguration(builder.Configuration);
            builder.Services.AddSingleton<IImageManagementService, ImageManagementService>();

            // Packages Services Registeration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(
                cfg => { },
                AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Built-in Services Registeration
            builder.Services.AddControllers();
            builder.Services.AddProblemDetails();
            builder.Services.AddCors( options =>
            {
                options.AddPolicy("Default Policy", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
                
            });
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                 RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? ".",
                        factory: partition => new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = 8,
                            Window = TimeSpan.FromSeconds(30),
                            QueueLimit = 0
                        })
                    );

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
               // options.AddSlidingWindowLimiter();
                //options.AddTokenBucketLimiter();
                //options.AddConcurrencyLimiter();
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(optios =>
            {
                optios.Cookie.Name = "token";
                optios.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();

            // ============================================> Configure the HTTP request pipeline. 

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<SecurityMiddleware>();

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
            app.UseCors("Default Policy");
            app.UseStatusCodePages();

            app.UseAuthorization();

            app.UseRateLimiter();
            app.MapControllers();

            app.Run();

        }
    }
}
