namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.MonoGame.Collections;

	public class MonoGameRectangle :Drawable {
		public MonoGameRectangle(GameManager game, Polygon2dBounds bounds) : base(game, bounds, RenderInstructions.Rectangle) { }
	}
	public sealed class Render_Rectangle :RenderInstruction { 
		public override string Name {
			get { return "Rectangle"; }
		}

		static object _sync= new System.Object();
		static volatile Render_Rectangle _instance;

		Render_Rectangle():base() {}

		public static Render_Rectangle Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new Render_Rectangle();
					}
				}
				return _instance;			
			}
		}
	}

}