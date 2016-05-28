namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Orbital<T> : GameComponent where T:OrbitalType {

		MapLocation Location;
		public Orbital(MapLocation location) : base() {
			Location = location;
		}
	
	}

	public class OrbitalType : GameComponent { }


	public class OT_Moon : OrbitalType { }
	public class OT_habitable : OrbitalType {}
	public class OT_Asteroid : OrbitalType { }
	public class OT_AsteroidBelt : OrbitalType { }
	public class OT_Comet : OrbitalType { }
	public class OT_Station : OrbitalType { }
	public class OT_Satelite : OrbitalType { }
	public class OT_Nebula : OrbitalType { }
	public class OT_Probe : OrbitalType { }
	public class OT_SpaceShip : OrbitalType {}
	public class OT_Debris : OrbitalType { }
	public class OT_Anomaly : OrbitalType { }

}
