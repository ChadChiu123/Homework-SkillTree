
using Homework.Data;
using Homework_SkillTree.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// �[�J MVC �A��
builder.Services.AddControllersWithViews();



// ���U MyBlogContext
builder.Services.AddDbContext<MyBlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkillTreeDatabase")));

// ���U TestDataService API
/builder.Services.AddScoped<TestDataService>();

var app = builder.Build();
// �����n��޽u�t�m
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Read}/{id?}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// �]�w����
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
// Add services to the container.
builder.Services.AddControllersWithViews();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
