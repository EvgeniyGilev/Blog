namespace BlogAPI.Controllers
{
    using BlogAPI.Contracts.Models;
    using BlogAPI.Contracts.Models.Users;
    using BlogAPI.DATA.Models;
    using BlogAPI.DATA.Repositories.Interfaces;
    using BlogAPI.Handlers;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Действия с пользователем.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("api/user/")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="signInManager"> SignInManager AspNetCore.Identity.</param>
        /// <param name="userManager"> UserManager AspNetCore.Identity.</param>
        /// <param name="logger">NLOG logger.</param>
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<UserController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <response code="200">Пользователи выведены.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает пользователей в формате JSON.</returns>
        // GET: UserController
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            List<ShowUserModel> model = new ();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                List<string> roles = new ();

                foreach (var role in userRoles)
                {
                    roles.Add(role.ToString());
                }

                model.Add(new ShowUserModel
                {
                    id = user.Id,
                    Email = user.Email,
                    Name = user.UserFirstName + " " + user.UserLastName,
                    Roles = roles,
                });
            }

            GetAllUsersModel resp = new ()
            {
                Count = users.Count,
                Users = model,
            };

            _logger.LogInformation("Форма отображения всех пользователей, всего пользователей: " + resp.Count.ToString());
            return Json(resp);
        }

        /// <summary>
        /// Получить одного пользователя по его ID.
        /// </summary>
        /// <param name="id"> GUID пользователя.</param>
        /// <response code="200">Пользователь выведен.</response>
        /// <response code="400">Пользователь не найден.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает одного пользователя в формате JSON.</returns>
        // GET: UserController
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                EditUserModel model = new ()
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserFirstName = user.UserFirstName,
                    UserLastName = user.UserLastName,
                };
                _logger.LogInformation("Форма пользователя по его id: " + id + " Email: " + user.UserName);
                return Json(model);
            }
            else
            {
                _logger.LogWarning("Пользователь не найден");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не найден", ErrorCode = 40009 }).Value);
            }
        }

        /// <summary>
        /// Отредактировать пользователя по его id.
        /// </summary>
        /// <param name="newUser"> Измененная форма пользователя.</param>
        /// <param name="id"> GUID пользователя.</param>
        /// <response code="200"> Пользователь успешно изменен.</response>
        /// <response code="400"> Пользователь не изменен.</response>
        /// <response code="500"> Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом редактирования, JSON.</returns>
        // GET: UserController/Edit/5
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromForm] EditUserModel newUser, [FromRoute] string id)
        {
            if (User.IsInRole("Администратор"))
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Email = newUser.Email;
                    user.UserName = newUser.Email;

                    user.UserLastName = newUser.UserLastName;
                    user.UserFirstName = newUser.UserFirstName;
                    user.UserPassword = newUser.UserPassword;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Пользователь отредактирован Email: " + user.UserName);

                        SuccessResponse resp = new ()
                        {
                            code = 0,
                            name = user.Email,
                            infoMessage = "Пользователь отредактирован",
                        };

                        return Json(resp);
                    }
                    else
                    {
                        string errorMessage = string.Empty;
                        foreach (var er in result.Errors)
                        {
                            errorMessage = er.Code + " " + er.Description + ";";
                        }

                        _logger.LogWarning("Пользователь не изменен");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не изменен - " + errorMessage, ErrorCode = 40010 }).Value);
                    }
                }
                else
                {
                    _logger.LogWarning("Пользователь не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не найден", ErrorCode = 40009 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Удалить пользователя по его ID.
        /// </summary>
        /// <param name="id"> GUID пользователя.</param>
        /// <response code="200">Пользователь удален.</response>
        /// <response code="400">Пользователь не найден.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом удаления, JSON.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (User.IsInRole("Администратор"))
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    /* Если удаляем пользователя то нужно решать что делать с его Статьями и его комментариями.
                    //В лоб удаляем статьи пользователя
                    /* if (user.Posts != null)
                     {
                         foreach (var post in user.Posts)
                         {
                             await _repoposts.DelPost(post);
                             _logger.LogInformation("Удаление статьей пользователя, id статьи: " + post.id + " название " + post.postName);
                         }
                     }
                     // в лоб удаляем комментарии
                     if (user.Comments != null)
                     {
                         foreach (var comment in user.Comments)
                         {
                             await _repocomments.DelComment(comment);
                             _logger.LogInformation("Удаляем комментарий пользователя, id комментария: " + comment.id);
                         }
                     }
                    */

                    // Удаляем самого пользователя
                    await _userManager.DeleteAsync(user);
                    _logger.LogInformation("Пользователь удален Email: " + user.UserName);

                    SuccessResponse resp = new ()
                    {
                        code = 0,
                        name = user.Email,
                        infoMessage = "Пользователь удаленн",
                    };

                    return Json(resp);
                }
                else
                {
                    _logger.LogWarning("Пользователь не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не найден", ErrorCode = 40009 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="newUser">Форма данных пользователя.</param>
        /// <response code="200">Пользователь зарегистрирован.</response>
        /// <response code="401">Пользователь не зарегистрирован.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом добавления, JSON.</returns>
        // POST: UserController/Register
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Register([FromForm] CreateUserModel newUser)
        {
            User user = new ()
            {
                Email = newUser.Email,
                UserName = newUser.Email,
                UserCreateDate = DateTime.Now.ToString(),
                UserFirstName = newUser.UserFirstName,
                UserLastName = newUser.UserLastName,
                UserPassword = newUser.UserPassword,
            };

            // по умолчанию права пользователя
            var result = await _userManager.CreateAsync(user, newUser.UserPassword);
            if (result.Succeeded)
            {
                // добавляем роль по умолчанию Пользователь
                await _userManager.AddToRoleAsync(user, "Пользователь");

                _logger.LogInformation("Пользователь зарегистрирован Email: " + user.UserName);

                SuccessResponse resp = new ()
                {
                    code = 0,
                    name = user.Email,
                    infoMessage = "Пользователь зарегистрирован",
                };

                return Json(resp);
            }
            else
            {
                string errorMessage = string.Empty;
                foreach (var er in result.Errors)
                {
                    errorMessage = er.Code + " " + er.Description + ";";
                }

                _logger.LogWarning("Пользователь не зарегистрирован");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не зарегистрирован - " + errorMessage, ErrorCode = 40008 }).Value);
            }
        }

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="user"> Модель пользователя - логин (email) и пароль.</param>
        /// <response code="200">Пользователь успешно залогинился.</response>
        /// <response code="401">Ошибки при аутентификации.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом аутентификации.</returns>
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUserModel user)
        {
            var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);

            if (searchuser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Пользователь успешно залогинился Email: " + user.Email);

                    SuccessResponse resp = new ()
                    {
                        code = 0,
                        name = searchuser.Email,
                        infoMessage = "Пользователь успешно залогинился",
                    };

                    return Json(resp);
                }
                else
                {
                    _logger.LogWarning("Неправильный пароль");
                    return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Неправильный пароль", ErrorCode = 40005 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Пользователь с таким логином не найден");
                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Пользователь с таким логином не найден", ErrorCode = 40006 }).Value);
            }
        }

        /// <summary>
        /// Выход пользователя.
        /// </summary>
        /// <response code="200">Пользователь успешно разлогинился.</response>
        /// <response code="401">Ошибки при выходе.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение о статусе выхода.</returns>
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await _signInManager.SignOutAsync();
                HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

                SuccessResponse resp = new ()
                {
                    code = 0,
                    name = User.Identity.Name,
                    infoMessage = "Пользователь успешно разлогинился",
                };

                _logger.LogInformation("Пользователь успешно вышел Email: " + username);
                return Json(resp);
            }
            else
            {
                _logger.LogWarning("Пользователь не залогинен, поэтому разлогиниться не получится");
                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Пользователь не залогинен", ErrorCode = 40007 }).Value);
            }
        }
    }
}
