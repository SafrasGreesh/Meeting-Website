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
                        Name = "�����",
                        BirthDate = new DateTime(2000, 12, 1),
                        City = "���������",
                        Description = "����� ����� �� ������",
                        Gender = "M",
                        Id = 1,
                        Mail = "aboba@mail.ru",
                        Photo = "c9b15ca0-2d59-4456-b0cb-549c120eafbe.jpg"
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
                    Photo = "c9b15ca0-2d59-4456-b0cb-549c120eafbe.jpg"
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
                    Photo = "c9b15ca0-2d59-4456-b0cb-549c120eafbe.jpg"
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
                    Photo = "c9b15ca0-2d59-4456-b0cb-549c120eafbe.jpg"
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
                    Photo = "c9b15ca0-2d59-4456-b0cb-549c120eafbe.jpg"
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
                    Photo = "1.jpg"
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
                    Photo = "1.jpg"
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
                    Photo = "c9b15ca0-2d59-4456-b0cb-549c120eafbe.jpg"
                });
                context.SaveChanges();
            }
            host.Run();
        }

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
        
    }
}
