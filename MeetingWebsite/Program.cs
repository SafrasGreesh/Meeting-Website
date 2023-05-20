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
                        City = "���������",
                        Description = "����� ����� �� ������",
                        Gender = "M",
                        Id = 1,
                        Mail = "aboba@mail.ru",
                        Photo = ""
                    });
                context.Users.Add(new Users
                {
                    Password = "2",
                    Name = "����",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "������",
                    Description = "����� ����� �� ������",
                    Gender = "W",
                    Id = 2,
                    Mail = "lada@mail.ru",
                    Photo = ""
                });
                context.Users.Add(new Users
                {
                    Password = "3",
                    Name = "����",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "�������",
                    Description = "����� ����� �� ������",
                    Gender = "M",
                    Id = 3,
                    Mail = "anna@mail.ru",
                    Photo = ""
                });
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "����",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "�������",
                    Description = "����� ����� �� ������",
                    Gender = "M",
                    Id = 4,
                    Mail = "vanya@mail.ru",
                    Photo = ""
                });
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "���",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "�������",
                    Description = "����� ����� �� ������",
                    Gender = "W",
                    Id = 5,
                    Mail = "aly@mail.ru",
                    Photo = ""
                });
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "����",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "�������",
                    Description = "����� ����� �� ������",
                    Gender = "W",
                    Id = 6,
                    Mail = "sasha@mail.ru",
                    Photo = ""
                });
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "Co��",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "�������",
                    Description = "����� ����� �� ������",
                    Gender = "W",
                    Id = 7,
                    Mail = "sonya@mail.ru",
                    Photo = ""
                });
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "������",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "�������",
                    Description = "����� ����� �� ������",
                    Gender = "W",
                    Id = 8,
                    Mail = "regina@mail.ru",
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
                    //await response.SendFileAsync("C:\\Users\\Professional\\source\\repos\\Meeting-Website\\MeetingWebsite\\Pages\\Home.cshtml");
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
