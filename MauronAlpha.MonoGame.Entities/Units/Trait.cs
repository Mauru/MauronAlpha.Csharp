using MauronAlpha.MonoGame.Entities.Collections;
namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Trait:EntityComponent {

		string STR_name;
		public string Name { get { return STR_name; } }
		public Trait() : base() { }
		public Trait(string name) : base() {
			STR_name = name;
			if (name == "Nothing")
				B_isNull = true;
		}

		bool B_isNull = false;
		public bool IsNull {
			get {
				return B_isNull;
			}
		}


		public bool Equals(Trait other) {
			return STR_name == other.Name;
		}
		public static Trait Default {
			get {
				return new Trait("Nothing");
			}
		}

		public Modifiers Modifers = new Modifiers();
	}

}
