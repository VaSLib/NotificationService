using Application.Services;
using Domain.Interfaces.Services;
using Infrastructure.DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IRedisService, RedisService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
