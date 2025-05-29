
using Microsoft.EntityFrameworkCore;
using BackendApi.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 加入 MySQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34))
    ));


// 註冊 CORS 政策
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFrontend", policy =>
    {
        policy.WithOrigins("http://192.168.253.13:4500") // ✅ 允許 Angular 前端
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // 若要送 cookie 或 auth token
    });
});

// jwt 設定
var jwrSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwrSettings["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwrSettings["Audience"],

            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwrSettings["SecretKey"]!)),
            ValidateIssuerSigningKey = true
        };
    });


// DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInputValidatorService, UserInputValidatorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IGuardService, GuardService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IUserTableService, UserTableService>();


builder.Services.AddControllers();

var app = builder.Build();

// 這邊是初始化資料庫的資料
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    InitData.Seed(context);
}



//  加入中介層（middleware）：
// ✅ 放在 UseRouting 前後都可
app.UseCors("AllowAngularFrontend"); 


// 要加入這個後面Controller才能用驗證
// ⚠️ 放在 UseAuthorization 前面
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
