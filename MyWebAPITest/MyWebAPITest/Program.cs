using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebAPITest.Data;
using MyWebAPITest.Helpers;
using MyWebAPITest.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add Core:: 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// add settings 
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton<ProductService, ProductService>();

builder.Services.AddScoped<ICategoryResponsitory, CategoryResponsitoty>();
//builder.Services.AddScoped<ICategoryResponsitory, CategoryRespositoryMemory>();
builder.Services.AddScoped<IBookResponsitory, BookResponsitory>();
builder.Services.AddScoped<IUserResponsitory, UserResponsitory>();


builder.Services.AddDbContext<MyTestDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
});


// lay secretkey::
var secretkey = builder.Configuration.GetSection("AppSettings")["SecretKey"];
var secretKeyByte = Encoding.UTF8.GetBytes(secretkey);
// config Authorization 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // tu cap token 
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        // ky vao token 
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),

                        ClockSkew = TimeSpan.Zero
                    };
                });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
