
namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Geometry.DataObjects;
	using MauronAlpha.MonoGame.Geometry;

	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	//new MonoGameLine(1,1,301,-299,1) is a completely pixel perfect line (as reference)
	public class Scene_BasicLine:GameScene {

		public Scene_BasicLine(GameManager game) : base(game) { }

		public override void Initialize() {

			GraphicsDevice d = Game.Engine.GraphicsDevice;

			Vector2d start = Game.Renderer.CenterOfScreen;


			/*LineBuffer lines = new LineBuffer() {
				new MonoGameLine(start, 0, 100,1),
				new MonoGameLine(start, 45, 100,1),
				new MonoGameLine(start, 90, 100,1),
				new MonoGameLine(start, 135, 100,1),
				new MonoGameLine(start, 180, 100,1),
				new MonoGameLine(start, 225, 100,1),
				new MonoGameLine(start, 270, 100,1),
				new MonoGameLine(start, 315, 100,1)
			};*/
			I_polygonShape2d source = new Ngon2d(50,10);

			Segment2dList segments = source.Segments;

			LineBuffer lines = new LineBuffer(segments);
			lines.Offset(Game.Renderer.CenterOfScreen.Copy.Divide(2));
			lines.SetThickness(1);
			
			/*LineBuffer lines = new LineBuffer() {
				new Segment2d(0,0,0,200)
			};*/

			base.SetLineBuffer(lines);
			GameRenderer renderer = Game.Renderer;
			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(DrawMethod);

			base.Initialize();			
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return LineRenderer.DrawLines; }
		}

		public void Print(string message) {
			System.Diagnostics.Debug.Print(message);
		}
		public void Print(Vector2d v) {
			string message = "V["+v.X+","+v.Y+"]";
			Print(message);
		}
		public void Print(Rectangle r) {
			string message = "REC["+r.X+","+r.Y+" "+r.Width+":"+r.Height+"]";
			Print(message);
		}
		public void Print(Segment2d s) {
			string message = "S{" + s.Start.AsString + "," + s.End.AsString + "}";
			Print(message);
		}
	}
}
