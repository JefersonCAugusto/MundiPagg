using DesafioMundi.Context;
using DesafioMundi.Entities;
using DesafioMundi.Services;
using DesafioMundi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DesafioMundi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddDbContext<MundiContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("StringConnect")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<MundiContext>()
                    .AddDefaultTokenProviders();



            //jwt 
            var appSettingsSection = Configuration.GetSection("AppSettings");
            //faz a classe Sppsettings responder aos dados do arquivo json Appsetting.json
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //informa que trabalhara com Tokens para autenticar ususarios
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    //trabalha com segurança / salva token
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    //validações do token
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        //as audiencias validas para comparaçao
                        ValidAudience = appSettings.ValidIn,
                        ValidIssuer = appSettings.Issuer
                    };
                });
            //swagger
            services.AddSwaggerGen(x =>
                {
                    //configurações do desenvolvedor 
                    x.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "MundiAPI",
                        TermsOfService = new Uri("https://localhost/"),
                        Description = "Desafio MundiPagg-Stone",
                        Contact = new OpenApiContact
                        {
                            Name = "Jeferson Augusto",
                            Email = "carvalho.jeferson@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/jeferson-augusto-aa611164/")

                        },
                        License = new OpenApiLicense
                        {
                            Name = "Home",
                            Url = new Uri("https://localhost/")
                        }

                    });
                    // Configurações para os comentários XMl
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    x.IncludeXmlComments(xmlPath);

                });


       
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                 app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.Json", "Desafio Stone");
            });
            app.UseSwagger();
            app.UseMvc();

        }
    }
}
