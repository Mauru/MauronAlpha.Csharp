using System;

namespace MauronAlpha.Layout.Layout2d.Position {

	public abstract class Layout2d_resizeMode:Layout2d_component,IEquatable<Layout2d_resizeMode> {

		//Constructor
		public Layout2d_resizeMode():base() { }

		public abstract string Name { get; }

		public bool Equals(Layout2d_resizeMode other) {
			return (Name == other.Name);
		}

		public static ResizeMode_fixed Fixed { get { return ResizeMode_fixed.Instance; } }
		public static ResizeMode_X X { get { return ResizeMode_X.Instance; } }
		public static ResizeMode_Y Y { get { return ResizeMode_Y.Instance; } }
		public static ResizeMode_XY XY { get { return ResizeMode_XY.Instance; } }
		public static Layout2d_resizeMode ByString(string mode) {
			switch (mode) {
				default:
					return Fixed;
				case "X":
					return X;
				case "Y":
					return Y;
				case "XY":
					return XY;
			}
		}
	}

	public class ResizeMode_fixed : Layout2d_resizeMode {
		private ResizeMode_fixed() : base() { }
		public static ResizeMode_fixed Instance {
			get { return new ResizeMode_fixed(); }
		}

		public override string Name { get { return "fixed"; } }

	}

	public class ResizeMode_Y : Layout2d_resizeMode {
		private ResizeMode_Y() : base() { }
		public static ResizeMode_Y Instance {
			get { return new ResizeMode_Y(); }
		}

		public override string Name { get { return "Y"; } }
	}

	public class ResizeMode_X : Layout2d_resizeMode {
		private ResizeMode_X() : base() { }
		public static ResizeMode_X Instance {
			get { return new ResizeMode_X(); }
		}

		public override string Name { get { return "X"; } }
	}

	public class ResizeMode_XY : Layout2d_resizeMode {
		private ResizeMode_XY() : base() { }

		public static ResizeMode_XY Instance {
			get { return new ResizeMode_XY(); }
		}
		public override string Name { get { return "XY"; } }
	}
}
