


namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;

	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Units;

	//Lowest common denominator of a species
	public abstract class SpeciesDefinition : GameComponent, I_SpeciesDefinition {

		GameRules DATA_rules;
		public SpeciesDefinition(GameRules rules) : base() {
			DATA_rules = rules;
		}

		public GameState GameState {
			get {
				return DATA_rules.GameState;
			}
		}

		public abstract SpeciesStats BaseStats {
			get;
		}

		public virtual bool IsOrganic { get { return true; } }
		public virtual bool IsMachine { get { return false; } }
		public virtual bool IsSpirit { get { return false; } }
		public virtual bool IsSentient { get { return false; } }
		public virtual bool CanUseEquipment { get { return IsSentient; } }

		public virtual GameValue<T_ChemistryState> DominantState {
			get {
				return T_ChemistryState.Organic;
			}
		}
		public virtual GameValue<T_ChemistryState> ActiveState {
			get {
				return T_ChemistryState.Organic;
			}
		}
		public virtual GameValue<T_ChemistryState> DeadState {
			get {
				return T_ChemistryState.Unknown;
			}
		}

		public abstract MovementTypes MovementTypes { get; }
	}

	public class RuleSet_Species : RuleSet {

		GameRules DATA_rules;

		public RuleSet_Species(GameRules rules) : base() {
			DATA_rules = rules;
		}


		public override GameName Name {
			get { return new GameName("Species"); }
		}
	}

	//Defines a node of a gamerule group
	public class GameRuleSubSet : GameRules {
	
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	
	public interface I_SpeciesDefinition {

		SpeciesStats BaseStats { get; }
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

	public class SpeciesStats : GameList<SpeciesStat> {

		Agility Agility = new Agility();
		Strength Strength = new Strength();
		Intellect Intellect = new Intellect();

	}

	public class MovementTypes : GameList<MovementType> { }

}

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

	public class SPECIES_machine : SpeciesDefinition { 
	
		//constructor
		public SPECIES_machine(GameRules rules) : base(rules) {	}
		public override bool IsOrganic { get { return false; } }
		public override bool IsMachine { get { return true; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return false; } }
		public override bool CanUseEquipment { get { return false; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Static(this); } }

		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Solid; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Solid; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Solid; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForMachines(this); }
		}
	
	}

	public class SPECIES_robot : SpeciesDefinition {

		SpeciesDefinition DATA_creator;

		//constructor
		public SPECIES_robot(GameRules rules, SpeciesDefinition creator): base(rules) {
			DATA_creator = creator;
		}
		public override bool IsOrganic { get { return false; } }
		public override bool IsMachine { get { return true; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return false; } }
		public override bool CanUseEquipment { get { return false; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Robot(this); } }

		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Solid; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Kinetic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Solid; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForRobots(this); }
		}
	}
	public class SPECIES_android : SpeciesDefinition {

		SpeciesDefinition DATA_creator;

		//constructor
		public SPECIES_android(GameRules rules, SpeciesDefinition creator): base(rules) {
			DATA_creator = creator;
		}
		public override bool IsOrganic { get { return false; } }
		public override bool IsMachine { get { return true; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return false; } }
		public override bool CanUseEquipment { get { return true; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Humanoid(this); } }

		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Solid; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Kinetic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Solid; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForAndroids(this); }
		}
	}
	public class SPECIES_synth : SpeciesDefinition {

		SpeciesDefinition DATA_creator;
		//constructor
		public SPECIES_synth(GameRules rules, SpeciesDefinition creator): base(rules) {
			DATA_creator = creator;
		}
		
		public override bool IsOrganic { get { return false; } }
		public override bool IsMachine { get { return true; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return true; } }
		public override bool CanUseEquipment { get { return true; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Humanoid(this); } }
		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Solid; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Kinetic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Solid; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForSynths(this); }
		}

	}

	public class SPECIES_plant : SpeciesDefinition {
		//constructor
		public SPECIES_plant(GameRules rules) : base(rules) { }

		public override bool IsOrganic { get { return true; } }
		public override bool IsMachine { get { return false; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return false; } }
		public override bool CanUseEquipment { get { return false; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Obstacle(this); } }
		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Organic; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Organic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Organic; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForPlants(this); }
		}
	}
	public class SPECIES_animal : SpeciesDefinition {
		//constructor
		public SPECIES_animal(GameRules rules) : base(rules) { }

		public override bool IsOrganic { get { return true; } }
		public override bool IsMachine { get { return false; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return false; } }
		public override bool CanUseEquipment { get { return false; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Creature(this); } }
		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Organic; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Kinetic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Organic; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForAnimals(this); }
		}
	}
	public class SPECIES_humanoid : SpeciesDefinition {
		//constructor
		public SPECIES_humanoid(GameRules rules) : base(rules) { }
		
		public override bool IsOrganic { get { return true; } }
		public override bool IsMachine { get { return false; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return true; } }
		public override bool CanUseEquipment { get { return true; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Humanoid(this); } }
		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Organic; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Kinetic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Organic; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForHumanoids(this); }
		}
	}

	public class SPECIES_SuperNatural : SpeciesDefinition {
		//constructor
		public SPECIES_SuperNatural(GameRules rules) : base(rules) { }
		public override bool IsOrganic { get { return true; } }
		public override bool IsMachine { get { return false; } }
		public override bool IsSpirit { get { return false; } }
		public override bool IsSentient { get { return true; } }
		public override bool CanUseEquipment { get { return true; } }
		public override MovementTypes MovementTypes { get { return DefaultMovementTypes.Demon(this); } }
		public override GameValue<T_ChemistryState> DominantState { get { return T_ChemistryState.Organic; } }
		public override GameValue<T_ChemistryState> ActiveState { get { return T_ChemistryState.Kinetic; } }
		public override GameValue<T_ChemistryState> DeadState { get { return T_ChemistryState.Spirit; } }

		public override SpeciesStats BaseStats {
			get { return base.GameRules.Species.BaseStatsForSuperNaturals(this); }
		}
	}
}