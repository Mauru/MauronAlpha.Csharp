namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.MonoGame.Collections;

	public class MonoGameText :Drawable {
		public MonoGameText(GameManager game, Polygon2dBounds bounds) : base(game, bounds, RenderInstructions.Text) { }
	}
	public sealed class Render_Text :RenderInstruction { 
		public override string Name {
			get { return "Text"; }
		}

		static object _sync= new System.Object();
		static volatile Render_Text _instance;

		Render_Text() : base() { }

		public static Render_Text Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new Render_Text();
					}
				}

				return _instance;
			}
		}
	}

}