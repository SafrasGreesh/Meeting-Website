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
			services.AddDbContext<ApplicationContext>(opt =>
				opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));

			services.AddAutoMapper(typeof(UserProfile));
			services.AddCors();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sailora WEB API API", Version = "v1" });
			});

			services.AddScoped<IUserService, UserService>();
            services.AddSession(); //!
            // Добавьте следующие строки

            services.AddRazorPages();
			services.AddSignalR();
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
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapHub<ChatHub>("/chatHub");
				endpoints.MapFallbackToPage("/home");
			});
        }

		
	}
}
