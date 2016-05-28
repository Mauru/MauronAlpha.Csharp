using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	//Where an effect applies
	public class BattleLayer:GameComponent {
	}

	public class SpaceBattle : GameComponent {




	}

	//A controllable entity in a battle
	public class BattleGroup : GameComponent {

		public GameParty Owner;
		public GameList<BattleUnit> Units;

	}

	public class BattleUnit : GameComponent {

		public GameParty Party;
		public BattleGroup Group;

	}

	//What "side" a unit is on
	public class GameParty:GameComponent {}

	public class BattleAction : GameComponent {

		public BuildingComponent Cost;
		public BuildingComponent Yield;

		public BattleLayer BattleLayer;
		public GameList<BattleEffect> EffectsFriendly;
		public GameList<BattleEffect> EffectsEnemy;
		public GameList<BattleEffect> EffectsSelf;

	}

	public class BattleEffect : GameComponent {

		public BattleLayer Layer;
		public ResourceAmount Substract;
		public ResourceAmount Add;
		public ResourceAmount ToRemove;

	}

	public class ShieldPoints : ResourceType { }
	public class ArmorPoints : ResourceType { }
	public class Morale : ResourceType { }
	public class HullPoints : ResourceType { }

	public class BattleDamage : BattleEffect { }

}
