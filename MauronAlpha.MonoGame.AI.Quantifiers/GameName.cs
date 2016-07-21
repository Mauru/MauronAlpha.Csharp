namespace MauronAlpha.MonoGame.Logic.DataObjects {
	using MauronAlpha.MonoGame.Logic.Interfaces;

	/// <summary> A short string that does not need escaping to be saved </summary>
	public class GameName :GameComponent {

		public GameName() { }
		public GameName(string s): this() {
			value = s;
		}

		string value;
		public string AsString { get { return value; } }
		public bool IsNull {
			get {
				return value == null;
			}
		}
		public bool IsUnassigned {
			get {
				return value == "#";
			}
		}

		public bool Equals(GameName other) {
			if(IsNull)
				return other.IsNull;
			else if(other.IsNull)
				return false;

			return value.Equals(other.value);
		}

	}

	/// <summary> Defines a data set used for logic </summary>
	public class GameValue<T> :GameComponent, I_GameValue where T :ValueType, new() {

		public T Type {	get { return new T(); }	}

		public GameValue(bool isNull): base() {
			B_IsNull = isNull;
		}
		public GameValue() : this(true) { }
		public GameValue(long number): this(false) {
			D_int = Type.CheckInt(number);
		}
		public GameValue(bool isNull, bool isInfiniteNegative, bool isInfinitePositive)	:this(isNull) {
			B_isInfinitePositive = isInfinitePositive;
			B_isInfiniteNegative = isInfiniteNegative;
		}
		public GameValue(bool isNull, int val) :this(isNull) {
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

		internal long D_int;
		public long ValueAsInt {
			get {
				if(D_int == null)
					return 0;
				return D_int;
			}
		}

		internal string D_string;
		public string ValueAsString {
			get {
				if(D_string == null)
					return "";
				return D_string;
			}
		}

		public long CompareToInt(long data) {
			return ValueAsInt.CompareTo(data);
		}
		public bool IsBiggerThan(long data) {
			return CompareToInt(data) == 1;
		}
		public bool IsBiggerThan(GameValue<T> other) {
			return ValueAsInt > other.ValueAsInt;
		}
		public bool IsSmallerThan(long data) {
			return CompareToInt(data) == -1;
		}
		public bool IsSmallerThan(GameValue<T> other) {
			return ValueAsInt < other.ValueAsInt;
		}

		public bool Equals(long data) {
			return CompareToInt(data) == 0;
		}
		public bool Equals(string str) {
			return ValueAsString.Equals(str);
		}
		public bool Equals(GameValue<T> other) {

			if(IsNull) {
				if(IsInt && other.IsInt)
					return true;
				if(IsString && other.IsString)
					return true;

				return false;
			}

			if(IsInt) {

				if(other.IsInt)
					return D_int == other.ValueAsInt;

				if(other.StringComparableToInt)
					return D_int == other.ValueAsInt;

				return false;
			}

			if(IsString) {
				if(other.IsString)
					return D_string == other.ValueAsString;
				if(other.IntComparableToString)
					return D_string == "" + other.ValueAsInt;

				return false;
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

		public GameValue<K> Sum<K>(GameValue<K> u, GameValue<K> o) where K :ValueType, new() {
			return new GameValue<K>(u.ValueAsInt + o.ValueAsInt);
		}
		public GameValue<T> Sum(GameValue<T> o) {
			return new GameValue<T>(this.ValueAsInt + o.ValueAsInt);
		}

		public string AsSaveData {
			get {
				T def = Type;
				if(IsNull)
					return def.SaveName + ":" + def.SaveName + ".NULL";

				if(B_isInfinitePositive)
					return def.SaveName + ":" + def.SaveName + ".INFPOS";

				if(B_isInfiniteNegative)
					return def.SaveName + ":" + def.SaveName + ".INFNEG";

				if(IsString) {
					string val = VT_String.Escape(ValueAsString);
					return def.SaveName + ":" + val;
				}

				return def.SaveName + ":" + def.SaveName + ".INFPOS";
			}
		}

		public GameValue<T> FromSaveData(string data) {

			return new GameValue<T>();

		}

		public ValueType ValueType {
			get { throw new System.NotImplementedException(); }
		}

		public GameName Name {
			get { throw new System.NotImplementedException(); }
		}

		public bool Equals(I_GameValue other) {
		throw new System.NotImplementedException();
		}
	}

	/// <summary>	Defines a DataTye	</summary>
	public abstract class ValueType : GameComponent {

		public ValueType() : base() { }
		public abstract GameName Name { get; }

		public virtual string SaveName {
			get {
				return Name.AsString;
			}
		}
		public static GameValue<T> NullAs<T>() where T : ValueType, new() {
			return new GameValue<T>(true);
		}

		public virtual long CheckInt(long number) {
			return number;
		}

		public virtual bool IsString {
			get {
				return false;
			}
		}
		public virtual bool IsInt {
			get {
				return true;
			}
		}
		public virtual bool IsLong {
			get {
				return false;
			}
		}
		public virtual bool IsCoordinate {
			get {
				return false;
			}
		}

		public virtual BaseValueType BaseType { get { return VT_Int.Instance; } }
	}

	/// <summary> A basic ValueTypeGroup determining what is comparable to another </summary>
	public abstract class BaseValueType :GameComponent {
		public abstract string Name { get; }
	}

	public class VT_Long :BaseValueType {

		public override string Name {
			get { return "Long"; }
		}

		private VT_Long() : base() { }
		public static VT_Long Instance {
			get { return new VT_Long(); }
		}
	}
	public class VT_Int :BaseValueType {

		public override string Name {
			get { return "Int"; }
		}

		private VT_Int() : base() { }
		public static VT_Int Instance {
			get { return new VT_Int(); }
		}
	}
	public class VT_Percent :BaseValueType {

		private VT_Percent() : base() { }
		public override string Name { get { return "Percent"; } }

		public static VT_Percent Instance {
			get { return new VT_Percent(); }
		}
	}
	public class VT_String :BaseValueType {
		public override string Name { get { return "String"; } }

		private VT_String() : base() { }
		public static VT_String Instance {
			get { return new VT_String(); }
		}

		public static string Escape(string str) {
			return str.Replace(@"\",@"\\");
		}
		public static string UnEscape(string str) {
			return str.Replace(@"\\",@"\");
		}

	}
	public class VT_Vector :BaseValueType { 
		public override string Name { get { return "Vector"; } }
		private VT_Vector() : base() { }
		public static VT_Vector Instance {
			get { return new VT_Vector(); }
		}

	}
	public class VT_Shape :BaseValueType { 
	
		public override string Name { get { return "Shape"; } }
		private VT_Shape() : base() { }
		public static VT_Shape Instance {
			get { return new VT_Shape(); }
		}

	}

}

namespace MauronAlpha.MonoGame.Logic {

	public class GameComponent :MonoGameComponent { }

}
	
namespace MauronAlpha.MonoGame.Logic.Interfaces {
	using MauronAlpha.MonoGame.Logic.DataObjects;


	public interface I_GameValue {
	
		ValueType ValueType { get; }
		GameName Name { get; }
		bool Equals(I_GameValue other);
		string AsSaveData { get; }
	}

		/// <summary>	Interface for savable data	</summary>
	public interface I_SaveData {
		bool IsString { get; }
		bool IsInt { get; }
		bool IsCoordinate { get; }

		int ValueAsInt { get; }
		float ValueAsFloat { get; }


	}
}
namespace MauronAlpha.MonoGame.Logic.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Logic.DataObjects;

	//A collection of ValueTypes
	public class ValueTypes : List<ValueType> {
		public ValueTypes() : base() { }
		public ValueTypes(ValueType c)	: this() {
			Add(c);
		}
	}

}
