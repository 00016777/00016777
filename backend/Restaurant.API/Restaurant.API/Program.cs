using Microsoft.OpenApi.Models;
using NSwag;
using Restaurant.Application;
using Restaurant.Application.Models.Identities.JWTSettings;
using Restaurant.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("JWTSetting"));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerDocument(options =>
{
    options.AddSecurity("Bearer Token",
        Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}."
        });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("defaultCors", op =>
    {
        op.AllowAnyHeader();
        op.AllowAnyMethod();
        op.AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("defaultCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
