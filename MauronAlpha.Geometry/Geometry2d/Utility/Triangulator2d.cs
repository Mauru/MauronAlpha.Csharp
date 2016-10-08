using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;

using MauronAlpha.HandlingData;

using System;

namespace MauronAlpha.Geometry.Geometry2d.Utility {

	//Triangulation of a Polygon
	public class Triangulator2d:GeometryComponent2d {

		public MauronCode_dataList<Polygon2d> Triangulate(Polygon2d poly) {
			//Create a scrap copy
			Vector2dList points = poly.Points.Copy;
			Polygon2d tool = new Polygon2d(points);
			//Make sure it is oriented clockwise
			OrientClockWise(tool);
			//list to collect triangles
			MauronCode_dataList<Polygon2d> triangles = new MauronCode_dataList<Polygon2d>();

			//Remove ears until 3 points are left
			while (tool.Points.Count > 3)
				triangles = CycleEars(tool, triangles);
			
			return triangles.Add(tool);
		}
		public MauronCode_dataList<Polygon2d> Triangulate(Vector2dList points) {
			//Create a scrap copy
			Polygon2d tool = new Polygon2d(points);
			//Make sure it is oriented clockwise
			OrientClockWise(tool);
			//list to collect triangles
			MauronCode_dataList<Polygon2d> triangles = new MauronCode_dataList<Polygon2d>();

			//Remove ears until 3 points are left
			while (tool.Points.Count > 3)
				triangles = CycleEars(tool, triangles);

			return triangles.Add(tool);
		}

		public void FindEar(ref Polygon2d poly, ref int a, ref int b, ref int c) {

			Vector2dList pts = poly.Points;
			int count = pts.Count;
			for(a=0; a < count; a++) {
				b = (a+1) % count;
				c = (b+1) % count;

				if(IsEar(pts,a,b,c)) 
					return;
			}

			//no ear found (shouldnt happen)
			return;
		}
		public MauronCode_dataList<Polygon2d> CycleEars(Polygon2d source, MauronCode_dataList<Polygon2d> collector) {
			int a = 0, b = 0, c = 0;
			FindEar(ref source, ref a, ref b, ref c);
			Polygon2d newEar = new Polygon2d(new Vector2dList(){
				FindPoint(source,a),
				FindPoint(source,b),
				FindPoint(source,c)
			});

			collector.Add(newEar);
			RemovePoint(source, b);
			return collector;
		}
		public Vector2d FindPoint(Polygon2d source, int point) {
			int pointCount = source.CountPoints;
			return source.Points.Value(point);
		}
		public Polygon2d RemovePoint(Polygon2d poly, int index) {
			Vector2dList points = poly.Points;
			points.RemoveByKey(index);
			poly.SetPoints(points);
			return poly;
		}

		public Vector2d Centroid(Polygon2d poly) {
			int count = poly.Points.Count;
			Vector2dList points = poly.Points.Instance;
			points.Add(points.LastElement.Instance);
			double x = 0;
			double y = 0;
			double factor;
			for (int i = 0; i < count; i++) {
				factor = points.Value(i).X * points.Value(i + 1).Y -
					points.Value(i + 1).X * points.Value(i).Y;
				x += (points.Value(i).X + points.Value(i + 1).X) * factor;
				y += (points.Value(i).Y + points.Value(i + 1).Y) * factor;
			}
			double polyArea = PolygonArea(poly);
			x/=(6*polyArea);
			y/=(6*polyArea);

			if(x<0){
				x=-x;
				y=-y;
			}
			return new Vector2d(x,y);
		}

		public bool IsEar(Vector2dList points, int a, int b, int c) {
			//Is ABC concave
			if (AngleABC(
				points[a],
				points[b],
				points[c])
			> 0)
				return false;

			//Form Triangle
			Polygon2d tri = new Polygon2d(new Vector2dList() { 
				points[a], 
				points[b],
				points[c]
			});

			//If the point lies in the triangle it is not an ear
			for (int i = 0; i < points.Count; i++) {
				if (i != a && i != b && i != c)
					if (IsInPolygon(tri, points[i]))
						return false;
			}

			return true;

		}
		public bool IsOrientedClockWise(Polygon2d poly) {
			return (SignedPolygonArea(poly) < 0);
		}
		public bool IsInPolygon(Polygon2d poly,Vector2d vector) {
			Vector2dList points = poly.Points;
			double angle = AngleABC(points.LastElement, vector, points.FirstElement);

			int count = points.Count;
			for (int i = 0; i < count - 1; i++)
				angle += AngleABC(points[i], vector, points[i+1]);

			return (Math.Abs(angle) > 0.000001);
		}

		public Polygon2d OrientClockWise(Polygon2d poly) {
			if (!IsOrientedClockWise(poly)) {
				Vector2dList points = poly.Points;
				points.SetIsReadOnly(false).Reverse();
				poly.SetPoints(points);
			}
			return poly;
		}
		
		public double DotProduct(Vector2d a, Vector2d b, Vector2d c) {
			Vector2d BA = new Vector2d(a.X - b.X, a.Y - b.Y);
			Vector2d BC = new Vector2d(c.X - b.X, c.Y - b.Y);
			return (BA.X * BC.X + BA.Y * BC.Y);
		}
		public double CrossProduct(Vector2d a, Vector2d b, Vector2d c) {
			Vector2d BA = new Vector2d(a.X - b.X, a.Y-b.Y);
			Vector2d BC = new Vector2d(c.X - b.X, c.Y - b.Y);
			return (BA.X * BC.Y - BA.Y * BC.X);

		}
		public double AngleABC(Vector2d a, Vector2d b, Vector2d c) {
			double dot = DotProduct(a, b, c);
			double cross = CrossProduct(a,b,c);
			double result = Math.Atan2(cross, dot);
			return result;
		}
		public double PolygonArea(Polygon2d poly) {
			return Math.Abs(SignedPolygonArea(poly));
		}
		public double SignedPolygonArea(Polygon2d poly) {
			int count = poly.Points.Count;
			Vector2dList points = poly.Points.Instance;
			points.Add(points.LastElement.Instance);

			double area = 0;
			for (int i = 0; i < count; i++) {
				area +=
					(points.Value(i + 1).X - points.Value(i).X) *
					(points.Value(i + 1).Y + points.Value(i).Y) / 2;
			}
			return area;
		}

		/// <summary> Pseudo-Singleton </summary>
		public static Triangulator2d Instance { get { return new Triangulator2d(); } }
	}
}
