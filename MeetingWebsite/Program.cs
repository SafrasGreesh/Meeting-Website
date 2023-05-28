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
using Microsoft.AspNetCore.Http.HttpResults;

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
                        Name = "Анна",
                        BirthDate = new DateTime(2001, 12, 1),
                        City = "Ижевск",
                        Description = "Увлекаюсь танцами",
                        Gender = "W",
                        Id = 1,
                        Mail = "anna@mail.ru",
                        Photo = "1639034091_1.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "2",
                    Name = "Катя",
                    BirthDate = new DateTime(2000, 12, 1),
                    City = "Ижевск",
                    Description = "Люблю рисовать",
                    Gender = "W",
                    Id = 2,
                    Mail = "katya@mail.ru",
                    Photo = "212e84f55685b7d688be7fc348702e91.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "3",
                    Name = "Антон",
                    BirthDate = new DateTime(2003, 12, 1),
                    City = "Ижевск",
                    Description = "Хожу в театр и оперу",
                    Gender = "M",
                    Id = 3,
                    Mail = "anna@mail.ru",
                    Photo = "15520220501617672.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "Ваня",
                    BirthDate = new DateTime(2004, 12, 1),
                    City = "Ижевск",
                    Description = "Люблю спорт, играю в футбол",
                    Gender = "M",
                    Id = 4,
                    Mail = "vanya@mail.ru",
                    Photo = "0-15.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "Аля",
                    BirthDate = new DateTime(2002, 12, 1),
                    City = "Ижевск",
                    Description = "Люблю лежать дома и смотреть сериалы",
                    Gender = "W",
                    Id = 5,
                    Mail = "aly@mail.ru",
                    Photo = "6fcde8c90885de47abc724430eae0987.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "Саша",
                    BirthDate = new DateTime(2001, 12, 1),
                    City = "Ижевск",
                    Description = "Люблю плавать",
                    Gender = "W",
                    Id = 6,
                    Mail = "sasha@mail.ru",
                    Photo = "bf1eb795b5f269e3ca7a833f954d1c85.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "Coня",
                    BirthDate = new DateTime(2004, 12, 1),
                    City = "Ижевск",
                    Description = "Хочу научиться многому в этой жизни",
                    Gender = "W",
                    Id = 7,
                    Mail = "sonya@mail.ru",
                    Photo = "f55422c6a32eb07846bc9558bc28a03b.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "4",
                    Name = "Регина",
                    BirthDate = new DateTime(1999, 12, 1),
                    City = "Ижевск",
                    Description = "Просто хороший человек)",
                    Gender = "W",
                    Id = 8,
                    Mail = "regina@mail.ru",
                    Photo = "e23a88177aacb2cf1695497f9bd3116b.jpg"
				});
                context.Users.Add(new Users
                {
                    Password = "Qwerty12345",
                    Name = "Алексей",
                    BirthDate = new DateTime(1999, 12, 1),
                    City = "Ижевск",
                    Description = "Просто хороший человек)",
                    Gender = "W",
                    Id = 9,
                    Mail = "alexey@mail.ru",
                    Photo = "29f22c87a5033d3bcfd54170538fff6c.jpg"
                });
                context.Matches.Add(new Matches()
                {
                    UserId1 = 1,
                    UserId2=9,
                    Id = 0,
                    CreatedAt = new DateTime(2023, 05, 28)
                });
                context.Matches.Add(new Matches()
                {
                    UserId1 = 1,
                    UserId2 = 2,
                    Id = 1,
                    CreatedAt = new DateTime(2023, 05, 28)
                });
                context.Likes.Add(new Likes()
                {
                    UserId = 1,
                    LikeUserId = 9,
                    Like = true,
                    Id = 0,
                    CreatedAt = new DateTime(2023, 05, 28)
                });
                context.Likes.Add(new Likes()
                {
                    UserId = 9,
                    LikeUserId = 1,
                    Like = true,
                    Id = 1,
                    CreatedAt = new DateTime(2023, 05, 28)
                });
                context.Likes.Add(new Likes()
                {
                    UserId = 2,
                    LikeUserId = 9,
                    Like = true,
                    Id = 2,
                    CreatedAt = new DateTime(2023, 05, 28)
                });
                context.Likes.Add(new Likes()
                {
                    UserId = 9,
                    LikeUserId = 2,
                    Like = true,
                    Id = 3,
                    CreatedAt = new DateTime(2023, 05, 28)
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
