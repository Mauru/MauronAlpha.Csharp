namespace MauronAlpha.MonoGame.DataObjects {
	

	public abstract class BlendMode:MonoGameComponent {

		public abstract string Name { get; }

		public bool Euals(BlendMode other) {
			return Name.Equals(other.Name);
		}

	}

	/// <summary> Occludes objects behind it, no alpha channel </summary>
	public class BlendMode_Solid :BlendMode {

		public override string Name {
			get { return "Solid"; }
		}

		public static BlendMode_Solid Instance {
			get {
			return new BlendMode_Solid();
			}
		}
	}

	/// <summary> Subtracts from the object behind it </summary>
	public class BlendMode_Mask :BlendMode {

		public override string Name {
			get { return "Mask"; }
		}

		public static BlendMode_Mask Instance {
			get {
			return new BlendMode_Mask();
			}
		}
	}

	/// <summary> GreyScale on Alpha channel means transparency </summary>
	public class BlendMode_Alpha :BlendMode {

		public override string Name {
			get { return "Alpha"; }
		}

		public static BlendMode_Alpha Instance {
			get {
			return new BlendMode_Alpha();
			}
		}
	}

}
