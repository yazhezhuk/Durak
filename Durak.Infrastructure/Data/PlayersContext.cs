using Durak.Core.GameModels.Players;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Durak.Infrastructure.Data;

public class PlayersDbContext : IdentityDbContext<Player>
{

	public PlayersDbContext(DbContextOptions<PlayersDbContext> options)
		: base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Player>().Metadata.SetTableName("Players");
		var appUser = new Player {
			Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
			Email = "zhukovets@gmail.com",
			EmailConfirmed = true,
			UserName = "vitalich",
			NormalizedUserName = "VITALICH"
		};

		var ph = new PasswordHasher<Player>();
		appUser.PasswordHash = ph.HashPassword(appUser, "bruh");

		modelBuilder.Entity<Player>().HasData(appUser);
	}


}
