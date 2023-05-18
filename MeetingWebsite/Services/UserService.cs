using MeetingWebsite.Entity;
using Org.BouncyCastle.Crypto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MeetingWebsite.Helpers;
using MeetingWebsite.Models;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Http;

namespace MeetingWebsite.Services
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<Users> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IEfRepository<Users> userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model) //проверяет наличие юзера в бд и соответствие логина и пароля
        {
            var user = _userRepository
                .GetAll() //список возащает пользователй или ссылку
                .FirstOrDefault(x => x.Mail == model.Mail && x.Password == model.Password); //поиск по списку

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user); //генерация токена

            return new AuthenticateResponse(user, token); //предоставление прав доступа? 
        }

        public async Task<AuthenticateResponse> Register(UserModel userModel)
        {
            var user = _mapper.Map<Users>(userModel); //создает объект юзера

            var addedUser = await _userRepository.Add(user); //добавление юзера  в бд

            var response = Authenticate(new AuthenticateRequest
            {
                Mail = user.Mail,
                Password = user.Password
            });

            return response; //если норм возвращает норм
        }

        public IEnumerable<Users> GetAll()
        {
            return _userRepository.GetAll(); //возвращает репозиторий юзеров
        }

        public IEnumerable<Users> Swipe(int id_y)
        {
            var UsersSwipe = _userRepository.GetAll().Where(u => u.Id != id_y); ;

            return UsersSwipe; //возвращает репозиторий юзеров
        }

        public Users GetById(int id)
        {
            return _userRepository.GetById(id); //вовзаращет репозиторий
        }

        public async Task<AuthenticateResponse> UpdateInformation(UserModel userModel, int? id)
        {

            var user = _userRepository.GetById(id); // Найти пользователя по идентификатору

            if (user == null)
            {
                // Обработка случая, когда пользователь с заданным идентификатором не найден
                // Можно выбросить исключение или вернуть сообщение об ошибке
                // Например:
                throw new DllNotFoundException("User not found");
            }

            // Обновить свойства пользователя с помощью значений из модели
            if (userModel.Name != "")
            {
				user.Name = userModel.Name;
			}
			if (userModel.Mail != "")
			{
				user.Mail = userModel.Mail;
			}
			if (userModel.City != "")
			{
				user.City = userModel.City;
			}
			if (userModel.Gender != "")
			{
				user.Gender = userModel.Gender;
			}
			if (userModel.Description != "")
			{
				user.Description = userModel.Description;
			}

			await _userRepository.UserUpdate(id ?? 0, user); // Сохранить изменения в базе данных

            var response = Authenticate(new AuthenticateRequest
            {
                Mail = user.Mail,
                Password = user.Password
            });

            return response;
        }

    }
}
