using Microsoft.EntityFrameworkCore;
using MyWebAPITest.Data;
using MyWebAPITest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductService, ProductService>();

//builder.Services.AddScoped<ICategoryResponsitory, CategoryResponsitoty>();
builder.Services.AddScoped<ICategoryResponsitory, CategoryRespositoryMemory>();
builder.Services.AddDbContext<MyTestDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
