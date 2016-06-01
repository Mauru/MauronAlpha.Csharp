using System;
namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	public abstract class ResourceType : ValueType {

	}

	public class T_Food : ResourceType {

		public override GameName Name {
			get { return new GameName("Food"); }
		}

		public static T_Food Instance {
			get {
				return new T_Food();
			}
		}
	}
	public class T_Ammo : ResourceType {

		public override GameName Name {
			get { return new GameName("Ammo"); }
		}
	}
	public class T_Energy : ResourceType {

		public override GameName Name {
			get { return new GameName("Energy"); }
		}

		public static T_Energy Instance {
			get {
				return new T_Energy();
			}
		}
	}
	public class T_Mineral : ResourceType {
		public override GameName Name {
			get { return new GameName("Mineral"); }
		}
	}
	public class T_Component : ResourceType {
		public override GameName Name {
			get { return new GameName("Component"); }
		}
	}
	public class T_Material : ResourceType {
		public override GameName Name {
			get { return new GameName("Material"); }
		}
	}
	public class T_Culture : ResourceType {
		public override GameName Name {
			get { return new GameName("Culture"); }
		}
	}
	public class T_RawEnergy : ResourceType {

		public override GameName Name {
			get { return new GameName("RawEnergy"); }
		}

		public static T_RawEnergy Instance {
			get {
				return new T_RawEnergy();
			}
		}

	}
	public class T_Crew : ResourceType {
		public override GameName Name {
			get { return new GameName("Crew"); }
		}
	}

}
