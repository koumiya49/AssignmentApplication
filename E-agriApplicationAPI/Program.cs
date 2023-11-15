using EcommerceApplication.Common.Data;
using EcommerceApplication.Common.Mappings;
using EcommerceApplication.Repositories;
using EcommerceApplication.Services;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<ILoginMapper, LoginMapper>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<ICartMapper, CartMapper>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddScoped<ITokenHandler, EcommerceApplication.Repositories.TokenHandler>();
builder.Services.AddScoped<IEmailServices,EmailService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAddressMapper, AddressMapper>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressServices, AddressServices>();




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a Valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }

    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme,new string[] {} }
    });
});


builder.Services.AddDbContext<assignmentDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))


    });
builder.Services.AddCors((corsOptions) =>
{
    corsOptions.AddPolicy("ECommerceApplication", (policyoptions) =>
    {
        policyoptions.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("ECommerceApplication");
app.UseAuthorization();
app.MapControllers();

app.Run();
