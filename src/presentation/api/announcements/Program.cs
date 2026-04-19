using Advent.Announcements.Api.Endpoints;
using Advent.Announcements.Application;
using Advent.Announcements.Infrastructure;
//using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAnnouncementsApplicationServices(builder.Configuration);
builder.Services.AddAnnouncementsInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapNoticeEndpoints();

app.Run();

