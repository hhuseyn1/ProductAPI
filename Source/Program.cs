using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Source.Data;
using Source.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
 policy.WithOrigins("https://localhost:1234", "http://localhost:7176").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op =>
{
    op.AddSecurityDefinition("oauth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Standart Authorization header using the Baerer scheme",
        In = ParameterLocation.Header,
        Name = "Auth",
        Type = SecuritySchemeType.ApiKey
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    {
        op.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eifrubediweubrgufenidurgbnfiedurgnifediirfnemokefmnekfkenfnekfenf")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<AppDbContext>(op => op
.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<ILoginRegisterService, LoginRegisterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
