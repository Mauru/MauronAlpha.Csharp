using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Combatant {

		public bool CanRetreat;
		public bool CanAttack;
		public bool CanRetaliate;

		public Equipment Equipment;

		public GV_EvasionChance EvasionVs(Weapon w) {
			return new GV_EvasionChance();
		}

		public GameList<CombatTarget> Targetables;



	}

	public class Equipment : GameList<Equippable> { }

	public class Weapon : Equippable { }

	public class Module : Equippable {
		public bool Targetable { get { return true; } }
		ShieldPoints Shield;
		ArmorPoints Armor;
		BodyPoints Body;
	}

	public class Evasion<T> where T : Weapon { }

	public class GV_EvasionChance:GameValue<T_Percent> {}

	public class CombatTarget:GameComponent {}

	public class BodyPoints : GameValue<T_HitPoints> { }


	public class ModuleAbility : GameComponent {
		public GameList<MapAction> MapActions;
		public GameList<MapAction> BattleActions;
		public GameList<Factory> Factories;

		public GameList<BuildingComponent> Cost;
		public GameList<BuildingComponent> UpKeep;
		public GameList<BuildingComponent> Maintenance;

		public GameList<Technology> RequiredTech;
	}
}
