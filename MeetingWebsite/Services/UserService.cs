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
        private readonly IEfRepository<Options> _optionsRepository;
        private readonly IEfRepository<Likes> _likesRepository;
        private readonly IEfRepository<Matches> _matchesRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public async Task<int> GetMaxLikesId()
        {
            var events = await Task.Run(() => _likesRepository.GetAll());

            var maxId = events.Max(x => x.Id);

            maxId++;

            return maxId ?? 0;
        }
        public async Task<int> GetMaxMathesId()
        {
            var events = await Task.Run(() => _matchesRepository.GetAll());

            var maxId = events.Max(x => x.Id);

            maxId++;

            return maxId ?? 0;
        }
        public int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - birthDate.Year;

            if (birthDate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }
        public UserService(IEfRepository<Users> userRepository, IConfiguration configuration, IMapper mapper, IEfRepository<Options> optionsRepository, IEfRepository<Likes> likesRepository, IEfRepository<Matches> matchesRepository)
        {
            _optionsRepository = optionsRepository;
            _userRepository = userRepository;
            _likesRepository = likesRepository;
            _matchesRepository = matchesRepository;
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
        public Users GetById(int id)
        {
            return _userRepository.GetById(id); //вовзаращет репозиторий
        }
        public bool UnlickedUser(int id1, int id2)
        {
            // Проверяем наличие лайка пользователя id1 для пользователя id2
            var events = _likesRepository.GetAll().Where(u => u.UserId == id1 && u.LikeUserId == id2);

            // Если есть хотя бы одно совпадение, возвращаем false (попадался)
            if (events.Any())
            {
                return false;
            }

            // В противном случае, возвращаем true (не попадался)
            return true;
        }
        public IEnumerable<Users> Swipe(int id_y)
        {
            var options = GetOptionsById(id_y);
            if(options.Gender == "A")
            {
                var UsersSwipe = _userRepository.GetAll()
                    .Where(u => UnlickedUser(id_y, u.Id ?? 0))
                    .Where(u => u.Id != id_y && options.City == u.City && options.AgeMin < CalculateAge(u.BirthDate) && options.AgeMax > CalculateAge(u.BirthDate));
                return UsersSwipe;
            }
            else
            {
                var UsersSwipe = _userRepository.GetAll()
                    .Where(u => UnlickedUser(id_y, u.Id ?? 0))
                    .Where(u => u.Id != id_y && options.Gender == u.Gender && options.City == u.City && options.AgeMin < CalculateAge(u.BirthDate) && options.AgeMax > CalculateAge(u.BirthDate));  
                return UsersSwipe;
            }
     
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
        public async Task<bool> UpdateOptions(Options optModel, int? id)
        {

            var options = _optionsRepository.GetById(id); // Найти пользователя по идентификатору

            if (options == null)
            {

                optModel.Id = id;
                optModel.AgeMin = optModel.AgeMin;
                optModel.AgeMax = optModel.AgeMax;
                optModel.Gender = optModel.Gender;
                optModel.City = optModel.City;
                var addedUser = await _optionsRepository.Add(optModel);

                return true;
                //throw new DllNotFoundException("User not found");
            }

            options.AgeMin = optModel.AgeMin;
            options.AgeMax = optModel.AgeMax;
            options.Gender = optModel.Gender;
            options.City = optModel.City;

            await _optionsRepository.OptionsUpdate(id ?? 0, options); // Сохранить изменения в базе данных

            return true;
        }
        public Options GetOptionsById(int id)
        {
            var options = _optionsRepository.GetById(id);

            if(options == null)
            {
                var optModel = new Options();
                optModel.Id = id;
                optModel.AgeMin = 18;
                optModel.AgeMax = 100;
                optModel.Gender = "A";
                optModel.City = "";
                options = optModel;
            }
            return options; //вовзаращет репозиторий
        }
        public async Task<bool> Like(int? id1, int? id2, Boolean like_u)
        {
            var likeModel = new Likes();
            likeModel.Id = await GetMaxLikesId();
            likeModel.UserId = id1;
            likeModel.LikeUserId = id2;
            likeModel.CreatedAt = DateTime.Now;
            likeModel.Like = like_u;
            
            if (like_u == true)
            {
                var UsersMatches = _likesRepository.GetAll().Where(u => u.UserId == id2 && u.LikeUserId == id1 && u.Like == true);
                if(UsersMatches.Any())
                {
                    var matchesModel = new Matches();
                    matchesModel.Id = await GetMaxMathesId();
                    matchesModel.UserId1 = id1;
                    matchesModel.UserId2 = id2;
                    matchesModel.CreatedAt = DateTime.Now;

                    var match = _mapper.Map<Matches>(matchesModel); //создает объект юзера
                    var addedMatch = await _matchesRepository.Add(match);
                }
            }

            var like_user = _mapper.Map<Likes>(likeModel); //создает объект юзера

            var addedLikes = await _likesRepository.Add(like_user);



            var options = _likesRepository.GetById(id1); // Найти пользователя по идентификатору

            return true;
        }
        public IEnumerable<int> Matches(int id_y)
        {
            var options = GetOptionsById(id_y);
            var UsersMatches = _matchesRepository.GetAll().Where(u => u.UserId1 == id_y || u.UserId2 == id_y);

            // Проекция каждого элемента в id второго человека взаимного лайка и исключение значений null
            var secondUserIds = UsersMatches.Select(u => u.UserId1 == id_y ? u.UserId2 : u.UserId1)
                                            .Where(id => id.HasValue)
                                            .Select(id => id.Value)
                                            .Distinct();

            return secondUserIds;
        }



    }
}
