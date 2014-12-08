using System.Collections.Generic;
using System;

using MauronAlpha.HandlingErrors;

using MauronAlpha.Geometry.Geometry2d.Transformation;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Shapes;
using MauronAlpha.Geometry.Geometry2d.Collections;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A concave or convex Polygon in 2d Space
	public class Polygon2d:GeometryComponent2d_shape {

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool Equals (Polygon2d p) {
			return AsString==p.AsString;
		}

		public Polygon2d SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}
		protected Polygon2d SetMatrix (Matrix2d m) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetMatrix)", this, ErrorType_protected.Instance);
			}

			//Reset Points to original
			Matrix.RemoveFrom(this);

			M_matrix=m;

			//Apply new Matrix
			Matrix.ApplyTo(this);

			return this;
		}
		public Polygon2d SetPoints (ICollection<Vector2d> points) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetPoints)", this, ErrorType_protected.Instance);
			}

            DATA_points = new Vector2dList(points);


			//we offset the first point to be 0,0
			Matrix2d t_matrix = new Matrix2d();
			if( DATA_points.Count>0 ) {
				Vector2d offset = new Vector2d().Difference(DATA_points.Value(0));
				
			}

			DATA_points = Matrix.ApplyTo(DATA_points);
			return this;
		}

		public string AsString {
			get {
				string r="{";
				for( int i=0; i<Points.Length; i++ ) {
					Vector2d p=Points.Value(i);
					r+="["+(i+1)+":"+p.AsString+"]";
				}
				return r+"}";
			}
		}

		protected Matrix2d M_matrix;
		public override Matrix2d Matrix {
			get {
				if( M_matrix==null ) {
					M_matrix=new Matrix2d(this);
				}
				return M_matrix.Instance.SetIsReadOnly(IsReadOnly);
			}
		}

		protected Vector2dList DATA_points;
        public Vector2dList Points {
			get { return DATA_points.Instance.SetIsReadOnly(true); }
		}

        private Polygon2dBounds V_limits;
        private Polygon2dBounds Limits {
            get {
                if (V_limits == null){
                    V_limits = new Polygon2dBounds(this);
                }
                return V_limits;
            }
        }
		
        protected Segment2dList DATA_segments;
        public Segment2dList Segments {
			get {
                if (DATA_segments == null) {
                    DATA_segments = BuildSegments(DATA_points);
				}
                return DATA_segments;
			}
		}
		private Polygon2d SetSegments(Segment2dList segments) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetSegments)", this, ErrorType_protected.Instance);
			}
            DATA_segments = segments;
			return this;
		}
        public static Segment2dList BuildSegments (Vector2dList points) {
            int count = points.Count;

            Segment2dList result = new Segment2dList();

            if (count < 1)
                return result;
            if (count == 1)
                return result.Add(new Segment2d(points.Value(0), points.Value(0));

			for( int i=0; i<count; i++ ) {
				Vector2d v_current=points.Value(i);
				if( i%2==0&&points.ContainsKey(i+1) ) {
                    Vector2d v_next = points.Value(i + 1);
					Segment2d s=new Segment2d(v_current,v_next);
					result.Add(s);
				}
			}

			//final segment
            Vector2d v_last = points.Value(count - 1);
            Vector2d v_first = points.Value(0);
			Segment2d segment=new Segment2d(v_last, v_first);
            return result.Add(segment);
		}

		#region Get a copy of the polygon
		public Polygon2d Instance {
			get {
				return new Polygon2d(this);
			}
		}
		public Polygon2d():base(ShapeType_polygon.Instance) {}
		public Polygon2d (Polygon2d s) : base(ShapeType_polygon.Instance) { }
		public Polygon2d FromShape(Polygon2d s) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(FromShape)", this, ErrorType_protected.Instance);
			}
			foreach(Vector2d p in s.Points) {
				DATA_points.Add(p.Instance);
			}
			SetSegments(s.Segments);
			SetCenter(s.Center);
			R_bounds=s.Bounds;
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
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetCenter)", this, ErrorType_protected.Instance);
			}
			if( v==null ) {
				V_center=null;
				return;
			}
			Vector2d offset=Center.Difference(v);
			Move(v);
			V_center=v;		
		}
		#endregion

		protected Rectangle2d R_bounds;
		public override Rectangle2d Bounds { 
			get {
                if (R_bounds == null)
                {
                    Vector2d[] minmax = GeometryHelper2d.PolygonBounds(Points);
                    R_bounds = new Rectangle2d(minmax[0], minmax[1]);
				}
				//need to apply matrix here
				return (Rectangle2d) R_bounds.Instance.SetIsReadOnly(true);
			}
		}

		#region Rotation

		//get the rotation
		public override double Rotation { 
			get { 
				return Matrix.Rotation; 
			}
		}
		public Polygon2d SetRotation(double n) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetRotation)", this, ErrorType_protected.Instance);
			}
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
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetScale)", this, ErrorType_protected.Instance);
			}
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
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetPosition)", this, ErrorType_protected.Instance);
			}
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
			if( IsReadOnly ) {
				throw Error("Is Protected!,(Move)", this, ErrorType_protected.Instance);
			}
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
	public sealed class ShapeType_polygon:ShapeType {
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
