using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Session;
using Durak.Core.GameModels.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Durak.Infrastructure.Data;

public class GameContext : DbContext
{
	public IMediator _mediator;

	public GameContext(IMediator mediator)
	{
		_mediator = mediator;
	}

	public DbSet<Deck> Decks { get; set; }
	public DbSet<PlayerHand> PlayerHands { get; set; }
	public DbSet<Field> Fields { get; set; }
	public DbSet<Game> Games { get; set; }

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

	public override int SaveChanges()
	{
		return SaveChangesAsync().GetAwaiter().GetResult();
	}
}
