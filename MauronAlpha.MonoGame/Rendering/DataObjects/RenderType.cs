namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	/// <summary> Describes a renderBehavior </summary>
	public abstract class RenderType : MonoGameComponent {

		public abstract string Name { get; }
		public bool Equals(RenderType other) {
			return Name.Equals(other.Name);
		}

	}

	public class RenderType_Shape : RenderType {

		public override string Name {
			get { return "Shape"; }
		}

		private RenderType_Shape() : base() { }
		public static RenderType_Shape Instance {
			get {
				return new RenderType_Shape();
			}
		}

	}

	public class RenderType_Line : RenderType {

		public override string Name {
			get { return "Line"; }
		}

		private RenderType_Line() : base() { }
		public static RenderType_Line Instance {
			get {
				return new RenderType_Line();
			}
		}

	}

	public class RenderType_Sprite : RenderType {

		public override string Name {
			get { return "Sprite"; }
		}

		private RenderType_Sprite() : base() { }
		public static RenderType_Sprite Instance {
			get {
				return new RenderType_Sprite();
			}
		}

	}

	public class RenderType_Undefined : RenderType {

		public override string Name {
			get { return "Undefined"; }
		}

		private RenderType_Undefined() : base() { }
		public static RenderType_Undefined Instance {
			get {
				return new RenderType_Undefined();
			}
		}

	}

}