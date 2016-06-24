using MauronAlpha.MonoGame.HexGrid.Units;
using MauronAlpha.MonoGame.SpaceGame.Utility;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {

	public class GridMap : GameComponent {

		MapType Type;
		MapGenerator Control;

		public GridMap(MapGenerator generator)
			: base() {
			Grid = new DynamicGrid();
			Control = generator;
		}
		public DynamicGrid Grid;
		public GameList<GameLocation> Locations = new GameList<GameLocation>();
		
		//Methods
		public void Register(GameLocation location) {
			Locations.Add(location);
		}

	}

	public class Locations : GameList<GameLocation> { }

	public abstract class MapType : GameComponent { }

	public class MT_Universe : MapType {

		public LocationType ChildType;

	}

	public abstract class LocationType : GameComponent { }

}
