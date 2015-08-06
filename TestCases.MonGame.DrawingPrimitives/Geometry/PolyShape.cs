using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.HandlingData;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Transformation;

using MauronAlpha.MonoGame.Interfaces;

namespace MauronAlpha.MonoGame.Geometry {

	//Basic shape class - a polygon
	public class PolyShape : Polygon2d, I_MonoShape {
		
		public ShapeDefinition Definition {
			get { return new ShapeType_poly(); }
		}

		public new Polygon2dBounds Bounds {
			get { return new Polygon2dBounds(TransformedPoints); }
		}

		public new Vector2dList Points {
			get {
				return base.TransformedPoints;
			}
		}	
		
	}

	//Shape Description
	public class ShapeType_poly : ShapeDefinition {

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
