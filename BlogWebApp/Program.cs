using BlogWebApp;
using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Repositories;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BlogWebApp.BLL.Models.Entities;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Debug("����� ����������");

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddControllers();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" }); });

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

    builder.Services.Configure<PasswordHasherOptions>(options =>
        options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
    );


    builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
    builder.Services.AddSingleton<ITagRepository, TagRepository>();
    builder.Services.AddSingleton<IPostRepository, PostRepository>();

    //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    //builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
    //builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
    builder.Services.AddDbContext<AppDBContext>(ServiceLifetime.Singleton);




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

    // Configure the HTTP request pipeline.
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
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();    // ����������� ��������������
    app.UseAuthorization();
    //app.UseMvc();

    // ������������ �������� � �������������
    //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });


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