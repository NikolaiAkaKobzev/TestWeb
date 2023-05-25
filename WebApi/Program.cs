using WebApi.ActionFilters;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStringStorage, StringStorage>();
builder.Services.AddTransient<INumbersInStringService, NumbersInStringService>();
builder.Services.AddScoped<IdValidationAction>();
builder.Services.AddScoped<SumOfNumberAction>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();
