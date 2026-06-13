using asp_core_3.auth.Filter;
using DataBase.DBcon;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Repositry;
using E_commerce.Repositry.CartUserRepositry;
using E_commerce.Repositry.FileSizeRepositry;
using E_commerce.Repositry.ItemPhotoRepsitry;
using E_commerce.Repositry.ItemRepspsitry;
using E_commerce.Repositry.ItemSizeRepsitry;
using E_commerce.Repositry.OrderHistoryRepositry;
using E_commerce.Repositry.UserRepsitry;
using E_commerce.Srevice;
using E_commerce.Srevice.ItemPhotoServies;
using E_commerce.Srevice.ItemSizeServies;
using E_commerce.Srevice.UserServies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using rafeeqeq.auth.jwt.interfaces;
using rafeeqeq.auth.jwt.Service;
using System.Text;
using V1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBC>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("task")));
builder.Services.AddScoped<IItem,ItemRepo>();
builder.Services.AddScoped<IItemPhoto, ItemPhotoRepo>();
builder.Services.AddScoped<IItemSize, ItemSizeRepo>();
builder.Services.AddScoped<IFileStorageService, S3FileStorageService>();
builder.Services.AddScoped<IFileStorageServicePDF, S3FileStorageServicePDF>();
builder.Services.AddScoped<ICategory, CategoryRepo>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<IChildCategory, ChildCategoryRepo>();
builder.Services.AddScoped<ChildCategoryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<loginserves>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<ICartUser, CartUserRepo>();
builder.Services.AddScoped<IOrderHistory, OrderHistoryRepo>();
builder.Services.AddScoped<IFileSize, FileSizeRepo>();


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        ),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddHttpClient();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder =>
    builder.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

