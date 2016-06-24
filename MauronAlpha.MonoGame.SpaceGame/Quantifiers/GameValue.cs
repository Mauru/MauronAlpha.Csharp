namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Utility;

	public class GameValue<T> : GameComponent, I_GameValue where T : ValueType, new() {

		public T Type {
			get {
				return new T();
			}
		}

		public GameValue(bool isNull): base() {
			B_IsNull = isNull;
		}
		public GameValue() : this(true) { }
		public GameValue(int number): this(false) {
			D_int = Type.CheckInt(number);
		}
		public GameValue(bool isNull, bool isInfiniteNegative, bool isInfinitePositive)	: this(isNull) {
			B_isInfinitePositive = isInfinitePositive;
			B_isInfiniteNegative = isInfiniteNegative;
		}
		public GameValue(bool isNull, int val): this(isNull) {
			D_int = Type.CheckInt(val);
		}

		public bool StringComparableToInt {
			get {
				return false;
			}
		}
		public bool IntComparableToString {
			get {
				return false;
			}
		}

		internal int D_int;
		public int ValueAsInt {
			get {
				if (D_int == null)
					return 0;
				return D_int;
			}
		}

		internal string D_string;
		public string ValueAsString {
			get {
				if (D_string == null)
					return "";
				return D_string;
			}
		}

		public int CompareToInt(int data) {
			return ValueAsInt.CompareTo(data);
		}
		public bool IsBiggerThan(int data) {
			return CompareToInt(data) == 1;
		}
		public bool IsBiggerThan(GameValue<T> other) {
			return ValueAsInt > other.ValueAsInt;
		}
		public bool IsSmallerThan(int data) {
			return CompareToInt(data) == -1;
		}
		public bool IsSmallerThan(GameValue<T> other) {
			return ValueAsInt < other.ValueAsInt;
		}

		public bool Equals(int data) {
			return CompareToInt(data) == 0;
		}
		public bool Equals(string str) {
			return ValueAsString.Equals(str);
		}
		public bool Equals(GameValue<T> other) {

			if (IsNull) {

				if (IsInt && other.IsInt)
					return true;
				if (IsString && other.IsString)
					return true;

				return false;
			}

			if (IsInt) {

				if (other.IsInt)
					return D_int == other.ValueAsInt;

				if (other.StringComparableToInt)
					return D_int == other.ValueAsInt;

			}

			if (IsString) {
				if (other.IsString)
					return D_string == other.ValueAsString;
				if (other.IntComparableToString)
					return D_string == "" + other.ValueAsInt;
			}



			return false;
		}

		internal bool B_IsNull;
		public bool IsNull {
			get {
				return (D_int == null && D_string == null);
			}
		}
		public bool IsString {
			get {
				return (D_string != null);
			}
		}
		public bool IsInt {
			get {
				return (D_int != null);
			}
		}

		internal bool B_isInfinitePositive = false;
		public bool IsInfitivePositive {
			get {
				return B_isInfinitePositive;
			}
		}
		internal bool B_isInfiniteNegative = false;
		public bool IsInfitiveNegative {
			get {
				return B_isInfiniteNegative;
			}
		}

		public GameValue<K> Sum<K>(GameValue<K> u, GameValue<K> o) where K:ValueType,new() {
			return new GameValue<K>(u.ValueAsInt + o.ValueAsInt);
		}
		public GameValue<T> Sum(GameValue<T> o) {
			return new GameValue<T>(this.ValueAsInt + o.ValueAsInt);
		}

		public string AsSaveData {
			get {
				T def = Type;
				if (IsNull)
					return def.SaveName + ":" + def.SaveName+".NULL";

				if (B_isInfinitePositive)
					return def.SaveName + ":" + def.SaveName + ".INFPOS";

				if (B_isInfiniteNegative)
					return def.SaveName + ":" + def.SaveName + ".INFNEG";

				if (IsString) {
					string val = SaveGameManager.EscapeString(ValueAsString);
					return def.SaveName + ":" + val;
				}

				return def.SaveName + ":" + def.SaveName + ".INFPOS";
			}
		}
	}

	public interface I_GameValue { }

	public class ValueTypes : GameList<ValueType> {
		public ValueTypes() : base() { }
		public ValueTypes(ValueType c)	: this() {
			Add(c);
		}
	}
	public abstract class ValueType : GameComponent {

		public ValueType() : base() { }
		public abstract GameName Name { get; }

		public virtual string SaveName {
			get {
				return Name.Logical.AsString;
			}
		}
		public static GameValue<T> NullAs<T>() where T : ValueType, new() {
			return new GameValue<T>(true);
		}

		public virtual int CheckInt(int number) {
			return number;
		}
	
	}

	public class T_Percent : ValueType {

		public override GameName Name {
			get { return new GameName("Percent"); }
		}

	}
	public class T_String : ValueType {
		public override GameName Name {
			get { return new GameName("String"); }
		}

	}
	public class T_Int : ValueType {
		public override GameName Name {
			get { return new GameName("Int"); }
		}

		public string NullAsString {
			get {
				return "NULL";
			}
		}
		public int NullAsInt {
			get {
				return 0;
			}
		}

		public bool CanBeNull {
			get {
				return true;
			}
		}
		public GameValue<T_Int> Default {
			get {
				return new GameValue<T_Int>(true);
			}
		}

	}
}

namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

	public class WeightedList<K, V> : Assign<GameValue<K>, GameList<V>> where K:ValueType,new() {

		public WeightedList<K, V> Set(GameValue<K> key, V value) {
			GameList<V> values = new GameList<V>();
			if (!base.TryFind(key, out values)) { 
				values.Add(value); 
				base.SetValue(key, values);
			}
			return this;
		}

	}

}
