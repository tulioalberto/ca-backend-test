using Microsoft.EntityFrameworkCore;
using Nexer.Api.Configurations;
using Nexer.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//.ConfigureApiBehaviorOptions(options =>
// {
//     options.SuppressModelStateInvalidFilter = true;
// });

// Adicionar a configuração de segredos de usuário
builder.Configuration.AddUserSecrets<Program>();

var secretPassword = builder.Configuration["SECRETPASSWORD"];
if (string.IsNullOrEmpty(secretPassword))
{
    throw new InvalidOperationException("Secret password not found in user secrets");
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection").Replace("{Secret}", secretPassword);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ResolveDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
