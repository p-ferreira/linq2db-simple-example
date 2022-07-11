using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using WebAPI_linq2db.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddLinqToDBContext<AppDataConnection>((provider, options) => {
    options
    //will configure the AppDataConnection to use
    //SQL Server with the provided connection string
    //there are methods for each supported database. 
    //NOTE: it's necessary to create the database beforehand
    .UseSqlServer("Server=localhost;Database=linq2db;Trusted_Connection=True;TrustServerCertificate=True;")
    
    //default logging will log everything using the ILoggerFactory configured in the provider
    .UseDefaultLogging(provider);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var dataConnection = scope.ServiceProvider.GetService<AppDataConnection>()!;
    //NOTE: If tables are not created yet, you can use the commands bellow to automatically create them    
    //dataConnection.CreateTable<Buyer>();
    //dataConnection.CreateTable<Address>();
    //dataConnection.CreateTable<PaymentMethod>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
