using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Shapes;
using MauronAlpha.Geometry.Geometry2d.Transformation;


namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A Shape in 2d Polygon Space
	public abstract class GeometryComponent2d_shape:GeometryComponent2d {

		//constructor
		public GeometryComponent2d_shape(ShapeType shapeType){
			SetShapeType(shapeType);
		}
		
		//The Type of the shape
		protected ShapeType ST_shapeType;
		public ShapeType ShapeType { get { return ST_shapeType; } }
		public GeometryComponent2d_shape SetShapeType(ShapeType shapeType) {
			ST_shapeType=shapeType;
			return this;
		}
		
		
		//public abstract Shape2d SetAsIdentity();
		public abstract Rectangle2d Bounds { get; }
		public abstract Vector2d Position { get; }
		public abstract Matrix2d Matrix { get; }
		public abstract Vector2d Scale { get; }
		public abstract double Rotation { get; }
		public abstract Vector2d Center { get; }
		public abstract double X { get; }
		public abstract double Y { get; }

	}



}
