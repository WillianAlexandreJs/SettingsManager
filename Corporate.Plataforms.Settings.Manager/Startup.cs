using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System;
using Corporate.Plataforms.Settings.Manager;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Corporate.Plataforms.Settings.Manager.Datas;
using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;

namespace Corporate.Plataforms.Settings.Manager
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

            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddJsonOptions(opcoes => { opcoes.JsonSerializerOptions.IgnoreNullValues = true; });

            services.Configure<GzipCompressionProviderOptions>(opcoes => opcoes.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.AddResponseCompression(opcoes =>
            {
                opcoes.EnableForHttps = true;
                opcoes.Providers.Add<GzipCompressionProvider>();
            });

            //SignalR with Redis
            services.AddSignalR().AddStackExchangeRedis("localhost");

            services.AddSingleton<IUserIdProvider, ApplicationIdProvider>();
            services.AddSingleton<ISettingsData, SettingsData>();

            var audienceConfig = Configuration.GetSection("TokenConfigurations");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"])),
                    ValidateIssuer = true,
                    ValidIssuer = audienceConfig["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = audienceConfig["Audience"],
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = false,
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken) && context.HttpContext.Request.Path.StartsWithSegments("/Configuration"))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "XP Aplication Configuration SignalR API",
                    Version = PlatformServices.Default.Application.ApplicationVersion,
                    Description = "WebAPi SignalR para atualização das configurações das aplicações"
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllOrigins");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ConfigurationHubClient>("/Configuration");
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XP SignalR API");
            });


        }
    }
}
