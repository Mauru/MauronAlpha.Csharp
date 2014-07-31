using System.Collections.Generic;
using System;

using MauronAlpha.Geometry.Geometry2d.Transformation;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Shapes;
using MauronAlpha.Geometry.Geometry2d.Utility;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A concave or convex Polygon in 2d Space
	public class Polygon2d:Shape2d {

		#region Comparison { Polygon2d }
		public bool Equals (Polygon2d p) {
			return ToString==p.ToString;
		}
		public new string ToString {
			get {
				string r="{";
				for( int i=0; i<Points.Length; i++ ) {
					Vector2d p=Points[i];
					r+="["+(i+1)+":"+p.ToString+"]";
				}
				return r+"}";
			}
		}
		#endregion

		#region Matrix
		protected Matrix2d M_matrix;
		public override Matrix2d Matrix { 
			get { 
				if (M_matrix==null) { 
					M_matrix = new Matrix2d(this);
				}
				return M_matrix; 
			}
		}
		protected Polygon2d SetMatrix(Matrix2d m) {
			//Reset Points to original
			Matrix.RemoveFrom(this);

			M_matrix=m;

			//Apply new Matrix
			Matrix.ApplyTo(this);

			return this;
		}
		#endregion
		#region Points

		//Points as handled on the inside (unmodified)


		//Points as displayed to the outside (modified by Matrix)
		protected List<Vector2d> A_points=new List<Vector2d>();		
		
		public Vector2d[] Points {
			get { return A_points.ToArray(); }
		}
		public Polygon2d SetPoints (Vector2d[] points) {
			//get the distance of the first point
			if(points.Length>0){
				Vector2d offset = new Vector2d().Difference(points[0]);
				Matrix.Translation.Add(offset);
			}
			A_points=new List<Vector2d>(points);
			Matrix.ApplyTo(this);
			return this;
		}
		#endregion
		#region Segments
		protected List<Segment2d> A_segments=new List<Segment2d>();
		public Segment2d[] Segments {
			get {
				if( A_segments==null ) {
					A_segments=BuildSegments(Points);
				}
				return A_segments.ToArray();
			}
		}
		public static List<Segment2d> BuildSegments (Vector2d[] points) {
			if( points.Length<1 ) {
				return new List<Segment2d>();
			}
			if( points.Length==1 ) {
				return new List<Segment2d>() { new Segment2d(points[0], points[0]) };
			}
			List<Segment2d> a_segments=new List<Segment2d>();
			for( int i=0; i<points.Length; i++ ) {
				Vector2d v=points[i];
				if( i%2==0&&points[i+1]!=null ) {
					Segment2d s=new Segment2d(points[i], points[i+1]);
					a_segments.Add(s);
				}
			}

			//final segment
			Segment2d f=new Segment2d(points[points.Length-1], points[0]);
			a_segments.Add(f);

			return a_segments;
		}
		private Polygon2d SetSegments(Segment2d[] s) {
			A_segments=new List<Segment2d>(s);
			return this;
		}
		#endregion

		#region Get a copy of the polygon
		public Polygon2d Instance {
			get {
				return new Polygon2d(this);
			}
		}
		public Polygon2d():base(ShapeType_polygon.Instance) {}
		public Polygon2d (Polygon2d s) : base(ShapeType_polygon.Instance) { }
		public Polygon2d FromShape(Polygon2d s) {
			foreach(Vector2d p in s.Points) {
				A_points.Add(p.Instance);
			}
			SetSegments(s.Segments);
			SetCenter(s.Center);
			SetBounds(s.Bounds);
			SetMatrix(s.Matrix);
			return this;
		}
		#endregion

		#region Get the center
		protected Vector2d V_center=null;
		public override Vector2d Center {
			get {
				if( V_center==null ) {
					Vector2d m=Bounds.Size;
					V_center=new Vector2d(m.X/2, m.Y/2);
				}
				return V_center.Instance;
			}
		}
		public void SetCenter (Vector2d v) {
			if( v==null ) {
				V_center=null;
				return;
			}
			Vector2d offset=Center.Difference(v);
			Move(v);
			V_center=v;		
		}
		#endregion
		#region Bounds
		protected Rectangle2d R_bounds=null;
		public override Rectangle2d Bounds { 
			get {
				if(Bounds==null){
					SetBounds(new Rectangle2d(GeometryHelper2d.PolygonBounds(Points)));
				}
				//need to apply matrix here
				return (Rectangle2d) R_bounds.Instance;
			}
		}
		protected Polygon2d SetBounds(Rectangle2d bounds){
			R_bounds=bounds;
			return this;
		}
		#endregion

		#region Rotation

		//get the rotation
		public override double Rotation { 
			get { 
				return Matrix.Rotation; 
			}
		}
		public Polygon2d SetRotation(double n) {
			//update the matrix
			Matrix.SetRotation(n);
			Matrix.ApplyTo(this);
			return this;
		}
		public Polygon2d Rotate (double n) {
			Matrix.SetRotation(n+Rotation);
			Matrix.ApplyTo(this);
			return this;
		}
		#endregion
		#region Scale
		public override Vector2d Scale { 
			get { return Matrix.Scale; } 
		}
		public Polygon2d SetScale(Vector2d v) {
			Matrix.SetScale(v);
			Matrix.ApplyTo(this);
			return this;
		}
		public Polygon2d SetScale(double x, double y) {
			SetScale(new Vector2d(x,y));
			return this;
		}
		public Polygon2d SetScale(double n) {
			SetScale(new Vector2d(n));
			return this;
		}
		#endregion
		#region Size, Width, Height
		protected Vector2d V_size=null;
		public Vector2d Size { 
			get{ 
				if(V_size==null) {
					V_size=Bounds.Points[0].Difference(Bounds.Points[2]);
				}
				return V_size;
			}
		}
		public double Width { 
			get { return Size.X; }
		}
		public double Height {
			get { 
				return Size.Y;
			}
		}
		#endregion
		
		#region Position
		//position
		public override Vector2d Position {
			get { return Matrix.Translation;}
		}		
		public Polygon2d SetPosition(Vector2d v) {
			//Update Matrix
			Matrix.SetTranslation(v);
			Matrix.ApplyTo(this);
			return this;
		}
		public Polygon2d SetPosition(double x, double y) {
			return SetPosition(new Vector2d(x,y));
		}
		public Polygon2d SetPosition(double n) {
			return SetPosition(new Vector2d(n));
		}

		//Move position
		public Polygon2d Move (Vector2d v) {
			//Update matrix
			Matrix.Translation.Add(v);
			Matrix.ApplyTo(this);
			return this;
		}
		public Polygon2d Move (double x, double y) {
			return Move(new Vector2d(x, y));

		}
		public Polygon2d Move (double n) {
			return Move(new Vector2d(n));
		}
		#endregion

		#region Shape2d X,Y
		//X,Y
		public override double X {
			get {
				return Matrix.Translation.X;
			}
		}
		public override double Y {
			get {
				return Matrix.Translation.Y;
			}
		}
		#endregion

		#region HitTest { Vector2d }
		public bool HitTestAlternative(Vector2d v) {
			//Coarse
			if( v.X<Bounds.Points[0].X||v.X>Bounds.Points[2].X||v.Y<Bounds.Points[0].Y||v.Y>Bounds.Points[2].Y ) {
				return false;
			}
			//fine
			double e = (Bounds.Points[2].X-Bounds.Points[0].X)/100;
			Vector2d start = new Vector2d(Bounds.Points[0].X-e,v.Y);
			Vector2d end = v.Instance;
			Segment2d ray = new Segment2d(start,end);
			int intersections=0;
			//count intersections
			foreach(Segment2d s in Segments){
				if(s.Intersects(ray)) intersections++;
			}
			if((intersections&1)==1) {
				return true;
			}
			return false;
		}
		public bool HitTest(Vector2d v){		
			//Coarse
			if( v.X<Bounds.Points[0].X||v.X>Bounds.Points[2].X||v.Y<Bounds.Points[0].Y||v.Y>Bounds.Points[2].Y ) {
				return false;
			}

			bool result=false;

			double[] vx = new double[Points.Length];
			double[] vy = new double[Points.Length];

			for(int n=0;n<Points.Length;n++) {
				vx[n]=Points[n].X;
				vy[n]=Points[n].Y;
			}
			int i, j;
			for( i=0, j=Points.Length-1; i<Points.Length; j=i++ ) {
				if( ((vy[i]>v.Y)!=(vy[j]>v.Y))&&(v.X<(vx[j]-vx[i])*(v.Y-vy[i])/(vy[j]-vy[i])+vx[i])) { result=!result; }
			}
			return result;
		}
		#endregion

	}

	//Shape description for a polygon
	public sealed class ShapeType_polygon : ShapeType {
		#region singleton
		private static volatile ShapeType_polygon instance=new ShapeType_polygon();
		private static object syncRoot=new Object();
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
