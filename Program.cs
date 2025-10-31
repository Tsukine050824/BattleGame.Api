using BattleGame.Api.Data;
using BattleGame.Api.Endpoints;
using BattleGame.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ===== Services =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký CORS (sửa được lỗi ICorsService)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// DI cho Dapper/MySQL
builder.Services.AddSingleton<MySqlDb>();
builder.Services.AddScoped<PlayerRepository>();
builder.Services.AddScoped<AssetRepository>();
builder.Services.AddScoped<ReportRepository>();

var app = builder.Build();

// ===== Middleware =====
app.UseSwagger();
app.UseSwaggerUI();

// Quan trọng: phải gọi UseCors sau khi build services & trước khi map endpoints
app.UseCors("AllowAll");

// ===== Endpoints =====
app.MapPlayerEndpoints();
app.MapAssetEndpoints();
app.MapReportEndpoints();

// In sẵn link Swagger khi app started
app.Lifetime.ApplicationStarted.Register(() =>
{
    // Sau khi khởi động, app.Urls sẽ có địa chỉ thực tế
    var urls = string.Join(", ", app.Urls);
    Console.WriteLine($"Swagger UI available at: {urls}/swagger");
});

app.Run();
