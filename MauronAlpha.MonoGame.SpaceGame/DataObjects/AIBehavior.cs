using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	public class AIBehavior:GameComponent {
	}

	public class Combatant:Being {

		public Combatant(GameLocation location, Species species) : base(location, species) { }
		public GameParty Party;
	
	}
	public class CombatRole : AIBehavior { }

	public class Combat_meatWall : CombatRole {
		/* The actor will try to intercept any incoming damage with his own body */
	}

	public class Combat_intercept : CombatRole {
		/* The actor will try to intercept any incoming damage with skills and abilities */
	}

	public class Combat_survive : CombatRole {
		/* The actor will try to avoid all incoming damage at all costs, it will not dodge  stuff that it is immune against */
	}

	public class Combat_escape : CombatRole {
		/* the actor will try to escape the combat area, it will dodge attacks it is not immune against, if it can dispatch an enemy on the path to save movement it will */
	}

	public class Combat_assassin : CombatRole {
		/* the actor will dodge attacks it is not immune against, it will actively seek out priority targets to kill */
	}

	public class CombatRelation : GameComponent {

		public Combatant BiggestThreat;
		public Combatant ClosestEnemy;
		public Combatant ClosestAlly;

	}

	public interface I_CombatTarget : I_RelationParty {

		//bool IsBeing { get; }
		//bool IsEquipment { get; }
	
	}

	public class Attack<T> : GameComponent where T : AttackType { 
		Combatant Source;
		I_CombatTarget Target;
		DefenseStats Attacks;		
	}

	public class AttackType : GameComponent { }

	public interface I_SkillApplier:I_Being { }
	public interface I_SkillTarget {
		bool IsBeing { get; }
		bool IsGameValue { get; }
	}

	public class Skill : GameComponent {
		GameList<BaseStat> Attributes;
		Duration WindUp;
		Duration Cooldown;
	}

	public class BaseStat : GameValue<T_BeingStat> { }

	public class SkillLevel : GameValue<T_SkillLevel> { }
	public class T_SkillLevel : ValueType {
		public override GameName Name {
			get { return new GameName("SkillLevel"); }
		}
	}

	public class Effects : GameList<Effect> { }
	public class Effect : GameComponent, I_Modifier {
		public GameList<ValueType> Modifies;
		public Duration Duration;
	}

	public class Relations : GameList<Relation> {
	}
	public class Relation : GameComponent {

		public GameList<RelationTarget> Targets;
		public I_RelationParty Source;
		public GameValue<T_RelationStrength> Strength;

	}

	public class RelationTarget : GameObject, I_RelationParty {

		I_GameObject Source;
		public RelationTarget(I_GameObject source) : base() {
			Source = source;
		}

		public override GameName Name {
			get { return Source.Name; }
		}

		public override bool IsQuantity {
			get { return Source.IsQuantity; }
		}
		public override bool IsSentient {
			get { return Source.IsSentient; }
		}

		public override bool IsResource {
			get { return Source.IsResource; }
		}

		public override bool IsSkill {
			get { return Source.IsSkill; }
		}

		public override bool IsBeing {
			get { return Source.IsBeing; }
		}

		public override bool IsEquipment {
			get { return Source.IsEquipment; }
		}
	}

	public interface I_RelationParty {

		bool IsBeing { get; }
		bool IsEquipment { get; }


	}
	public interface I_RelationSource : I_RelationParty { }
	public interface I_RelationTarget : I_RelationParty { }
	public class T_RelationStrength : ValueType {
		public override GameName Name {
			get { return new GameName("RelationStrength"); }
		}
	}

	public class PlayerInstance : Being, I_ComplexBeing {

		public PlayerInstance(GameLocation location, Species species) : base(location, species) {
			D_origin = location;
		}

		public GameParty Party;
		public Effects SufferedModifiers;

		public Relations Relations;

		public GameLocation D_origin;
		public GameLocation Origin {
			get { return D_origin; }
		}

	}
}
