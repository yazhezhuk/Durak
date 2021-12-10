using Durak.Core.GameModels.Players;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Durak.Infrastructure.Data;

public class PlayersDbContext : IdentityDbContext<User>
{

	public PlayersDbContext(DbContextOptions<PlayersDbContext> options)
		: base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<User>().Metadata.SetTableName("Users");
		var appUser = new User {
			Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
			Email = "zhukovets@gmail.com",
			EmailConfirmed = true,
			UserName = "vitalich",
			NormalizedUserName = "VITALICH"
		};

		var ph = new PasswordHasher<User>();
		appUser.PasswordHash = ph.HashPassword(appUser, "bruh");

		modelBuilder.Entity<User>().HasData(appUser);
	}


}
