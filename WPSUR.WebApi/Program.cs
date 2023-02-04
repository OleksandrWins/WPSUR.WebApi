using Microsoft.EntityFrameworkCore;
using WPSUR.Repository;
using WPSUR.Repository.Interfaces;
using WPSUR.Repository.Repositories;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddTransient<IPostService, PostService>();
//builder.Services.AddTransient<IMainTagService, MainTagService>();
//builder.Services.AddTransient<ISubTagService, SubTagService>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IMainTagService, MainTagService>();
builder.Services.AddScoped<ISubTagService, SubTagService>();
builder.Services.AddScoped<IPostRepository, PostRepostitory>();
builder.Services.AddScoped<IMainTagRepository, MainTagRepository>();
builder.Services.AddScoped<ISubTagRepository, SubTagRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
