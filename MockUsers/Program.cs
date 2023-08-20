using MockUsers.Services;
using MockUsers.UsersInteralSemiDb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container
builder.Services.AddSingleton<IRanomUserApiService, RandomUserApiService>();
builder.Services.AddSingleton<IUserDb, UserDb>();

// Add logging configuration
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
