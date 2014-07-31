﻿using System.Collections.Generic;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Shapes;
using MauronAlpha.Geometry.Geometry2d.Transformation;


namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A Shape in 2d Polygon Space
	public abstract class Shape2d:GeometryComponent2d {

		//constructor
		public Shape2d(ShapeType shapeType){
			SetShapeType(shapeType);
		}
		
		//The Type of the shape
		protected ShapeType ST_shapeType;
		public ShapeType ShapeType { get { return ST_shapeType; } }
		public Shape2d SetShapeType(ShapeType shapeType) {
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
