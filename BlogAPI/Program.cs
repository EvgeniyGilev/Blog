// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Reflection;
using System.Text.Json.Serialization;
using BlogAPI;
using BlogAPI.DATA.Context;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Interfaces.Services;
using BlogAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Debug("Старт приложения");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Добавляем опцию игнорирования в ответах JSON NULL значений объектов
    builder.Services.AddControllers().AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
    });

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "BlogApi",
            Version = "v1",
            Description = "Pet-проект API для блога, учетка администратора - логин admin@gmail.com пароль 123",
            Contact = new OpenApiContact
            {
                Name = "Evgeniy Gilev",
                Email = "euggil@ayndex.ru",
            },
        });
        var filePath = Path.Combine(AppContext.BaseDirectory, "Documentation\\docApi.xml");
        c.IncludeXmlComments(filePath);
    });

    // Подключаем автомаппинг
    var assembly = Assembly.GetAssembly(typeof(MappingProfile));
    builder.Services.AddAutoMapper(assembly);
    builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies").AddCookie("Cookies", options =>
{
    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
    {
        OnRedirectToLogin = redirectContext =>
        {
            redirectContext.HttpContext.Response.StatusCode = 401;
            return Task.CompletedTask;
        },
    };
});

    // регистрация сервиса репозитория (и его жизненного цикла scoped, singleton..)
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
    builder.Services.AddScoped<ITagRepository, TagRepository>();
    builder.Services.AddScoped<IPostRepository, PostRepository>();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped<ITagService, TagService>();
    builder.Services.AddScoped<IPostService, PostService>();
    builder.Services.AddScoped<ICommentService, CommentService>();

    // задаем подключение к БД. Строку подключения берем из конфигурации
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString), ServiceLifetime.Singleton);

    builder.Services.AddIdentity<User, Role>(opts =>
    {
        opts.Password.RequiredLength = 3;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
        opts.Password.RequiredUniqueChars = 0;
        opts.SignIn.RequireConfirmedAccount = false;
        opts.SignIn.RequireConfirmedEmail = false;
        opts.SignIn.RequireConfirmedPhoneNumber = false;
    }).AddEntityFrameworkStores<AppDbContext>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<Role>>();
        await Initializer.CheckAdminUser(userManager, rolesManager);
    }


    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1"));
    }
    else
    {
        // обработка ошибок HTTP
        app.UseStatusCodePagesWithRedirects("/Error/Error{0}");

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // обработка ошибок HTTP
    app.UseStatusCodePagesWithRedirects("/Error/Error{0}");

    app.UseRouting();

    // подключение аутентификации
    app.UseAuthentication();
    app.UseAuthorization();

    // Сопоставляем маршруты с контроллерами
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Остановка программы, ошибка");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}