using MauronAlpha.MonoGame.HexGrid.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Units {

	public class GameLocation:GameComponent {
		
	}

	public class MapLocation : GameLocation {

		public MapLocation(Hex hex)
			: base() {
			Hex = hex;
		}
		public MapLocation(Hex hex, StarSystem system)
			: base() {
			Hex = hex;
			System = system;
			Galaxy = system.Galaxy;
		}

		public Galaxy Galaxy;
		public StarSystem System;
		public Hex Hex;

		public bool Equals(MapLocation other) {
			return Hex.Coordinates.Equals(other.Hex.Coordinates);
		}

		public void SetSystem(StarSystem system) {
			System = system;
		}
	}

	public class LocationType : GameComponent { }

}
