var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache(); // Session verilerini bellek üzerinde saklayacaðýz
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi (isteðe baðlý)
    options.Cookie.IsEssential = true; // Çerezlerin gerekli olduðunu belirtir
}); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()  // Tüm kaynaklardan gelen taleplere izin verir
               .AllowAnyMethod()  // Tüm HTTP metodlarýna izin verir
               .AllowAnyHeader(); // Tüm baþlýklara izin verir
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
