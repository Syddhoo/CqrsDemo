using CqrsDemo.Behaviours;
using CqrsDemo.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsDemo.Middleware;
using CqrsDemo.Mappings;
using CqrsDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Keep these if your project uses Swashbuckle.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssembly(
    typeof(Program).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(EmployeeMappingProfile).Assembly);

builder.Services.AddSingleton<
    IApplicationInstanceService,
    ApplicationInstanceService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
