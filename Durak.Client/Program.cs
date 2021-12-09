using System.Reflection;
using System.Text;
using Core.Infrastructure;
using Core.Infrastructure.Data;
using Core.Infrastructure.Integration;
using Durak.Core.Events;
using Durak.Core.GameModels.Players;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddRazorPages();
//SignalR
builder.Services.AddScoped<GameHub>();
builder.Services.AddSignalR();

//Cors settings
builder.Services.AddCors(options =>
{
	options.AddPolicy("ClientPermission", policy =>
	{
		policy.AllowAnyHeader()
			.AllowAnyMethod()
			.WithOrigins("http://localhost:3000")
			.AllowCredentials();
	});
});


//Logging misc
builder.Logging.AddConsole();

// For Entity Framework
builder.Services.AddDbContext<GameContext>(options =>
	options.UseSqlServer(connectionString:"Server=DESKTOP-E9ICKFV\\SQLEXPRESS;Initial Catalog = GameDb;User Id=chel;Password=bruh3228;"));
builder.Services.AddDbContext<PlayersDbContext>(options =>
	options.UseSqlServer("Server=DESKTOP-E9ICKFV\\SQLEXPRESS;Initial Catalog = UsersDb;User Id=chel;Password=bruh3228;"));
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(BaseEvent)));

// For Identity
builder.Services.AddIdentity<Player, IdentityRole>()
	.AddEntityFrameworkStores<PlayersDbContext>()
	.AddDefaultTokenProviders();


// Adding Authentication
builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
{
	options.SaveToken = true;
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidIssuer = "http://localhost:3000/",
		ValidAudience = "http://localhost:3000/",
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somesecretkey32228!"))
	};
});
builder.Services.AddAuthorization(options =>
{
	var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
		JwtBearerDefaults.AuthenticationScheme);

	defaultAuthorizationPolicyBuilder =
		defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

	options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
	app.UseSpaStaticFiles(new StaticFileOptions { RequestPath = "/ClientApp/build" });
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller}/{action}/{id?}");
});

app.UseCors("ClientPermission");
app.UseAuthentication();
app.MapHub<GameHub>("/gameHub");
app.UseSpa(spa =>
{
	spa.Options.SourcePath = "ClientApp";
	if (app.Environment.IsDevelopment())
	{
		spa.UseReactDevelopmentServer(npmScript: "start");
	}
});

app.Run();
