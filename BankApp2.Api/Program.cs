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


//Lägger på BankContext
builder.Services.AddDbContext<BankAppDataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("BankConn")));

//Lägger på Identity
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConn")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
 .AddEntityFrameworkStores<ApplicationDbContext>()
 .AddDefaultTokenProviders();


//Först talar vi om att vi skall använda authentication och att det skall vara via JWT
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   //Sedan sätter vi upp hur JWT skall fungera
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
           //Krypteringsnyckeln läggs under User secrets och hämtas därifrån

       };
   });

//Lägger på möjlighet för DI för mina interfejses
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


//lägger på för att komma åt HttpContext i layers
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Lägger på AutoMapper
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
