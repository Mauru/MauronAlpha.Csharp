namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {

	public class CustomizedName : GameComponent {
		public CustomizedName(string name)
			: base() {
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

		public GameName() : base() { }
		public GameName(string name): this() {
			ByPlayer = new CustomizedName(name);
		}

		CustomizedName ByPlayer;
		GeneratedName Generated = new GeneratedName();

		public string AsString {
			get {
				if (ByPlayer == null)
					return Generated.AsString;
				else
					return ByPlayer.AsString;
			}
		}


	}

}
