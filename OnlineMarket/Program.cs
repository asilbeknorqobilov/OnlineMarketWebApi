using Microsoft.EntityFrameworkCore;
using OnlineMarket;
using OnlineMarket.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=onlinemarket_db;Username=konstantinopol;Password=konstantinopol"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");


app.MapControllers();
app.UseHttpsRedirection();


app.Run();