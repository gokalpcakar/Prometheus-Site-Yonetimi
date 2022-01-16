using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus.API.Helpers;
using Prometheus.API.Infrastructure;
using Prometheus.DB.Database;
using Prometheus.Service.Apartment;
using Prometheus.Service.Bill;
using Prometheus.Service.CreditCard;
using Prometheus.Service.Message;
using Prometheus.Service.User;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.API
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
            // mapping için profil ekleniyor
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper();

            // configure mapper
            services.AddSingleton(mapper);

            services.AddMvc().AddSessionStateTempDataProvider();

            // session ekliyoruz
            services.AddSession(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "SessionUser";
            });

            var appSettingsSection = Configuration.GetSection("JwtConfig");
            services.Configure<JwtConfig>(appSettingsSection);

            // token ile yetkilendirme ayarlarý yapýlýyor
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var appSettings = appSettingsSection.Get<JwtConfig>();
                // appsettings.json içerisindeki key alýnýyor
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);

                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors();

            // mongodb'de bulunan kredi kartý bilgileri için bu servis ekleniyor
            services.AddSingleton<IDbClient, DbClient>();

            // mongodb konfigürasyonu
            services.Configure<CreditCardDbConfig>(Configuration);

            // gerekli servisler iliþkilendiriliyor
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<IMessageService, MessageService>();

            // jwt token için gerekli servis ekleniyor
            services.AddScoped<JwtService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prometheus.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prometheus.API v1"));
            }

            // 3000 portu react uygulamamýzýn portu
            app.UseCors(options => options.
                        WithOrigins("http://localhost:3000").
                        AllowAnyHeader().
                        AllowAnyMethod().
                        AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
