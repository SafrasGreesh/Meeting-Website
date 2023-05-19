using BlazorServerSignalRApp.Server.Hubs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MeetingWebsite;
using MeetingWebsite.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace MeetingWebsite.Services
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();


            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                db.Database.Migrate();
            }
            using (var context = new ApplicationContext())
            {

                context.Database.EnsureCreated();

                context.Users.RemoveRange(context.Users);
                context.Users.Add(new Users {
                        Password = "1",
                        Name = "Абоба",
                        BirthDate = new DateTime(2000, 12, 1),
                        City = "Абобчинск",
                        Description = "Люблю нихуя не делать",
                        Gender = "М",
                        Id = 1,
                        Mail = "aboba@mail.ru",
                        Photo = ""
                    });

                context.SaveChanges();
            }

            UploadFile();
            host.Run();

        }
        public static async void UploadFile()
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.Run(async (context) =>
            {
                var response = context.Response;
                var request = context.Request;

                response.ContentType = "text/html; charset=utf-8";

                if (request.Path == "/upload" && request.Method == "POST")
                {
                    IFormFileCollection files = request.Form.Files;
                    // путь к папке, где будут храниться файлы
                    var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
                    // создаем папку для хранения файлов
                    Directory.CreateDirectory(uploadPath);

                    foreach (var file in files)
                    {
                        // путь к папке uploads
                        string fullPath = $"{uploadPath}/{file.FileName}";

                        // сохраняем файл в папку uploads
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    await response.WriteAsync("Файлы успешно загружены");
                }
                else
                {
                    await response.SendFileAsync("C:\\Users\\Professional\\source\\repos\\Meeting-Website\\MeetingWebsite\\Pages\\Home.cshtml");
                }
            });

            app.Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
        
    }
}
