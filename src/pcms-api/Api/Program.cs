using Api.ActionResults;
using Api.Filters;
using Application.Extensions;
using Hangfire;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
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

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));
builder.Services.AddMvc(options =>
{
    options.Filters.Add<RequestLoggingFilter>();
    options.Filters.Add<HttpGlobalExceptionFilter>();
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
    });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NLPCPFA API",
        Version = "v1",
        Description = "NLPCPFA API"
    });
    c.OperationFilter<SwaggerHeaderFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    c.CustomSchemaIds(x => x.FullName);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//var scope = app.Services.CreateScope();
//var jobService = scope.ServiceProvider.GetRequiredService<BackgroundService>();


//// Schedule jobs
//RecurringJob.AddOrUpdate(
//    "HandleFailedTransactions",
//    () => jobService.HandleFailedTransactions(),
//    Cron.MinuteInterval(5));
    

//RecurringJob.AddOrUpdate(
//    "UpdateBenefitEligibility",
//    () => jobService.UpdateBenefitEligibility(),
//    Cron.Monthly);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NLPCPFA v1");
        //c.RoutePrefix = string.Empty;
    });
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
