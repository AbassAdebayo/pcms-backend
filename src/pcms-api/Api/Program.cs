using Application.Extensions;
using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Database
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("ApplicationContext"));

builder.Services.AddLogging()
    .AddCors();

// Add services to the container.
builder.Services.AddServices()
    .AddApplication()
    .AddLogging()
    .AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});

app.MapControllers();

app.Run();
