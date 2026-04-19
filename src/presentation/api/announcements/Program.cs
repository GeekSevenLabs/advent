using Advent.Announcements.Api.Endpoints;
using Advent.Announcements.Application;
using Advent.Announcements.Infrastructure;
using Advent.Announcements.Infrastructure.Contexts;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

builder.Services.AddAnnouncementsApplicationServices(builder.Configuration);
builder.Services.AddAnnouncementsInfrastructureServices(builder.Configuration);


var app = builder.Build();

#if DEBUG

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AnnouncementDbContext>();
    context.Database.EnsureCreated();
}

#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapNoticeEndpoints();

app.Run();

