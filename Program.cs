using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using newsApi;
using newsApi.Enteties;
using newsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthorizationHandler, EditorOrOwnerRequirementAuthorizationHandler>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddIdentity<NewsUser, IdentityRole>().AddEntityFrameworkStores<NewsContext>()
    .AddDefaultTokenProviders();

//add auth
var config = builder.Configuration;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    }
);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("subscriberPolicy", policy =>
        policy.RequireRole("Subscriber"))
    .AddPolicy("editorPolicy", policy =>
        policy.RequireRole("Editor"))
    .AddPolicy("writerPolicy", policy =>
        policy.RequireRole("Writer"))
    .AddPolicy("editorOrOwnerPolicy", policy =>
        policy.Requirements.Add(new EditorOrOwnerRequirement()));


builder.Services.AddScoped<INewsRepo, NewsRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<NewsContext>(
    a =>
    {
        var connString = config.GetSection("ConnectionStrings:DbConnection").Value;
        a.UseSqlite(connString);
    });
builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NewsContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();