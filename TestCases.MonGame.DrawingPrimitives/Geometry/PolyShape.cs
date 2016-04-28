using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.HandlingData;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Transformation;

using MauronAlpha.MonoGame.Interfaces;
using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.MonoGame.Collections;


namespace MauronAlpha.MonoGame.Geometry {

	//Basic shape class - a polygon
	public class PolyShape : Polygon2d, I_MonoShape {
		
		public ShapeDefinition Definition {
			get { return new ShapeType_poly(); }
		}

		public new Vector2dList Points {
			get {
				return base.TransformedPoints;
			}
		}

		public virtual TriangulationData RenderData {
			get {
				if (DATA_calculate == null)
					DATA_calculate = Triangulate();
				return DATA_calculate;
			}
		}
		private TriangulationData DATA_calculate;
		private TriangulationData Triangulate() {
			TriangulationData data = new TriangulationData();
			data.Polygon = this;
			data.Triangles = Vertices;
			data.Vertices = data.Triangles.AsPositionColor;
			return data;
		}
		
		public virtual TriangleList Vertices {
			get {
				return new TriangleList(this);
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
