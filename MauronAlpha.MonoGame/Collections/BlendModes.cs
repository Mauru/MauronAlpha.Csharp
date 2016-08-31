namespace MauronAlpha.MonoGame.Collections {
	using MauronAlpha.MonoGame.DataObjects;

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

	}

}