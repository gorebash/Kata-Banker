using BankingKata.Core;
using BankingKata.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string _originPolicy = "corsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _originPolicy,
                      policy =>
                      {
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.WithOrigins("http://localhost:4200");
                      });
});
builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer("name=ConnectionStrings:BankDb"));
builder.Services.AddScoped<IBankDataService, EfBankDataService>();
builder.Services.AddScoped<Bank>();
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors(_originPolicy);


//
using var scope = app.Services.CreateScope();

app.MapGet("/bank/statement", () =>
{
    var bank = scope.ServiceProvider.GetRequiredService<Bank>();

    return bank.GetLedger();
});

app.MapPost("/bank/deposit", (int amount) =>
{
    var bank = scope.ServiceProvider.GetRequiredService<Bank>();
    bank.Deposit(amount);
});

app.MapPost("/bank/withdraw", (int amount) =>
{
    var bank = scope.ServiceProvider.GetRequiredService<Bank>();
    bank.Withdraw(amount);
});



app.Run();