namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.MonoGame.Collections;

	public class MonoGameShape :MonoGameComponent {
		Polygon2d _polygon;
		public MonoGameShape(GameManager game): base() {}		
	}
}
