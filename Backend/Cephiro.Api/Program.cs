<<<<<<< HEAD
using listings_conf = Cephiro.Listings.Infrastructure;
using Cephiro.Listings.Infrastructure;
using Microsoft.OpenApi.Models;
//experimental
using MassTransit;
using Cephiro.Listings.Application;


=======
using Cephiro.Identity;
using Cephiro.Identity.Application;
using MassTransit;
>>>>>>> e0e781c61c4228bc84835fd6674e8323bb988dc3

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD
builder.Services.AddServices(builder.Configuration);


//Masstransit mediator
builder.Services.AddMediator(cfg =>
{
    cfg.AddListingMediator();
=======
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddMediator(cfg =>
{
    cfg.AddIdentityMediator();
>>>>>>> e0e781c61c4228bc84835fd6674e8323bb988dc3
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "cms", Version = "v1"}); 
    c.EnableAnnotations();
    c.CustomSchemaIds(x => x.FullName);
});
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //added might want to delete later 
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
