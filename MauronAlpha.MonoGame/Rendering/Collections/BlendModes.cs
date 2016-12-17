namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Collections;

	public class BlendModes:List<BlendMode> {

		public static BlendMode_Alpha Alpha {
			get {
				return BlendMode_Alpha.Instance;
			}
		}

		public static BlendMode_Solid Solid {
			get {
				return BlendMode_Solid.Instance;
			}
		}

		public static BlendMode_Mask Mask {
			get {
				return BlendMode_Mask.Instance;
			}
		}

		public static BlendMode_Subtract Subtract {
			get {
				return BlendMode_Subtract.Instance;
			}
		}

	}

}