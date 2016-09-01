using System;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.HandlingData;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

namespace MauronAlpha.MonoGame.Utility {

	public class LineBuilder:MonoGameComponent {

		Texture2D BlockTexture;

		public LineBuilder(GraphicsDevice device): base() {
			BlockTexture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
			BlockTexture.SetData(new[]{Color.White});
		}

		//Actual Drawing functions
		public LineBuilder DrawLine(SpriteBatch spriteBatch, Vector2d start, Vector2d end, Color color, double thickness) {
		
			//Distance
			double distance = start.Distance(end);

			// calculate the angle between the two vectors
			double angle = start.Angle(end);

			spriteBatch.Draw(BlockTexture, ConvertToVector2(start), null, color, (float)angle, Vector2.Zero, new Vector2((float)distance, (float)thickness), SpriteEffects.None, 0);

			return this;
		}
		public LineBuilder DrawCircle(SpriteBatch spriteBatch, Vector2d center, double radius, int sides, Color color, double thickness) {
			MauronCode_dataList<Vector2d> points = PointsOfCircle(center, radius, sides);

			//Append first point to create a full circle
			points.Add(points.LastElement);

			for (int n = 0; n < points.Count-1; n++) {
				Vector2d start = points.Value(n);
				Vector2d end = points.Value(n+1);

				DrawLine(spriteBatch, start, end, color, thickness);
			}
			DrawLine(spriteBatch, points.LastElement, points.FirstElement, color, thickness);

			return this;
		}
		public LineBuilder DrawArc(SpriteBatch spriteBatch, Vector2d center, double radius, double startAngle, double endAngle, int sides, Color color, double thickness) {
			MauronCode_dataList<Vector2d> points = PointsOfArc(center, radius, startAngle, endAngle, sides);
			for (int n = 0; n < points.Count - 1; n++) {
				Vector2d start = points.Value(n);
				Vector2d end = points.Value(n + 1);

				DrawLine(spriteBatch, start, end, color, thickness);
			}
			return this;
		}

		//Helper functions
		public MauronCode_dataList<Vector2d> PointsOfCircle(Vector2d center, double radius, int sides) {
			MauronCode_dataList<Vector2d> result = new MauronCode_dataList<Vector2d>();

			const double factor = 2 * Math.PI;
			double interval = factor / sides;

			for (double theta = 0; theta < factor; theta += interval) {
				Vector2d point = new Vector2d(
					radius * Math.Cos(theta),
					radius * Math.Sin(theta)
				).Add(center);
				result.Add(point);
			}

			return result;
		}
		public MauronCode_dataList<Vector2d> PointsOfArc(Vector2d center, double radius, double angleStart, double angleEnd, int sides) {
			MauronCode_dataList<Vector2d> result = new MauronCode_dataList<Vector2d>();
			const double radA = Math.PI/180;

			double distance=0;
			
			//total "distance" on circle to cover
			if (angleStart < angleEnd)
				distance = angleStart - angleEnd;
			else
				distance = Math.Abs(360 - angleStart)+angleEnd;

			//determine the distance from end to start where we need to add points
			double interval = distance / sides;

			for (int n = 0; n < sides; n++) {
				double angleNext = angleStart + n * interval;
				if (angleNext >= 360)
					angleNext = angleNext - 360;
				Vector2d next = new Vector2d(
					center.X + radius * Math.Cos(radA * angleNext),
					center.Y + radius * Math.Sin(radA * angleNext)
				);
				result.Add(next);
			}
			return result;
		}

		public Vector2 ConvertToVector2(Vector2d vector) {
			return new Vector2((float)vector.X, (float)vector.Y);
		}
		
	}


}
