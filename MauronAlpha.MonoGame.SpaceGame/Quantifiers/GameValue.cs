namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	
	public class GameValue<T> : GameComponent, I_GameValue where T : ValueType {

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

	public interface I_GameValue { }

	public class ValueTypes : GameList<ValueType> {
		public ValueTypes() : base() { }
		public ValueTypes(ValueType c) : this() {
			Add(c);
		}
	}
	public abstract class ValueType : GameComponent {
		public abstract GameName Name { get; }
	}
	
	public class T_Percent : ValueType {

		public override GameName Name {
			get { return new GameName("Percent"); }
		}

	}

}
