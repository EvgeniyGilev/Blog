using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogWebApp.DAL.Repositories;
using BlogWebApp.BLL.Models;
using BlogWebApp.DAL.Entities;
using AutoMapper;

namespace BlogWebApp.BLL.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;
        private IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //получить всех пользователей
        // GET: UserController
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> Index()
        {
            var users = await _repo.GetUsers();
            //var mapusers = _mapper.Map<UserEntity[], User[]>(users);
            return View(users);
        }

        //получить одного пользователя
        // GET: UserController
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.GetUserById (id);

            return View(user.UserName);
        }

        //зарегистрировать пользователя
        // POST: UserController/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserEntity newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }

        //отредактировать пользователя
        // GET: UserController/Edit/5
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(UserEntity newUser)
        {
            await _repo.EditUser(newUser);
            return View(newUser);
        }

        // POST: UserController/Delete/Id
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            
                var user = await _repo.GetUserById(id);
                if (user == null) { return RedirectToAction(nameof(Index)); }
                else
                { 
                await _repo.DelUser(user);
                return RedirectToAction(nameof(Index));
                }
        }
    }
}
