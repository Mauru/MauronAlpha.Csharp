using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	
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
		public bool IsOrganic = false;
		public bool IsSpeciesVariant = false;
		public bool IsMachine = false;
		public bool IsPool = false;
		public bool IsSentient = false;
		public bool IsEquipment = false;
		public bool IsResource = false;
		public bool IsQuantity = false;

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

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;


	public class SpeciesForm : GameComponent {
		
		
	
	}

	public class BodyPart : GameComponent {

		public bool CanGrasp;
		public bool CanThink;
		public bool IsMobility;
		public bool IsSustain;
		GameList<I_PhysicalActionFactor> Allows;
	}


}

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {

	public interface I_PhysicalActionFactor {

		Action Action { get; }
		GameValue<T_PhysicalFactor> Strength { get; }

	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {

	//Defines by how much a physical factor influences an ability
	public class T_PhysicalFactor:ValueType {
		public override GameName Name {
			get {
				return new GameName("PhysicalFactor");
			}
		}
	}

}