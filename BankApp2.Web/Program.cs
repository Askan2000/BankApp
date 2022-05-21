global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.SessionStorage;
using BankApp2.Web.Models;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7019/") });

//Vi behöver berätta att vi ska kunna injecta IcustomerService i index.razor
builder.Services.AddHttpClient<ICustomerWebService, CustomerWebService>();
builder.Services.AddHttpClient<ITransactionWebService, TransactionWebService>();
builder.Services.AddHttpClient<IAccountTypeWebService, AccountTypeWebService>();
builder.Services.AddHttpClient<IAccountWebService, AccountWebService>();
builder.Services.AddHttpClient<ILoanWebService, LoanWebService>();


//Lägger till DI för authentication
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthenticationCore();
builder.Services.AddBlazoredSessionStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();  
