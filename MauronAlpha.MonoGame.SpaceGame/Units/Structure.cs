namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Structure:GameComponent {

		public GameList<BuildingComponent> Cost;
		public GameList<BuildingComponent> UpKeep;
		public GameList<BuildingComponent> Maintenance;

		public GameList<Technology> RequiredTech;

	}
}
