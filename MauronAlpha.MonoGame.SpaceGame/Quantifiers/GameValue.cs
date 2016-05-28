namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	
	public class GameValue<T> : GameComponent where T : ValueType {

		public GameValue() : base() { }
		public int ValueAsInt {
			get {
				return 0;
			}
		}
		public int CompareToInt(int data) {
			return ValueAsInt.CompareTo(data);
		}
		public bool IsBiggerThan(int data) {
			return CompareToInt(data) == 1;
		}
		public bool IsSmallerThan(int data) {
			return CompareToInt(data) == -1;
		}
		public bool Equals(int data) {
			return CompareToInt(data) == 0;
		}
	
	}
	
	public abstract class ValueType : GameComponent {
		public abstract string Name { get; }
	}
	
	public class T_Percent : ValueType {

		public override string Name {
			get { return "Percent"; }
		}

	}

	public class T_PlanetSize : ValueType {
		public override string Name { get { return "PlanetSize"; } }
	}

	public class T_DistanceFromStarCentre : ValueType {

		public override string Name {
			get { return "DistanceFromCentre"; }
		}
	}

}
