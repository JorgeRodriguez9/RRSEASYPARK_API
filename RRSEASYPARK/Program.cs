using Microsoft.EntityFrameworkCore;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Service;
using RRSEASYPARK.Utilities;
using RRSEASYPARK.Models.Dto;
using AutoMapper;


namespace RRSEASYPARK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string MiCor = "MiCor";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<RRSEASYPARKContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IClientParkingLotService, ClientParkingLotService>();
            builder.Services.AddScoped<IParkingLotService, ParkingLotService>();
            builder.Services.AddScoped<IPropietaryParkService, PropietaryParkService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IRolService, RolService>();
            builder.Services.AddScoped<ITypeVehiculeService, TypeVehiculeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IValueService, ValueService>();

            //Use the Utilities folder
            builder.Services.AddAutoMapper(typeof(AutoMaperProfile));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MiCor, builder =>
                {
                    builder.WithOrigins("*"); // permito que sea desde cualquier origen
                    builder.WithHeaders("*"); // permito desde cualquier encabezado
                    builder.WithMethods("*"); // permito desde cualquier metodo
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(MiCor);
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}