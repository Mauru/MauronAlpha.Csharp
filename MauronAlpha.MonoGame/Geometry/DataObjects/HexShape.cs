using MauronAlpha.MonoGame.Collections;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;

using MauronAlpha.MonoGame.Rendering.Collections;

namespace MauronAlpha.MonoGame.Geometry {

	//A hexagon
	public class HexShape:PolyShape {

		public HexShape(GameManager game, Vector2d center, double radius):base(game) {
			Ngon2d shape = new Ngon2d(6, radius);
		}


	}

	public class HexShapeProperties : ShapeDefinition {

		public override string Name { get { return "poly"; } }

		public override bool UsesSpriteBatch {
			get { return false; }
		}

		public override bool UsesVertices {
			get { return false; }
		}

		public override bool IsPolygon { 
			get { return true; } 
		}
	}

}
