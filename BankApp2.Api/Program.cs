using BankApp2.Core.Interfaces;
using BankApp2.Core.Profiles;
using BankApp2.Core.Services;
using BankApp2.Data.Interfaces;
using BankApp2.Data.Models;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Data.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//L�gger p� BankContext
builder.Services.AddDbContext<BankAppDataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("BankConn")));

//L�gger p� Identity
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConn")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
 .AddEntityFrameworkStores<ApplicationDbContext>()
 .AddDefaultTokenProviders();


//F�rst talar vi om att vi skall anv�nda authentication och att det skall vara via JWT
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   //Sedan s�tter vi upp hur JWT skall fungera
   .AddJwtBearer(opt =>
   {
       opt.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = "http://localhost:7019",
           ValidAudience = "http://localhost:7019",
           IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTkey"]))
           //Krypteringsnyckeln l�ggs under User secrets och h�mtas d�rifr�n

       };
   });

//L�gger p� m�jlighet f�r DI f�r mina interfejses
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<ITransactionRepo, TransactionRepo>();
builder.Services.AddTransient<IAccountRepo, AccountRepo>();
builder.Services.AddTransient<IAspNetUserService, AspNetUserService>();
builder.Services.AddTransient<IAspNetUserRepo, AspNetUserRepo>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountTypeService, AccountTypeService>();
builder.Services.AddTransient<IAccountTypeRepo, AccountTypeRepo>();
builder.Services.AddTransient<IDispositionService, DispositionService>();
builder.Services.AddTransient<IDispositionRepo, DispositionRepo>();
builder.Services.AddTransient<ILoanService, LoanService>();
builder.Services.AddTransient<ILoanRepo, LoanRepo>();


//l�gger p� f�r att komma �t HttpContext i layers
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//L�gger p� AutoMapper
builder.Services.AddAutoMapper(typeof(CustomerProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
