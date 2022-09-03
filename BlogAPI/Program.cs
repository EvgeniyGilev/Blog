using BlogAPI;
using BlogAPI.DATA.Context;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories;
using BlogAPI.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Debug("����� ����������");

try
{


    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Documentation\\docApi.xml");
    c.IncludeXmlComments(filePath);

});

    // ���������� �����������
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
        }
    };
});
    
// ����������� ������� �����������
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
builder.Services.AddSingleton<ITagRepository, TagRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();

//������ ����������� � ��. ������ ����������� ����� �� ������������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlite(connectionString), ServiceLifetime.Singleton);

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
}).AddEntityFrameworkStores<AppDBContext>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1"));

    }
    else
    {
        // ��������� ������ HTTP
        app.UseStatusCodePagesWithRedirects("/Error/Error{0}");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();

    }
    //app.UseHttpsRedirection();


    app.UseAuthentication();    // ����������� ��������������
    app.UseAuthorization();

    app.UseRouting();
    // ������������ �������� � �������������
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


    app.MapControllers();

    app.Run();

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "��������� ���������, ������");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}