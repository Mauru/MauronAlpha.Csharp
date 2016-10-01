using MauronAlpha.MonoGame.Collections;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;

namespace MauronAlpha.MonoGame.Geometry {

	//A hexagon
	public class HexShape:PolyShape {

		public HexShape(GameManager game, Vector2d center, double radius):base(game) {
			Ngon2d shape = new Ngon2d(6, radius);
			Initialize(shape.Points);
			Matrix.SetTranslation(center);
		}

		public TriangleList Vertices {
			get {
				Vector2d centroid = Center;
				Vector2dList pts = TransformedPoints;

				TriangleList result = new TriangleList(new Triangle2d[6]{
					new Triangle2d(centroid,pts[0],pts[1]),
					new Triangle2d(centroid,pts[1],pts[2]),
					new Triangle2d(centroid,pts[2],pts[3]),
					new Triangle2d(centroid,pts[3],pts[4]),
					new Triangle2d(centroid,pts[4],pts[5]),
					new Triangle2d(centroid,pts[5],pts[0])
				});
				return result;
			}
		}
		
		public new Vector2d Center { 
			get {
			Vector2dList pts = TransformedPoints;
			Vector2d result = new Vector2d(
				pts[5].X + pts[2].X,
				pts[0].Y + pts[3].Y
			).Divide(2);
			return result;
		} }
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
