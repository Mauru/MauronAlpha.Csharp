using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.MonoGame.Utility;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

using MauronAlpha.HandlingData;

using MauronAlpha.MonoGame.Resources;



namespace MauronAlpha.MonoGame.Actors {
	
	public class GameStage:GameActor {
		MauronCode_dataList<RenderLevel> Levels = new MauronCode_dataList<RenderLevel>();

		public override Vector2d Position { get { return new Vector2d(); } }

		public override RenderInstructions RenderInstructions {
			get { return new RenderInstructions_gameStage(this); }
		}

		public override Polygon2dBounds Bounds {
			get { return new Polygon2dBounds(SHAPE_maxSize); }
		}

		public bool IsInitialized {
			get {
				return SHAPE_maxSize != null
					&& Renderer != null;
			}
		}

		public GameStage(GameManager manager) : base(new RenderLevel(0,manager),manager) {}

		private Rectangle2d SHAPE_maxSize;

		public void SetSize( Vector2d maxWindowSize) {
			SHAPE_maxSize = new Rectangle2d(maxWindowSize);
		}

		public RenderLevel NewLevel {
			get {
				RenderLevel newLevel = new RenderLevel(Levels.Count,Manager);
				Levels.Add(newLevel);
				return newLevel;
			}
		}


	}

	public class RenderInstructions_gameStage : RenderInstructions {
		GameStage Source;

		public RenderInstructions_gameStage(GameStage source) : base() {
			Source = source;
		}
	}
}
