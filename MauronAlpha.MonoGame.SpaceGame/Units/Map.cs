using MauronAlpha.MonoGame.HexGrid.Units;
using MauronAlpha.MonoGame.SpaceGame.Utility;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Map : GameComponent {

		MapGenerator Control;

		public Map(MapGenerator generator): base() {
			Grid = new DynamicGrid();
			Control = generator;
		}
		public DynamicGrid Grid;
		public GameList<MapLocation> Locations = new GameList<MapLocation>();
		public void Register(MapLocation location) {
			Locations.Add(location);
		}

	}

}
