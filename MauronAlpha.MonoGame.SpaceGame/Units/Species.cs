using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Species:GameComponent {

		public GameName Name;

		public Species(GameName name, I_SpeciesDefinition definition): base() {
			Name = name;
			Definition = definition;			
		}
		I_SpeciesDefinition Definition;
		public BaseStats BaseStats;
		public GameList<SpeciesModifier> Modifiers;
		public bool IsHabitable = false;
		public bool IsMoveTarget = false;
		public bool IsMovable = false;

	}
	public class T_SpeciesStat:ValueType {
		public override GameName Name {
			get { return new GameName("SpeciesStat"); }
		}
	}
	public class BaseStats : SpeciesStats {

		public Agility Agility;
		public Intellect Intellect;
		public Strength Strength;

		public bool IsModifiable { get { return false; } }

	}

	public class SpeciesModifier : Modifier<T_SpeciesStat> { }

}
