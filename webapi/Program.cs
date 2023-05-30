using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors();
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 2;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICalendarService, CalendarService>();

var app = builder.Build();
app.UseRateLimiter();
app.UseCors(builder =>
{
    builder.WithOrigins("http://ngayamlich.vn", "http://ngayamlich.online")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
