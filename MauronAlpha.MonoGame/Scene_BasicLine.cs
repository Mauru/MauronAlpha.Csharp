
namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Geometry.DataObjects;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Geometry;
	
	public class Scene_BasicLine:GameScene {

		public Scene_BasicLine(GameManager game) : base(game) { }

		public override void Initialize() {

			LineBuffer lines = new LineBuffer() {
				new MonoGameLine(1,1, 45, 200, 1, true )
				//new MonoGameLine(1,1,301,-299,1)
			};
			base.SetLineBuffer(lines);

			foreach (MonoGameLine line in lines) {
				System.Diagnostics.Debug.Print(line.Segment.AsString);
			}

			GameRenderer renderer = Game.Renderer;
			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(DrawMethod);

			base.Initialize();			
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return LineRenderer.DrawLines; }
		}
	}
}
