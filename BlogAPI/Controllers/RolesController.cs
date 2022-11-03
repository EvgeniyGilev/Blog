// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using AutoMapper;
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Roles;
using BlogAPI.DATA.Models;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("api/role/")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleManager">RoleManager AspNetCore.Identity.</param>
        /// <param name="userManager">UserManager AspNetCore.Identity.</param>
        /// <param name="logger">NLOG logger.</param>
        /// <param name="mapper">Automapper.</param>
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager, ILogger<RolesController> logger, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все роли.
        /// </summary>
        /// <response code="200"> Роли успешно выведены.</response>
        /// <response code="400"> Роли не найдены, создайте роль.</response>
        /// <response code="500"> Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает существующие роли.</returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetRoles()
        {
            List<Role> roles = _roleManager.Roles.ToList();

            if (roles.Count != 0)
            {
                GetRolesModel resp = new ()
                {
                    Count = roles.Count,
                    Roles = _mapper.Map<List<Role>, List<ShowRoleView>>(roles),
                };

                _logger.LogInformation("Показываем все доступные роли");
                return Json(resp);
            }
            else
            {
                _logger.LogInformation("Роли не найдены");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роли не найдены", ErrorCode = 40003 }).Value);
            }
        }

        /// <summary>
        /// Создать новую роль.
        /// </summary>
        /// <param name="newRole">Данные для создания новой роли.</param>
        /// <response code="200">Роль успешно создана.</response>
        /// <response code="400">Роль не создана.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом создания роли, JSON.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] CreateRoleView newRole)
        {
            if (User.IsInRole("Администратор"))
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role { Name = newRole.Name, Description = newRole.Description });
                if (result.Succeeded)
                {
                    _logger.LogInformation("Новая роль добавлена: " + newRole.Name);

                    SuccessResponse resp = new ()
                    {
                        Code = 0,
                        InfoMessage = "Новая роль добавлена: " + newRole.Name,
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

                    _logger.LogWarning("Роль не добавлена");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Новая роль не добавлена - " + errorMessage, ErrorCode = 40010 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Удалить роль.
        /// </summary>
        /// <param name="id">Id (GUID) роли.</param>
        /// <response code="200"> Роль успешно удалена.</response>
        /// <response code="400"> Роль не удалена.</response>
        /// <response code="500"> Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом удаления, JSON.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (User.IsInRole("Администратор"))
            {
                Role role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    if ((role.Name != "Администратор") && (role.Name != "Пользователь"))
                    {
                        IdentityResult result = await _roleManager.DeleteAsync(role);
                        _logger.LogInformation("Роль удалена: " + role.Name);
                        if (result.Succeeded)
                        {
                            SuccessResponse resp = new ()
                            {
                                Code = 0,
                                Id = id,
                                Name = role.Name,
                                InfoMessage = "Роль успешно удалена",
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

                            _logger.LogWarning("Роль не добавлена");
                            return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роль не удалена - " + errorMessage, ErrorCode = 40010 }).Value);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Нельзя удалить роль Пользователя и роль Администратора");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Нельзя удалить роль Пользователя и роль Администратора", ErrorCode = 40003 }).Value);
                    }
                }
                else
                {
                    _logger.LogInformation("Роль не найдена");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роль не найдена", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Добавить роль пользователю.
        /// </summary>
        /// <param name="userId">Id (GUID) пользователя.</param>
        /// <param name="roleid">Id (GUID) роли.</param>
        /// <response code="200"> Роль успешно добавлена пользователю.</response>
        /// <response code="400"> Роль не добавлена.</response>
        /// <response code="500"> Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом добавления, JSON.</returns>
        [HttpPost]
        [Route("{roleid}/user/{userId}")]
        public async Task<IActionResult> AddUserRole([FromRoute] string userId, [FromRoute] string roleid)
        {
            if (User.IsInRole("Администратор"))
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получаем роль по id
                    var roleToAdd = await _roleManager.FindByIdAsync(roleid);

                    if (roleToAdd != null)
                    {
                        await _userManager.AddToRoleAsync(user, roleToAdd.Name);
                        _logger.LogInformation("Роль успешно добавлена для пользователя: " + user.Email + " имя роли: " + roleToAdd.Name);

                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            InfoMessage = "Роль успешно добавлена для пользователя: " + user.Email + " имя роли: " + roleToAdd.Name,
                        };
                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Роль не найдена");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роль не найдена", ErrorCode = 40003 }).Value);
                    }
                }
                else
                {
                    _logger.LogInformation("Пользователь не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не найден", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Добавить роль пользователю.
        /// </summary>
        /// <param name="userId">Id (GUID) пользователя.</param>
        /// <param name="roleid">Id (GUID) роли.</param>
        /// <response code="200"> Роль успешно удалена у пользователя.</response>
        /// <response code="400"> Роль не удалена.</response>
        /// <response code="500"> Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом добавления, JSON.</returns>
        [HttpDelete]
        [Route("{roleid}/user/{userId}")]
        public async Task<IActionResult> RemoveUserRole([FromRoute] string userId, [FromRoute] string roleid)
        {
            if (User.IsInRole("Администратор"))
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получаем роль по id
                    var roleToAdd = await _roleManager.FindByIdAsync(roleid);

                    if (roleToAdd != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user, roleToAdd.Name);
                        _logger.LogInformation("Роль успешно удалена для пользователя: " + user.Email + " имя роли: " + roleToAdd.Name);

                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            InfoMessage = "Роль успешно удалена для пользователя: " + user.Email + " имя роли: " + roleToAdd.Name,
                        };
                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Роль не найдена");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роль не найдена", ErrorCode = 40003 }).Value);
                    }
                }
                else
                {
                    _logger.LogInformation("Пользователь не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не найден", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Изменение имени или описания роли.
        /// </summary>
        /// <param name="newRole"> модель роли - имя и описание.</param>
        /// <response code="200"> Роль успешно удалена у пользователя.</response>
        /// <response code="400"> Роль не удалена.</response>
        /// <response code="500"> Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом изменения роли, JSON.</returns>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> EditRole([FromForm] ShowRoleView newRole)
        {
            if (User.IsInRole("Администратор"))
            {
                Role role = await _roleManager.FindByIdAsync(newRole.id);
                if (role != null)
                {
                    if ((role.Name != "Администратор") && (role.Name != "Пользователь"))
                    {
                        role.Name = newRole.Name;
                        role.Description = newRole.Description;
                        IdentityResult result = await _roleManager.UpdateAsync(role);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("изменили роль: " + role.Name);

                            SuccessResponse resp = new ()
                            {
                                Code = 0,
                                Id = newRole.id,
                                Name = newRole.Name,
                                InfoMessage = "Роль успешно отредактирована",
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

                            _logger.LogWarning("Роль не изменена");
                            return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роль не изменена - " + errorMessage, ErrorCode = 40010 }).Value);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Нельзя редактировать роль Пользователя и роль Администратора");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Нельзя удалить роль Пользователя и роль Администратора", ErrorCode = 40003 }).Value);
                    }
                }
                else
                {
                    _logger.LogInformation("Роль не найдена");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Роль не найдена", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }
    }
}