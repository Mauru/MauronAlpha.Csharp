namespace MauronAlpha.MonoGame.UI.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.UI.DataObjects;

	public class BlendModes :List<BlendMode> {
		public static BlendMode_Solid Solid { get { return BlendMode_Solid.Instance; } }
		public static BlendMode_Alpha Alpha { get { return BlendMode_Alpha.Instance; } }
		public static BlendMode_None None { get { return BlendMode_None.Instance; } }
	}

}


namespace MauronAlpha.MonoGame.UI.DataObjects {

	public abstract class BlendMode :UIComponent {
		public abstract string Name { get; }

		public bool Equals(BlendMode other) {
			return Name.Equals(other.Name);
		}
	}

	public class BlendMode_Solid :BlendMode {

		public override string Name { get { return "Solid"; } }

		public static BlendMode_Solid Instance {
			get { return new BlendMode_Solid(); }
		}
	}

	public class BlendMode_Alpha :BlendMode {

		public override string Name { get { return "Alpha"; } }

		public static BlendMode_Alpha Instance {
			get { return new BlendMode_Alpha(); }
		}
	}

	public class BlendMode_None :BlendMode {

		public override string Name { get { return "None"; } }

		public static BlendMode_None Instance {
			get { return new BlendMode_None(); }
		}
	}

}