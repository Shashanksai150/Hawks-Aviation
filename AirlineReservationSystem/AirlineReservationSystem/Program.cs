using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using AirlineReservationSystem.Repository;
using Microsoft.Extensions.Configuration;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<HawksAvaitionDBContext>(options =>
{
    options.UseSqlServer(
        connectionString: "server=*********************;database=HawksAviationDb;Trusted_Connection=True" //builder.Configuration["ConnectionString:HawksAviationDB"]
    ); 
});

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7178",
            ValidAudience = "http://localhost:5178",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}); 

builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<BookingService, BookingService>();

builder.Services.AddTransient<IFlightRepository, FlightRepository>();
builder.Services.AddTransient<FlightService, FlightService>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<CustomerService, CustomerService>();

builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<AdminService, AdminService>();

builder.Services.AddTransient<IAirportRepository, AirportRepository>();
builder.Services.AddTransient<AirportService, AirportService>();

builder.Services.AddTransient<IExceptionRepository, ExceptionRepository>();
builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("EnableCORS");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
