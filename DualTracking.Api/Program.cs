using Microsoft.AspNetCore.Identity;
using IdentityServer4.Stores;
using System.Security.Cryptography.X509Certificates;
using DualTracking.Database;
using DualTracking.Api.Stores;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

//For correct order of middlewares, see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddIdentity<Parent, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 3;
	options.Password.RequiredUniqueChars = 0;

	options.SignIn.RequireConfirmedEmail = false;
	options.SignIn.RequireConfirmedPhoneNumber = false;

	options.ClaimsIdentity.UserIdClaimType = "UserID";
})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

// Add IdentityServer services
builder.Services.AddSingleton<IClientStore, CustomClientStore>();

builder.Services.AddIdentityServer()
				 //.AddTemporarySigningCredential() // Can be used for testing until a real cert is available
				.AddDeveloperSigningCredential()
												  //.AddSigningCredential(new X509Certificate2(Path.Combine("..", "..", "certs", "IdentityServer4Auth.pfx")))
				.AddInMemoryApiResources(ResourceProvider.GetAllResources())
				//.AddInMemoryApiScopes(IdentityConfiguration)
				.AddAspNetIdentity<Parent>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(cfg =>
	{
		cfg.RequireHttpsMetadata = false;
		cfg.SaveToken = true;

		cfg.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidIssuer = "Usama",
			ValidAudience = "DualTrackers",
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DEF74CFD-1743-4438-8DF1-0F3056406508"))
		};
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Using Identity implies cookie authentication.
// Cookie authentication can also be added explicitly if not using Identity
// app.UseCookieAuthentication();

// Note that UseIdentityServer must come after UseIdentity in the pipeline
app.UseIdentityServer();


// Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
// External authentication middleware should come after app.UseIdentityServer (but before app.UseMvc) https://identityserver4.readthedocs.io/en/release/quickstarts/4_external_authentication.html

app.MapControllers();

app.Run();
