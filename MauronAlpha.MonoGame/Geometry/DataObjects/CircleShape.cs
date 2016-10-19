namespace MauronAlpha.MonoGame.Geometry {
	using MauronAlpha.MonoGame.Utility;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.HandlingData;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using System;


	//A circle like shape
	public class CircleShape:PolyShape, I_MonoShape {

		//Properties
		private double Radius;
		private int Sides;

		//Constructor
		public CircleShape(GameManager game, Vector2d center, double radius, int sides) : base(game) {
			Radius = radius;
			Sides = sides;
			Matrix.SetTranslation(center);
		}

		//Create the points of the circle
		public Vector2dList CreatePoints(double radius, int sides) {
			Vector2dList result = new Vector2dList();

			const double factor = 2 * Math.PI;
			double interval = factor / sides;

			for (double theta = 0; theta < factor; theta += interval) {
				Vector2d point = new Vector2d(
					radius * Math.Cos(theta),
					radius * Math.Sin(theta)
				);
				result.Add(point);
			}

			return result;
		}
	
	}

	//Shape Description
	public class ShapeType_circle:ShapeDefinition {

		public override string Name { get { return "circle"; } }

		public override bool UsesSpriteBatch {
			get { return false; }
		}

		public override bool UsesVertices {
			get { return true; ; }
		}

		public override bool IsPolygon {
			get { return true; }
		}
	
	}

}
