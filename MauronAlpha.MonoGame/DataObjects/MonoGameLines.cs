namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.MonoGame.Collections;

	public class MonoGameLines :Drawable {
		public MonoGameLines(GameManager game, Polygon2dBounds bounds) : base(game, bounds, RenderInstructions.Lines) { }
	}
	public sealed class Render_Lines :RenderInstruction { 
		public override string Name {
			get { return "Lines"; }
		}

		static object _sync= new System.Object();
		static volatile Render_Lines _instance;

		Render_Lines():base() {}

		public static Render_Lines Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new Render_Lines();
					}
				}
				return _instance;			
			}
		}
	}
}