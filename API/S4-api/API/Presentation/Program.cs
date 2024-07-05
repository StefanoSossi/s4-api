using Microsoft.OpenApi.Models;
using s4.Configuration;
using s4.Data;
using s4.Logic.Managers;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models.Mapper;

namespace s4.Presentation
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .WithExposedHeaders("Authorization")
                );
            });
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddDbContext<S4DBContext>(); 
            builder.Services.AddTransient<IStudentManager, StudentsManager>();
            builder.Services.AddTransient<IClassesManager, ClassesManager>();
            builder.Services.AddTransient<IApplicationConfiguration, ApplicationConfiguration>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration.GetSection("S4APIInfo")["Name"],
                    Version = builder.Configuration.GetSection("S4APIInfo")["Version"],
                    Description = builder.Configuration.GetSection("S4APIInfo")["Description"],
                    Contact = new OpenApiContact
                    {
                        Name = builder.Configuration.GetSection("S4APIInfo")["Contact:Name"],
                        Email = builder.Configuration.GetSection("S4APIInfo")["Contact:Email"]
                    }
                });
            });


            var app = builder.Build();





            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAnyOrigin");
            app.MapControllers();

            app.Run();
        }
    }
}
