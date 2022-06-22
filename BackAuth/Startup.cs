using BackAuth.Data;
using BackAuth.Data.Entity;
using BackAuth.Data.Interface;
using BackAuth.Data.Service;
using BackAuth.Model.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackAuth
{
    public class Startup
    {
        private readonly string _MyCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            var apiConfiguration = new APIConfiguration(Configuration.GetConnectionString("MySqlConnection"));
            var sectionEmail = Configuration.GetSection("EmailConfiguration");
            services.Configure<EmailSettings>(sectionEmail);

            var settigsEmail = sectionEmail.Get<EmailSettings>();
            var emailConfiguration = new EmailConfiguration(settigsEmail.Email, settigsEmail.Password);

            services.AddSingleton(emailConfiguration);
            services.AddSingleton(apiConfiguration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackAuth", Version = "v1" });
            });

            var appSettingssSection = Configuration.GetSection("AppSetings");
            services.Configure<AppSettings>(appSettingssSection);

            var appSettigs = appSettingssSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettigs.Secret);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(d =>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyCors, builder =>
                {
                    //builder.AllowAnyOrigin("*");
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "62b376e5b99f4e24579f28d4--ubiquitous-croquembouche-e7da09.netlify.app")
                    .AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserEntity, UserEntity>();            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IValidationsService, ValidationsService>();
            services.AddScoped<IImageProfileService, ImageProfileService>();
            services.AddScoped<IImageProfileEntity, ImageProfileEntity>();
            services.AddScoped<ISendingEmailService, SendingEmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackAuth v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_MyCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
