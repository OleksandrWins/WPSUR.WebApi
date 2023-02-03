using Microsoft.EntityFrameworkCore;
using WPSUR.Repository;
using WPSUR.Repository.Interfaces;
using WPSUR.Repository.Repositories;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
});
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatHubService, ChatHubService>();
builder.Services.AddScoped<ChatHub>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Enable policy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("Enable policy");

app.MapControllers();

app.UseAuthorization();

app.UseEndpoints(
    endpoints =>
    {
        endpoints.MapHub<ChatHub>("/chat");
    }
);  

app.Run();
