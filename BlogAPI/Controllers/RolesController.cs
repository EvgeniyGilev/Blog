using AutoMapper;
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Roles;
using BlogAPI.DATA.Models;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ExceptionHandler]
    public class RolesController : Controller
    {
        RoleManager<Role> _roleManager;
        UserManager<User> _userManager;
        private readonly ILogger<RolesController> _logger;
        private IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager, ILogger<RolesController> logger, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все роли
        /// </summary>
        /// <response code="200"> Роли успешно выведены</response>
        /// <response code="400"> Роли не найдены, создайте роль </response>
        /// <response code="500"> Произошла непредвиденная ошибка</response>
        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            List<Role> roles = _roleManager.Roles.ToList();

            if (roles.Count != 0)
            {
                GetRolesModel resp = new()
                {
                    Count = roles.Count,
                    Roles = _mapper.Map<List<Role>, List<ShowRoleView>>(roles)
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
        /// Создать новую роль
        /// </summary>
        /// <response code="200"> Роль успешно создана</response>
        /// <response code="400"> Роль не создана </response>
        /// <response code="500"> Произошла непредвиденная ошибка</response>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreateRoleView newRole)
        {
            if (User.IsInRole("Администратор"))
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role { Name = newRole.Name, Description = newRole.Description });
                if (result.Succeeded)
                {
                    _logger.LogInformation("Новая роль добавлена: " + newRole.Name);

                    SuccessResponse resp = new()
                    {
                        code = 0,
                        infoMessage = "Новая роль добавлена: " + newRole.Name
                    };

                    return Json(resp);
                }
                else
                {
                    string errorMessage = "";
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
        /// Удалить роль
        /// </summary>
        /// <param name="id">Id (GUID) роли</param>
        /// <response code="200"> Роль успешно удалена</response>
        /// <response code="400"> Роль не удалена </response>
        /// <response code="500"> Произошла непредвиденная ошибка</response>
        /// 
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (User.IsInRole("Администратор"))
            {
                Role role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                    _logger.LogInformation("Роль удалена: " + role.Name);
                    if (result.Succeeded)
                    {
                        SuccessResponse resp = new()
                        {
                            code = 0,
                            id = id,
                            name = role.Name,
                            infoMessage = "Роль успешно удалена"
                        };
                        return Json(resp);
                    }
                    else
                    {
                        string errorMessage = "";
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
        /// Добавить роль пользователю
        /// </summary>
        /// <param name="userId">Id (GUID) пользователя</param>
        /// <param name="roleid">Id (GUID) роли</param>
        /// <response code="200"> Роль успешно добавлена пользователю</response>
        /// <response code="400"> Роль не добавлена </response>
        /// <response code="500"> Произошла непредвиденная ошибка</response>
        [HttpPatch]
        [Route("AddUserRole")]
        public async Task<IActionResult> AddUserRole(string userId, string roleid)
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

                        SuccessResponse resp = new()
                        {
                            code = 0,
                            infoMessage = "Роль успешно добавлена для пользователя: " + user.Email + " имя роли: " + roleToAdd.Name
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
        /// Добавить роль пользователю
        /// </summary>
        /// <param name="userId">Id (GUID) пользователя</param>
        /// <param name="roleid">Id (GUID) роли</param>
        /// <response code="200"> Роль успешно удалена у пользователя</response>
        /// <response code="400"> Роль не удалена </response>
        /// <response code="500"> Произошла непредвиденная ошибка</response>
        [HttpPatch]
        [Route("RemoveUserRole")]
        public async Task<IActionResult> RemoveUserRole(string userId, string roleid)
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

                        SuccessResponse resp = new()
                        {
                            code = 0,
                            infoMessage = "Роль успешно удалена для пользователя: " + user.Email + " имя роли: " + roleToAdd.Name
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
        /// Изменение имени или описания роли
        /// </summary>
        /// <param name="newRole"> модель роли - имя и описание</param>
        /// <response code="200"> Роль успешно удалена у пользователя</response>
        /// <response code="400"> Роль не удалена </response>
        /// <response code="500"> Произошла непредвиденная ошибка</response>
        [HttpPatch]
        [Route("EditRole")]
        public async Task<IActionResult> EditRole([FromForm] ShowRoleView newRole)
        {
            if (User.IsInRole("Администратор"))
            {
                Role role = await _roleManager.FindByIdAsync(newRole.id);
                if (role != null)
                {
                    role.Name = newRole.Name;
                    role.Description = newRole.Description;
                    IdentityResult result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("изменили роль: " + role.Name);

                        SuccessResponse resp = new()
                        {
                            code = 0,
                            id = newRole.id,
                            name = newRole.Name,
                            infoMessage = "Роль успешно отредактирована"
                        };
                        return Json(resp);

                    }
                    else
                    {
                        string errorMessage = "";
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