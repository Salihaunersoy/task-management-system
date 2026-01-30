using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementSystem.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<TaskManagementDbContext>();
builder.Services.AddOpenApi();

var jwtKey   = builder.Configuration["JwtConfig:Key"];
var issuer   = builder.Configuration["JwtConfig:Issuer"];
var audience = builder.Configuration["JwtConfig:Audience"];

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowNext", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme			  = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata	  = false;
	options.SaveToken				  = true;	
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer           = true,
		ValidIssuer              = issuer,
		ValidateAudience         = true,
		ValidAudience            = audience,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
		ValidateLifetime         = true,
		ClockSkew                = TimeSpan.Zero 
	};
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowNext");

app.UseAuthentication();
app.UseAuthorization();  

app.MapControllers();

app.Run();
