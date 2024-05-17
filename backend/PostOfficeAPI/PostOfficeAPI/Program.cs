
using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.Contracts.Repos;
using PostOfficeAPI.Contracts.Services;
using PostOfficeAPI.Data;
using PostOfficeAPI.Repos;
using PostOfficeAPI.Services;

namespace PostOfficeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PostOfficeOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IParcelRepo, ParcelRepo>(); 
            builder.Services.AddScoped<IParcelService, ParcelService>();
            builder.Services.AddScoped<IBagRepo, BagRepo>();
            builder.Services.AddScoped<IBagService, BagService>();
            builder.Services.AddScoped<IShipmentRepo, ShipmentRepo>();
            builder.Services.AddScoped<IShipmentService, ShipmentService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("PostOfficeOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}