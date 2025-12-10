using Microsoft.OpenApi.Models;
using CourseWork.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

// Додаємо сервіс для контролерів з представленнями
builder.Services.AddControllersWithViews();

// Додаємо підтримку сесій
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Час дії сесії
    options.Cookie.HttpOnly = true; // Захист від XSS
    options.Cookie.IsEssential = true; // Важливість cookie для сесії
});

// Налаштовуємо залежності для бази даних
builder.Services.AddDbContext<ApplicationDbContext>();

// Налаштування Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseWork API", Version = "v1" });
    c.DocInclusionPredicate((docName, description) => true);
    c.TagActionsBy(api => new[] { api.GroupName ?? "default" });
});

var app = builder.Build();

// Налаштовуємо конвеєр обробки запитів
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseStaticFiles(); // Підтримка статичних файлів
app.UseRouting();
app.UseSession(); // Додаємо сесії
app.UseAuthorization();

// Налаштовуємо маршрутизацію
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();