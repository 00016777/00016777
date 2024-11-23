using Microsoft.AspNetCore.Builder;
using NSwag;
using NSwag.Generation.Processors.Security;
using Restaurant.Application;
using Restaurant.Application.Models.Identities.JWTSettings;
using Restaurant.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("JWTSetting"));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(options =>
{
    options.AddSecurity("JWT",
        Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Enter 'Bearer' followed by your JWT token in the text box below.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
        });

    options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
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
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseCors("defaultCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
