using AspNet.Security.OAuth.Validation;
using AutoMapper;
using HappyCompany.Domain.Data;
using HappyCompany.Domain.Interface.Common;
using HappyCompany.Domain.Mapping.Profiles;
using HappyCompany.Domain.Models;
using HappyCompany.Domain.Models.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HappyCompany.API.Installers
{
    public class AppSettingInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            // Add UserManager To User as Identity User
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidAudience = jwtSettings.Site,
                ValidIssuer = jwtSettings.Site,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            // Mapping installer

            var mappingConfig = new MapperConfiguration(mc =>
            {
                //Add Your Profiles Below
                mc.AddProfile(new PaginationQueryToPaginationFilterProfile());
                mc.AddProfile<UpdateUserDtoToUserProfile>();
                mc.AddProfile<UserDtoToUserProfile>();
                mc.AddProfile<WarehouseToDto>();
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);


            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                policy =>
                {
                    //policy.WithOrigins("https://localhost:7052").WithMethods("PUT", "DELETE", "GET","POST");
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            // TODO: ADD Logg Configuration
        }
    }
}
