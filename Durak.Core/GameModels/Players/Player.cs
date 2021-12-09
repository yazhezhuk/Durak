using Durak.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Durak.Core.GameModels.Players;

public class Player : IdentityUser, IRootEntity
{ }
