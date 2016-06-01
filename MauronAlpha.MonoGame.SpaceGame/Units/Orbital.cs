namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Orbital<T> : GameObject where T:OrbitalType,new() {

		GameLocation Location;
		public Orbital(GameLocation location): base() {
			Location = location;
		}

		public OrbitalType Type {
			get {
				return new T();
			}
		}

		public override Quantifiers.GameName Name {
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsBeing {
			get {
				return Type.IsBeing;
			}
		}

		public override bool IsEquipment {
			get { return Type.IsEquipment; }
		}

		public override bool IsQuantity {
			get { return Type.IsQuantity; }
		}

		public override bool IsResource {
			get { return Type.IsResource; }
		}
	}

	public abstract class OrbitalType : GameComponent {

		public OrbitalType() : base() { }

		public virtual bool IsBeing {
			get { return false; }
		}

		public virtual bool IsEquipment {
			get { return false; }
		}

		public virtual bool IsQuantity {
			get { return false; }
		}

		public virtual bool IsResource {
			get { return false; }
		}
	}


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
