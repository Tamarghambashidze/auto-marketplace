using FinalProject.Data;
using FinalProject.Interfaces;
using FinalProject.Repositories;
using FinalProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinalProject.Extensions
{
    public static class ServiceExtensions
    {
        private static void AddServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarDetailsRepository, CarDetailsRepository>();
            services.AddScoped<ISortCarsRepository, SortCarsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFavouritesRepository, FavouritesRepository>();
            services.AddScoped<IBuyCarRepository, BuyCarRepository>();

            // Services
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarDetailsService, CarDetailsService>();
            services.AddScoped<IMapCarsService, MapCarsService>();
            services.AddScoped<ISortCarsService, SortCarsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMapUsersService, MapUsersService>();
            services.AddScoped<IFavouritesService, FavouritesService>();
            services.AddScoped<IBuyCarService, BuyCarService>();
            services.AddScoped<IJwtService, JwtService>();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.ConfigureAuthentication(configuration);
            services.ConfigureCors(configuration);
            services.ConfigureDatabase(configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddServices();
        }

        private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("AppSettings");
            var jwtSecret = jwtConfig["JWTSecret"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new ArgumentNullException("JWTSecret is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =
                x.DefaultChallengeScheme =
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(y =>
            {
                y.SaveToken = false;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:JWTSecret"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AppSettings:Issuer"], 
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:Audience"], 
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero  
                };
            });
        }

            private static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetValue<string>("allowedOrigins");
            if (string.IsNullOrEmpty(allowedOrigins))
            {
                throw new ArgumentNullException("allowedOrigins configuration is missing.");
            }

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("DefaultConnection is not configured properly.");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
