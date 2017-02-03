namespace MauronAlpha.Geometry.Geometry2d.Utility {
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	using MauronAlpha.HandlingData;

	//Triangulation of a Polygon
	public class Triangulator2d:GeometryComponent2d {
		/* ATTEMPT AT MAKING THIS CLASS STATIC - last checkmark : FindNextEar
		public static TriangleList2d Triangulate(I_polygonShape2d shape, Vector2d offset) {

			Vector2dList points = shape.Points.Copy;
			points.Offset(offset);

			points = OrientPointsClockWise(points);

			TriangleList2d result = new TriangleList2d();
			
			int remaining = points.Count;
			int index = 0;
			while (remaining > 3) {

				bool isEar = FindNextEar( points, ref index, ref result )

			}


		}
		public static bool IsOrientedClockWise(Vector2dList points) {
			return CalculateSignedPolygonArea(points) < 0;
		}
		public static Vector2dList OrientPointsClockWise(Vector2dList points) {
			if (IsOrientedClockWise(points))
				return points;
			points.Reverse();
			return points;
		}
		public static double CalculateSignedPolygonArea(Vector2dList points) {
			int count = points.Count;

			if(count == 0)
				return 0;

			Vector2d next = null;

			int index = 0;

			double result = 0;
			foreach(Vector2d current in points) {
				index++;
				if(!points.TryIndex(index, ref next))
					next = points.LastElement;				
				result += ( next.X - current.X ) * ( next.Y + current.Y ) / 2;
			}

			return result;
		}
		
		//TODO:
		public static bool FindNextEar(Vector2dList points, ref int index, ref TriangleList2d result) {

			int count = points.Count;

			// Not enough points for an ear
			if (count < 3)
				return false;

			int a = index, b = index + 1, c = index + 2;

			if (b >= count)
				b = b % count;

			if (c >= count)
				c = c % count;

			//we now have 3 points a b c which could be an ear
			Triangle2d ear = null;

			bool isEar = CheckIfEar(points, a, b, c, ref ear);

			if (isEar)
				result.Add(ear);

			
		}
		*/

		/// <summary>abc are three indexes in points, see if they are an ear and set result</summary>
		public static bool CheckIfEar(Vector2dList points, int a, int b, int c, ref Triangle2d result) {
			Vector2d va = null, vb = null, vc = null;
			if (!points.TryIndex(a, ref va))
				return false;
			if (!points.TryIndex(b, ref vb))
				return false;
			if (!points.TryIndex(c, ref vc))
				return false;

			double totalAngle = CalculateTotalAngle(va, vb, vc);
			
			//concave polygons are novalid ears
			if (totalAngle > 0)
				return false;

			int count = points.Count;

			result = new Triangle2d(va, vb, vc);

			//If the point lies in the triangle it is not an ear
			for (int i = 0; i < count; i++) {
				//ommit points which are the tringle
				if (i != a && i != b && i != c)
					if (HitTestPolygon(result.Points, points[i])) return false;
			}

			return true;		
		}
		public static bool HitTestPolygon(Vector2dList points, Vector2d vector) {

			int count = points.Count;

			if (count < 1)
				return false;

			Vector2d a = points.LastElement;
			Vector2d b = vector;
			Vector2d c = points.FirstElement;

			double angle = CalculateTotalAngle(points.LastElement, vector, points.FirstElement);

			for (int i = 0; i < count - 1; i++)
				angle += CalculateTotalAngle(points[i], vector, points[i + 1]);

			return (GeometryHelper2d.Abs(angle) > 0.000001);
		}

		public static bool IsConcave(Vector2d a, Vector2d b, Vector2d c) {
			return CalculateTotalAngle(a, b, c) > 0;
		}
		public static double CalculateTotalAngle(Vector2d a, Vector2d b, Vector2d c) {
			double dot = GeometryHelper2d.DotProduct(a, b, c);
			double cross = GeometryHelper2d.CrossProduct(a, b, c);
			double result = GeometryHelper2d.Atan2(cross, dot);

			return result;
		}

		//constructors
		public TriangleList2d Triangulate(Polygon2d poly) {
			//Create a scrap copy
			Vector2dList points = poly.Points.Copy;
			Polygon2d tool = new Polygon2d(points);
			//Make sure it is oriented clockwise
			OrientClockWise(tool);
			//list to collect triangles
			TriangleList2d triangles = new TriangleList2d();

			//Remove ears until 3 points are left
			while (tool.Points.Count > 3)
				triangles = CycleEars(tool, triangles);
			points = tool.Points;

			triangles.Add(new Triangle2d(points.Value(0),points.Value(1),points.Value(2)));
			return triangles;
		}
		public TriangleList2d Triangulate(Vector2dList points) {
			//Create a scrap copy
			Polygon2d tool = new Polygon2d(points);
			//Make sure it is oriented clockwise
			OrientClockWise(tool);
			//list to collect triangles
			TriangleList2d triangles = new TriangleList2d();

			//Remove ears until 3 points are left
			while (tool.Points.Count > 3)
				triangles = CycleEars(tool, triangles);
			Vector2dList pp = tool.Points;

			triangles.Add(new Triangle2d(pp.Value(0),pp.Value(1),pp.Value(2)));
			return triangles;
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
		public TriangleList2d CycleEars(Polygon2d source, TriangleList2d collector) {
			int a = 0, b = 0, c = 0;
			FindEar(ref source, ref a, ref b, ref c);
			Triangle2d newEar = new Triangle2d(
				FindPoint(source,a),
				FindPoint(source,b),
				FindPoint(source,c)
			);

			collector.Add(newEar);
			RemovePoint(source, b);
			return collector;
		}
		public Vector2d FindPoint(Polygon2d source, int point) {
			int pointCount = source.Count;
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
			Triangle2d tri = new Triangle2d(
				points[a], 
				points[b],
				points[c]
			);

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

			return (GeometryHelper2d.Abs(angle) > 0.000001);
		}

		public Polygon2d OrientClockWise(Polygon2d poly) {
			if (!IsOrientedClockWise(poly)) {
				Vector2dList points = poly.Points;
				points.SetIsReadOnly(false).Reverse();
				poly.SetPoints(points);
			}
			return poly;
		}
		public Triangle2d OrientClockWise(Triangle2d poly) {
			if (!IsOrientedClockWise(poly)) {
				Vector2dList points = poly.Points;
				points.SetIsReadOnly(false).Reverse();
				poly.SetPoints(points);
			}
			return poly;
		}

		public double AngleABC(Vector2d a, Vector2d b, Vector2d c) {
			double dot = GeometryHelper2d.DotProduct(a, b, c);
			double cross = GeometryHelper2d.CrossProduct(a, b, c);
			double result = GeometryHelper2d.Atan2(cross, dot);
			return result;
		}
		public double PolygonArea(Polygon2d poly) {
			return GeometryHelper2d.Abs(SignedPolygonArea(poly));
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

	}
}

