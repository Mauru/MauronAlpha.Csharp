namespace MauronAlpha.MonoGame.Geometry {
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Geometry.Collections;
	using MauronAlpha.Geometry.Geometry2d.Utility;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	public class MonoShape2d:PolyShape,I_MonoShape {

		public MonoShape2d(GameManager game, Polygon2dBounds bounds):base(game) {
			base.SetPoints(bounds.Points);
			base.SetBounds(bounds);
		}

		public static MonoShape2d CreateRectangle(GameManager game, Polygon2dBounds bounds) {
			return new MonoShape2d(game, bounds);
		}


	}

	public class ShapeDefinition_MonoShape : ShapeDefinition {

		/// <summary> Pseudo-Singleton </summary>
		public static ShapeDefinition_MonoShape Instance {
			get { return new ShapeDefinition_MonoShape(); }
		}

		public override string Name {
			get { return "MonoShape"; }
		}

		public override bool UsesSpriteBatch {
			get { return false; }
		}

		public override bool UsesVertices {
			get { return true; }
		}

		public override bool IsPolygon {
			get { return true; }
		}
	}

}
