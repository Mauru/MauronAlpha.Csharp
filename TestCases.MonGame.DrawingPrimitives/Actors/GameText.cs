using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.MonoGame.Utility;
using MauronAlpha.MonoGame.Resources;
using MauronAlpha.Forms.Units;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.MonoGame.Actors {
	
	public class GameText:GameActor {
		GameFont Font;
		FormUnit_textField Text;

		public GameText(RenderLevel index, GameManager manager,string text, GameFont font) : base(index, manager) {
			Text = new FormUnit_textField(text);
			Font = font;
			RequestRender(this);
		}

		public Vector2d MaxSize = new Vector2d(0);

		public override RenderInstructions RenderInstructions {
			get {
				return new RenderInstructions_gameText(this);
			}
		}
		public override MauronAlpha.Geometry.Geometry2d.Units.Polygon2dBounds Bounds {
			get { return Font.MeasureText(DATA_text, Position, MaxSize, Resources); }
		}
	
	}

	public class RenderInstructions_gameText : RenderInstructions {
		private GameText Source;
		public RenderInstructions_gameText(GameText source):base() {
			Source = source;
		}
	}

}
