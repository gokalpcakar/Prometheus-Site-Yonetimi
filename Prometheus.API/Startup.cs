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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // adding profile
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper();

            // configure mapper
            services.AddSingleton(mapper);

            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddSession(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "SessionUser";
            });

            // configure strongly typed settings object
            var appSettingsSection = Configuration.GetSection("JwtConfig");
            services.Configure<JwtConfig>(appSettingsSection);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                //jwt.Events = new JwtBearerEvents()
                //{
                //    OnTokenValidated = context =>
                //    {
                //        var userMachine = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
                //        var user = userMachine.GetUserAsync(context.HttpContext.User);

                //        if (user is null)
                //        {
                //            context.Fail("UnAuthorized");
                //        }

                //        return Task.CompletedTask;
                //    }
                //};
                var appSettings = appSettingsSection.Get<JwtConfig>();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);

                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    //ValidateLifetime = true,
                    //RequireExpirationTime = false
                };
            });

            services.AddCors();

            // allows us injection of idbclient 
            services.AddSingleton<IDbClient, DbClient>();

            // mongodb configuration
            services.Configure<CreditCardDbConfig>(Configuration);

            // configure application services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<IMessageService, MessageService>();

            // adding service for json web token
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

            // port 3000 is react app port
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
