using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nexer.Api.Configurations;
using Nexer.Business.Interfaces;
using Nexer.Business.Services;
using Nexer.Data.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Configuration.AddUserSecrets<Program>();

var secretPassword = builder.Configuration["SECRETPASSWORD"];
if (string.IsNullOrEmpty(secretPassword))
{
    throw new InvalidOperationException("Chaves não encontradas");
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection").Replace("{Secret}", secretPassword);
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<IExternalBillingService, ExternalBillingService>();

builder.Services.ResolveDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Nexer - ca-backend-test",
        Description = "API REST para gerenciar faturamento de clientes.",
        Contact = new OpenApiContact
        {
            Name = "Tulio Campos",
            Email = "tulio.koca@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "LinkedIn",
            Url = new Uri("https://www.linkedin.com/in/tulio-alberto-da-rocha-campos/")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.EnableAnnotations();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(option =>
{
    option.RouteTemplate = "docs/{documentName}/swagger.json";
    option.SerializeAsV2 = true;
});

app.UseSwaggerUI(option =>
{
    option.DocumentTitle = "Open API";
    option.RoutePrefix = "";
    option.SwaggerEndpoint("/docs/v1/swagger.json", "Open API");
    option.InjectStylesheet("/swagger-ui/custom.css");
    option.InjectJavascript("/swagger-ui/custom.js");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
