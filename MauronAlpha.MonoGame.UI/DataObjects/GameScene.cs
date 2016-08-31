namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	public class UIInfo :Polygon2dBounds {
		public UIInfo() : base(0,0) { }
	}

	/// <summary> Gives a renderer Information on what to render </summary>
	public abstract class GameScene :Drawable {

		public GameScene(GameManager game) : base(game,game.Engine.GameWindow.Bounds,RenderInstructions.Composite) { 
		}

	}



}

namespace MauronAlpha.MonoGame.UI.Presets {
	using MauronAlpha.MonoGame.UI.DataObjects;


	public class StatusScreen :GameScene {

		public StatusScreen(GameManager game) : base(game) {
			
		}

		
		public void PreRender(GameRenderer renderer) {

			

		}

	}

}

namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.TextProcessing.Units;

	using MauronAlpha.MonoGame.DataObjects;

	public class TextDisplay :UIElement {



		public TextDisplay(GameManager game) : base(game) { }



		public override MauronAlpha.Geometry.Geometry2d.Units.Polygon2dBounds Bounds {
			get { throw new System.NotImplementedException(); }
		}

		public override MonoGame.Collections.List<MauronAlpha.Geometry.Geometry2d.Shapes.Polygon2d> Shapes {
			get { throw new System.NotImplementedException(); }
		}

		public override MonoGame.Collections.List<MonoGame.DataObjects.MonoGameTexture> Sprites {
			get { throw new System.NotImplementedException(); }
		}

		public override MonoGame.DataObjects.MonoGameTexture Rendered {
			get { throw new System.NotImplementedException(); }
		}
	}
}