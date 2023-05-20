/*Проверяет корректность поступивших пост запросов от клиента на сервер, например если запрос пустой, то вернет ошибки*/

using MeetingWebsite.Models;
using MeetingWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeetingWebsite.Helpers;
using Microsoft.AspNetCore.Http;
using MeetingWebsite.Pages;
using MeetingWebsite.Entity;

namespace MeetingWebsite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
       

        public UsersController(IUserService userService)
        {
            _userService = userService;
           
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });


            HttpContext.Session.SetInt32("Id", response.Id ?? 0); //!
                                                                  // Получение значения переменной из сессии
           


            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            var response = await _userService.Register(userModel);

            if (response == null)
            {
                return BadRequest(new { message = "Didn't register!" });
            }

            return Ok(response); 
        }


        [HttpPost("addEvent")]
        public async Task<IActionResult> AddEventq(Events eventModel)
        {
            var maxEventsId = await _userService.GetMaxEventId();
            var result = await _userService.AddEvent(eventModel, maxEventsId);
            if (result == null)
            {
                return BadRequest(new { message = "Didn't register!" });
            }
            
            return Ok(result);
           
        }

        [HttpGet("MaxEventId")]
        public async Task<int> GetMaxEventIdq()
        {
            var maxEventsId = await _userService.GetMaxEventId();
            return maxEventsId;
        }



        [HttpGet("id")]
        public IActionResult GetId()
        {
            int? id = HttpContext.Session.GetInt32("Id");

            if (id != null)
            {
                Console.WriteLine("Значение id из сессии: " + id);
                //return Ok(new { Id = id });
            }
            else
            {
                Console.WriteLine("Error");
                //return BadRequest("Id not found in session.");
            }

            var user = _userService.GetById(id ?? 0);

            if (user == null)
                return NotFound();

            return Ok(user);
        }



       // [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            var events = _userService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("EventId/{id}")]
        public IActionResult GetEventById(int id)
        {
            var eventq = _userService.GetEventById(id);

            if (eventq == null)
                return NotFound();

            return Ok(eventq);
        }


    }
}
