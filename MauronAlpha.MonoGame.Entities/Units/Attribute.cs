namespace MauronAlpha.MonoGame.Entities.Units {

	public class Attribute:Trait {

		public Attribute(string name) : base() {
			STR_name = name;
			if (name == "Nothing")
				B_isNull = true;
		}

		string STR_name;
		public string Name { get { return STR_name; } }

		public bool Equals(Attribute other) {
			return STR_name == other.Name;
		}

		bool B_isNull = false;
		public bool IsNull {
			get {
				return B_isNull;
			}
		}

		public static Attribute Default {
			get {
				return new Attribute("Nothing");
			}
		}
	}

}
