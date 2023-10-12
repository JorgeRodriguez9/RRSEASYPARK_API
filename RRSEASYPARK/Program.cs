using Microsoft.EntityFrameworkCore;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Service;
using RRSEASYPARK.Utilities;
using RRSEASYPARK.Models.Dto;
using AutoMapper;
using System.Reflection;
using Microsoft.OpenApi.Models;
using RRSEASYPARK.Models.Common;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "API RRSEASYPARK", 
                    Version = "v1" ,
                    Description = "This documentation shows the different API methods used by our RRS EASYPARK application, " +
                    "to be able to make HTTP calls from the front to the backend, implementing more security in data handling." +
                    " We have different API's, for the models of: City, clientParkingLot, ParkingLot, ParkingLot, Reservation, " +
                    "User, TypeVehicle and PropietaryPark.",
                    Contact = new OpenApiContact
                    {
                        Name = "Our GITHUB",
                        Url = new Uri("https://github.com/JorgeRodriguez9/RRSEASYPARK_API"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<RRSEASYPARKContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //token
            var appSettingsConfiguration = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingsConfiguration);

            //jwt

            var appSettings = appSettingsConfiguration.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.secret);//obtiene el secreto y lo pone en una matriz de bytes
            builder.Services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}