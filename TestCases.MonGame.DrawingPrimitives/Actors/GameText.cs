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

		public override Vector2d Position { get { return new Vector2d(); } }

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
			get {
				if (Font == null)
					return GameText.EmptyBounds;
				if(Text == null)
					return GameText.EmptyBounds;
				ResourceManager manager = base.Resources;
				Vector2d size = Font.MeasureText(Text, manager);
				MauronAlpha.Geometry.Geometry2d.Shapes.Rectangle2d shape = new MauronAlpha.Geometry.Geometry2d.Shapes.Rectangle2d();
			}
		}
		public static Polygon2dBounds EmptyBounds {
			get {
				MauronAlpha.Geometry.Geometry2d.Shapes.Rectangle2d empty = new MauronAlpha.Geometry.Geometry2d.Shapes.Rectangle2d();
				return empty.Bounds;
			}
		}
	}

	public class RenderInstructions_gameText : RenderInstructions {
		private GameText Source;
		public RenderInstructions_gameText(GameText source):base() {
			Source = source;
		}
	}

}
