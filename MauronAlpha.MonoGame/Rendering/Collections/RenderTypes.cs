namespace MauronAlpha.MonoGame.Rendering.Collections {

	using MauronAlpha.MonoGame.Rendering.DataObjects;

	public class RenderTypes:MonoGameComponent {

		public static RenderType_Shape Shape { get { return RenderType_Shape.Instance; } }

		public static RenderType_Sprite Sprite { get { return RenderType_Sprite.Instance; } }

		public static RenderType_Line Line { get { return RenderType_Line.Instance; } }

		public static RenderType_Undefined Undefined { get { return RenderType_Undefined.Instance; } }

	}

}
