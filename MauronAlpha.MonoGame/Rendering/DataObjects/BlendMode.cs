﻿namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	

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

	/// <summary> Masks one object with another </summary>
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

	/// <summary> Subtract shape2 from shape1 </summary>
	public class BlendMode_Subtract : BlendMode {

		public override string Name {
			get { return "Subtract"; }
		}

		public static BlendMode_Subtract Instance {
			get {
				return new BlendMode_Subtract();
			}
		}
	}

}
