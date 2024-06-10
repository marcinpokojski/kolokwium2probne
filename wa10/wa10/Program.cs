using Microsoft.EntityFrameworkCore;
using wa10;
using wa10.Repositories;
using wa10.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ITravelRepository, TravelRepository>();
builder.Services.AddScoped<IEventService,EventService>();



builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();



app.Run();

// //1. Nugget doddac
// Microsoft.EntityFrameWorkCore
// Microsoft.EntityFrameWorkCore.SqlServer
// Microsoft.EntityFrameWorkCore.Design
// 2.
//     cd ./zad9
//     dotnet new tool-manifest
// dotnet tool install dotnet-ef
// dotnet ef migrations add InitialCreate
//     dotnet ef database update
//
//
//     dotnet ef migrations add InitialCreate
// dotnet ef database update

//PAMIETAC O DBSETACH w contexcie
//konstruktory w context na public 
//PAMIETAC O KASKADOWYM USUWANIU 

//Wytwornia - Album
//wytwornia - jeden
// album wiele

//WYTWORNIA
//public Icollection<album> Albumy

//aLBUM
// [fOREGINKEY[NAMEOF()))]
//public Wytwornia wytwornie

//hasone wytwornie
//with many albumy
// hasforeginkey  idwytwornia
//on delete cascade

