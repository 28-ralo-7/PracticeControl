using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Repositories;
using PracticeControl.WebAPI.Services;
using System.Data.Common;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PracticeControl.WebAPI.Database;
using System.Text.Json.Serialization;

namespace PracticeControl.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAuthorization();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IAuthRepository, AuthRepository>();

            builder.Services.AddTransient<IGetService, GetService>();
            builder.Services.AddTransient<IGetRepository, GetRepository>();

            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();

            builder.Services.AddTransient<IDeleteService, DeleteService>();
            builder.Services.AddTransient<IDeleteRepository, DeleteRepository>();

            builder.Services.AddTransient<IPutService, PutService>();
            builder.Services.AddTransient<IPutRepository, PutRepository>();

            builder.Services.AddTransient<ProductionPracticeControlContext>();

           builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("SecretKey").Value!))
                };
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}