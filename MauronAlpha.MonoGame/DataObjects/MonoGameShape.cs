namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.MonoGame.Collections;

	public class MonoGameShape :Drawable {
		Polygon2d DATA_polygon;
		public MonoGameShape(GameManager game, Polygon2d poly): base(game, poly.Bounds, RenderInstructions.Shape) {
			DATA_polygon = poly;
		}		
	}
	public sealed class Render_Shape :RenderInstruction {
		public override string Name {
			get { return "Shape"; }
		}

		static object _sync= new System.Object();
		static volatile Render_Shape _instance;

		Render_Shape() : base() { }
	
		public static Render_Shape Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new Render_Shape();
					}
				}

				return _instance;
			}
		}
	}
}
