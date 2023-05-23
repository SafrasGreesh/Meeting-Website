using MeetingWebsite.Helpers;
using MeetingWebsite.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using BlazorServerSignalRApp.Server.Hubs;
using System.Reflection.Metadata;
using MeetingWebsite.Entity;
using MeetingWebsite.Handler;
using MeetingWebsite.Interfaces;
using MeetingWebsite.Services.HubService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebChat.AvatarWriter;
using WebChat.AvatarWriter.Interface;
using WebChat.Hubs.ConnectionMapper;
using WebChat.Hubs.Interfaces;
using WebChat.Hubs;

namespace MeetingWebsite
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
            //this.RegisterAuthentication(services);
            this.RegisterServices(services);

            services.AddDbContext<ApplicationContext>(opt =>
				opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped(typeof(IEfRepository<>), typeof(UserRepository<>));

			services.AddAutoMapper(typeof(UserProfile));
			services.AddCors();
			services.AddControllers();
			services.AddSwaggerGen();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sailora WEB API API", Version = "v1" });
			});

			services.AddScoped<IUsersService, UsersService>();
            services.AddSession(); //!
            // Добавьте следующие строки

            services.AddRazorPages();
			services.AddSignalR();
		}
        //private void RegisterAuthentication(IServiceCollection services)
        //{
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
        //        AddJwtBearer(options =>
        //        {
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = false,
        //                ValidateAudience = false,
        //                ValidateLifetime = true,
        //                ValidateIssuerSigningKey = true,

        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JWTSecretKey")))
        //            };
        //            options.Events = new JwtBearerEvents
        //            {
        //                OnMessageReceived = context =>
        //                {
        //                    var accessToken = context.Request.Query["access_token"];

        //                    // If the request is for our hub...
        //                    var path = context.HttpContext.Request.Path;
        //                    if (!string.IsNullOrEmpty(accessToken) &&
        //                        (path.StartsWithSegments("/chat")))
        //                    {
        //                        // Read the token out of the query string
        //                        context.Token = accessToken;
        //                    }
        //                    return Task.CompletedTask;
        //                }
        //            };
        //        });

        //}

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthService>
                (
                    new AuthService(
                        Configuration.GetValue<string>("JWTSecretKey"),
                        Configuration.GetValue<int>("JWTLifespan")

                        )
                );
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IThreadService, ThreadService>();
            services.AddTransient<IMappingService, MappingService>();
            services.AddTransient<IValidator, Validator>();
            services.AddSingleton(typeof(IConnectionMapping<string>), typeof(ConnectionMapping<string>));
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IAvatarWriter,
                                  AvatarWriter>();

            var test = Environment.GetEnvironmentVariable("HELLO");


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
            
            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(x =>
				{
					x.SwaggerEndpoint("/swagger/v1/swagger.json", "Sailora WEB API v1");
				});
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}
            app.UseSession(); //!
            app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseAuthentication(); // Добавьте эту строку, если вы используете аутентификацию
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapHub<ChatHub>("/chatHub");
				endpoints.MapFallbackToPage("/home");
			});
        }

		
	}
}
