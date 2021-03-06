using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Durak.Client.Controllers;
using Durak.Client.Services;
using Durak.Core;
using Durak.Core.Events.ApplicationEvents;
using Durak.Infrastructure.Data;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Durak.Infrastructure.Integration;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder.SetIsOriginAllowed((str) => true)
			.AllowCredentials()
			.AllowAnyHeader());
});

builder.Services.AddControllers()
	.AddNewtonsoftJson(x =>
		x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
//SignalR
builder.Services.AddScoped<GameHubService>();
//Cors settings



//Logging mis

// For Entity Framework
builder.Services.AddDbContext<GameContext>(options =>
	options.UseSqlServer(connectionString: "Server=localhost\\SQLEXPRESS;Initial Catalog=GameDB;Trusted_Connection=True;"
	),ServiceLifetime.Singleton);

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
	.AddEntityFrameworkStores<GameContext>()
	.AddDefaultTokenProviders();
builder.Services.AddLogging(options =>
	options.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Critical + 1));
//application events
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(BaseApplicationEvent)));


//Data
builder.Services.AddScoped<IRepository<PlayerHand>, BaseEfRepository<PlayerHand>>();
builder.Services.AddScoped<IGameSessionRepository, GameSessionRepository>();
builder.Services.AddScoped<IRepository<GameCard>, BaseEfRepository<GameCard>>();
builder.Services.AddScoped<IRepository<Player>, BaseEfRepository<Player>>();
builder.Services.AddScoped<IRepository<Field>, BaseEfRepository<Field>>();
builder.Services.AddScoped<IRepository<Game>, BaseEfRepository<Game>>();
builder.Services.AddScoped<IRepository<Deck>,BaseEfRepository<Deck>>();

builder.Services.AddScoped<IFieldValidator, FieldValidator>();
builder.Services.AddScoped<IGameSessionService, GameSessionService>();
builder.Services.AddScoped<IMoveService, MoveService>();
builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();



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
		ValidIssuers = new List<string>
		{
			Helper.ApplicationOptions.DEFAULT_HOST,
			" http://localhost:3001"
		},
		ValidAudiences = new List<string>
		{
			Helper.ApplicationOptions.DEFAULT_HOST,
			" http://localhost:3001"
		},
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(Helper.ApplicationOptions.DEFAULT_SECRET))
	};
	options.Events = new JwtBearerEvents
	{
		OnMessageReceived = context =>
		{
			var accessToken = context.Request.Query["access_token"];

			// If the request is for our hub...
			var path = context.HttpContext.Request.Path;
			if (!string.IsNullOrEmpty(accessToken) &&
			    (path.StartsWithSegments("/gameHub")))
			{
				// Read the token out of the query string
				context.Token = accessToken;
			}
			return Task.CompletedTask;
		}
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
builder.Services.AddSignalR();
builder.Services.AddSpaStaticFiles(configuration =>
{
	configuration.RootPath = "ClientApp/build";
});


var app = builder.Build();
app.UseCors("AllowAllOrigins");

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
}
app.UseSpaStaticFiles(new StaticFileOptions { RequestPath = "/ClientApp/build" });

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller}/{action}/{id?}");
});


app.UseAuthentication();
app.MapHub<GameHubService>("/gameHub");
app.UseSpa(spa =>
{
	spa.Options.SourcePath = "ClientApp";
	if (app.Environment.IsDevelopment())
	{
		spa.UseReactDevelopmentServer(npmScript: "start");
	}
});

app.Run();
