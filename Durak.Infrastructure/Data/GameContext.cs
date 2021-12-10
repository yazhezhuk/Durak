using System.Text.RegularExpressions;
using Durak.Core;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.GameModels.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Durak.Infrastructure.Data;

public class GameContext : IdentityDbContext
{
	public IMediator _mediator;

	public GameContext(DbContextOptions<GameContext> options, IMediator mediator)
	:base(options)
	{
		_mediator = mediator;
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	public DbSet<GameSession> GameSessions { get; set; }
	public DbSet<PlayerHand> PlayerHands   { get; set; }
	public DbSet<Player> Players           { get; set; }
	public DbSet<Field> Fields             { get; set; }
	public DbSet<Deck> Decks               { get; set; }
	public DbSet<Game> Games               { get; set; }



	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		// ignore events if no dispatcher provided

		var entitiesWithEvents = ChangeTracker
			.Entries()
			.Select(e => e.Entity as BaseEntity<int>)
			.Where(e => e?.Events != null && e.Events.Any())
			.ToArray();

		foreach (var entity in entitiesWithEvents)
		{
			var events = entity.Events.ToArray();
			entity.Events.Clear();
			foreach (var domainEvent in events)
			{
				await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
			}
		}


		return result;
	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//	modelBuilder.Entity<Card>().HasData(Helper.Game.InitialCardSet);

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

	public override int SaveChanges()
	{
		return SaveChangesAsync().GetAwaiter().GetResult();
	}
}
