namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Module:GameComponent {}

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
