using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Creature:Being {

		GeneratedName Name;
		CreatureBehavior Type;
		CreatureRole Role;

	}

	public class CreatureBehavior : EntityComponent {

		Chance Agressive;
		Chance Evade;

	}

	public class Chance : EntityValue<T_Percent> { }

	public abstract class CreatureRole:EntityComponent {
		public abstract string Name { get; }
	}

	public abstract class CreatureRole_prey:CreatureRole {
		public override string Name {
			get {
				return "Prey";
			}
		}
	}

	public abstract class CreatureRole_citizen : CreatureRole {
		public override string Name {
			get {
				return "Citizen";
			}
		}
	}

	public abstract class CreatureRole_predator : CreatureRole {
		public override string Name {
			get {
				return "Predator";
			}
		}
	}

	public abstract class CreatureRole_domesticated : CreatureRole {

	}

}
