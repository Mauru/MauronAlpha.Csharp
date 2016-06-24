namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {

	public class LogicalName : GameComponent {

		internal bool B_isNull = false;
		public LogicalName(bool isNull)	: base() {
			B_isNull = isNull;
		}
		public LogicalName(string name)	: this(false) {
			AsString = name;
		}
		public string AsString = "Logical.NULL";
		public bool Equals(LogicalName other) {
			if (IsNull) {
				if (other.IsNull)
					return true;
				return false;
			}
			return AsString.Equals(other.AsString);
		}
		public bool IsNull {
			get { return B_isNull; }
		}
		public bool Equals(string other) {
			if (IsNull)
				return false;
			return AsString == other;
		}
	
	}
	public class CustomizedName : GameComponent {
		public CustomizedName(string name): base() {
			AsString = name;
		}
		public string AsString;
	}
	public class GeneratedName : GameComponent {

		public string AsString {
			get {
				return "{Generated:" + Id + "}";
			}
		}
	
	}
	public class GameName : GameComponent {

		public GameName(bool isNull) : base() { }
		public GameName() : this(true) { }
		public GameName(string name): this(false) {
			Logical = new LogicalName(name);
		}

		CustomizedName Customized;
		GeneratedName Generated;
		public readonly LogicalName Logical;

		public bool Equals(GameName other) {
			return Logical.Equals(other.Logical);
		}

		public string AsString {
			get {
				if (Logical != null)
					return Logical.AsString;
				return new LogicalName(true).AsString;
			}
		}

		private bool B_isNull = false;
		public bool IsNull {
			get {				
				return B_isNull;
			}
		}

		private bool B_isGeneric = false;
		public bool IsGeneric {
			get { return B_isGeneric; }
		}
	}

}
