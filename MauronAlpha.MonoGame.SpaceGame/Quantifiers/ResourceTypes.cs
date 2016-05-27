using System;
namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	public class ResourceType : GameComponent {


	}

	public class Food : ResourceType {
		public static Food Instance {
			get {
				return new Food();
			}
		}
	}
	public class Ammo : ResourceType { }
	public class Energy : ResourceType {

		public static Energy Instance {
			get {
				return new Energy();
			}
		}
	}
	public class Mineral : ResourceType { }
	public class Component : ResourceType { }
	public class Material : ResourceType { }
	public class Culture : ResourceType { }
	public class RawEnergy : ResourceType {

		public static RawEnergy Instance {
			get {
				return new RawEnergy();
			}
		}

	}
	public class Crew : ResourceType { }

}
