using MauronAlpha.HandlingErrors;
using MauronAlpha.Geometry.Shapes;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Transformation;

using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Interfaces;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A concave or convex Polygon in 2d Space
	public class Polygon2d : GeometryComponent2d_shape, I_polygon2d<Polygon2d> {
		
		//constructor
		public Polygon2d() : base() { }
		public Polygon2d(Vector2dList points): this() {
			_points = points;
		}

		public double X {
			get { return Bounds.X; }
		}
		public double Y {
			get { return Bounds.Y; }
		}
		public double Width {
			get { return Bounds.Width ; }
		}
		public double Height {
			get { return Bounds.Height; }
		}
		public Vector2d Size {
			get { return Bounds.Size; }
		}

		Vector2dList _points;
		public Vector2dList Points { get { return _points; } }
		public Polygon2d SetPoints(Vector2dList points) {
			_points = points;
			return this;
		}

		public int Count {
			get {
				return _points.Count;
			}
		}

		public Polygon2d( Polygon2d s )	: base() {
			_points = s.Points;
		}

		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				if (_bounds != null)
					return _bounds;

				Vector2dList MinMax = _points.MinMax;
				_bounds =  Polygon2dBounds.FromMinMax(MinMax[0], MinMax[1]);
				return _bounds;
			}
		}
		public void SetBounds(Polygon2dBounds bounds) {
			_bounds = bounds;
		}

		public Vector2d Center {
			get {
				return Bounds.Center;
			}
		}

		//Equals
		public bool Equals( Polygon2d other ) {
			if(Id.Equals(other.Id))
				return true;			
			return _points.Equals(other.Points);
		}
		public bool Equals( I_polygonShape2d other ) {
			if(Id.Equals(other.Id))
				return true;
			return _points.Equals(other.Points);
		}

		public Segment2dList Segments {
			get {
				Vector2dList points = Points;
				int count = points.Count;

				if (count < 2)
					return Segment2dList.Empty;

				Segment2dList result = new Segment2dList();

				Vector2d buffer = null;

				Segment2d s;
				foreach (Vector2d p in points) {
					if (buffer == null)
						buffer = p;
					else { 
						s = new Segment2d(buffer, p);
						result.Add(s);
						buffer = p;
					}
				}

				s = new Segment2d(buffer, points[0]);
				result.Add(s);

				return result;
			}
		}

		public Polygon2d Copy {
			get {
				return new Polygon2d(this);
			}
		}
		I_polygonShape2d I_polygonShape2d.Copy {
			get { return new Polygon2d(this); }
		}
		
		public Polygon2d Instance {
			get {
				return new Polygon2d( this );
			}
		}

		public string AsString {
			get {
				return _points.AsString;

			}
		}

		/*
		public bool HitTestAlternative(Vector2d v) {
			//Coarse
            if (v.X < Bounds.Min.X 
				|| v.X > Bounds.Max.X 
				|| v.Y < Bounds.Min.Y 
				|| v.Y > Bounds.Max.Y)
				return false;

			//fine
			double e = (Bounds.Max.X-Bounds.Min.X)/100;
			Vector2d start = new Vector2d(Bounds.Min.X-e,v.Y);
			Vector2d end = v.Instance;
			Segment2d ray = new Segment2d(start,end);
			int intersections=0;
			//count intersections
			foreach(Segment2d s in Segments)
				if(s.Intersects(ray)) intersections++;

			if((intersections&1)==1)
				return true;

			return false;
		}
		public bool HitTest(Vector2d v){		
			//Coarse
			if( v.X<Bounds.Min.X||v.X>Bounds.Max.X||v.Y<Bounds.Min.Y||v.Y>Bounds.Max.Y )
				return false;

			bool result=false;

			Vector2dList points = TransformedPoints;

			double[] vx = new double[points.Count];
			double[] vy = new double[points.Count];

			for (int n = 0; n < points.Count; n++) {
				vx[n] = points.Value(n).X;
				vy[n] = points.Value(n).Y;
			}
			int i, j;
			for (i = 0, j = points.Count - 1; i < points.Count; j = i++) {
				if( ((vy[i]>v.Y)!=(vy[j]>v.Y))&&(v.X<(vx[j]-vx[i])*(v.Y-vy[i])/(vy[j]-vy[i])+vx[i])) { result=!result; }
			}
			return result;
		}*/
	}

	//Shape description for a polygon
	public sealed class ShapeType_polygon:ShapeType {
		#region singleton
		private static volatile ShapeType_polygon instance=new ShapeType_polygon();
		private static object syncRoot=new System.Object();
		//constructor singleton multithread safe
		static ShapeType_polygon ( ) { }
		public static ShapeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ShapeType_polygon();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "polygon"; } }

	}

}