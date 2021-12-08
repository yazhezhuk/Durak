using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Durak.Core.Game;
using Durak.Core.Game.Fields;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure
{
	public class Context : DbContext
	{
		public IMediator _mediator;

		public Context(IMediator mediator)
		{
			_mediator = mediator;
		}

		public DbSet<Deck> Decks { get; set; }
		public DbSet<PlayerHand> PlayerHands { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Field> Fields { get; set; }
		public DbSet<Game> Games { get; set; }

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			// ignore events if no dispatcher provided

			var entitiesWithEvents = ChangeTracker
				.Entries()
				.Select(e => e.Entity as BaseEntity)
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
}
