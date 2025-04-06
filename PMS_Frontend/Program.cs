var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache(); // Session verilerini bellek �zerinde saklayaca��z
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi (iste�e ba�l�)
    options.Cookie.IsEssential = true; // �erezlerin gerekli oldu�unu belirtir
}); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()  // T�m kaynaklardan gelen taleplere izin verir
               .AllowAnyMethod()  // T�m HTTP metodlar�na izin verir
               .AllowAnyHeader(); // T�m ba�l�klara izin verir
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
