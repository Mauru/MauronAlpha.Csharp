using System.Collections.Generic;
using MauronAlpha;
using System;

namespace MauronAlpha.Geometry._2d {

	//Keeps track of all applied transforms
	public class Matrix2d:Geometry2d {
		public double Rotation=0; //1st
		public Vector2d Scale=new Vector2d(1); //2nd
		public Vector2d Shear=new Vector2d(); //3
		public Vector2d Offset=new Vector2d(); //4

		//reset any modifications
		public Polygon2d RemoveFrom (Polygon2d pp) {
			Vector2d center=Geometry2d.PolygonCenter(pp);
			Vector2d s=new Vector2d(1).Divide(Scale);

			foreach( Vector2d v in pp.Points ) {
				//Reset Rotation
				v.Rotate(center, Rotation*-1);

				//Scale
				v.Transform(center, s);

				//Offset
				v.Subtract(Offset);
			}
			return pp;	
		}
		public Vector2d[] RemoveFrom(Vector2d[] pp){
			Vector2d center=Geometry2d.PolygonCenter(pp);
			Vector2d s=new Vector2d(1).Divide(Scale);

			foreach( Vector2d v in pp ) {
				//Reset Rotation
				v.Rotate(center, Rotation*-1);

				//Scale
				v.Transform(center, s);

				//Offset
				v.Subtract(Offset);
			}
			return pp;
		}

		//apply matrix
		public Polygon2d ApplyTo (Polygon2d pp) {
			Vector2d center=Geometry2d.PolygonCenter(pp);
			foreach( Vector2d v in pp.Points ) {
				//Reset Rotation
				v.Rotate(center, Rotation);

				//Scale
				v.Transform(center, Scale);

				//Offset
				v.Add(Offset);
			}
			return pp;
		}
		public Vector2d[] ApplyTo (Vector2d[] pp) {
			Vector2d center = Geometry2d.PolygonCenter(pp).Divide(2);

			foreach( Vector2d v in pp ) {
				//Reset Rotation
				v.Rotate(center, Rotation);

				//Scale
				v.Transform(center, Scale);

				//Offset
				v.Add(Offset);
			}
			return pp;
		}
	}

	public class Polygon2d:Shape2d {
		
		//Comparison, string
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

		//Matrix
		protected Matrix2d M_matrix=new Matrix2d();
		public Matrix2d Matrix { get { return M_matrix; } }
		protected Polygon2d SetMatrix(Matrix2d m) {
			//Reset Points to original
			Matrix.RemoveFrom(this);

			M_matrix=m;

			//Apply new Matrix
			Matrix.ApplyTo(this);

			return this;
		}

		//Points
		protected List<Vector2d> A_points=new List<Vector2d>();
		public Vector2d[] Points {
			get { return A_points.ToArray(); }
		}
		public void SetPoints (Vector2d[] points) {
			A_points=new List<Vector2d>(points);
		}

		//Segments, needs to be manually updated
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
		protected Polygon2d SetSegments(Segment2d[] s) {
			A_segments=new List<Segment2d>(s);
			return this;
		}

		//Get a copy of the polygon
		public Polygon2d Instance {
			get {
				return new Polygon2d(this);
			}
		}
		public Polygon2d() {} 
		public Polygon2d(Polygon2d s){}
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

		//Get the center
		protected Vector2d V_center=null;
		public Vector2d Center {
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
	
		//Bounds
		protected Rectangle2d R_bounds=null;
		public Rectangle2d Bounds { 
			get {
				if(Bounds==null){
					SetBounds(new Rectangle2d(Geometry2d.PolygonBounds(Points)));
				}
				//need to apply matrix here
				return (Rectangle2d) R_bounds.Instance;
			}
		}
		protected Polygon2d SetBounds(Rectangle2d bounds){
			R_bounds=bounds;
			return this;
		}

		//Rotation
		public double Rotation { get { return Matrix.Rotation; } }
		public Polygon2d SetRotation(double n) {
			Matrix.Rotation=n;
			return this;
		}
		public Polygon2d Rotate(double n) {
			Matrix.Rotation+=n;
			return this;
		}

		//Scale
		public Vector2d Scale { 
			get { return Matrix.Scale; } 
		}
		public Polygon2d SetScale(Vector2d v) {
			Matrix.Scale=v;
			foreach(Vector2d p in Points){
				p.Transform(Center,v);
			}

			//reset bounds and center
			SetCenter(null);
			SetBounds(null);

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

		//Size
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
	
		//Position
		public Vector2d Position {
			get { return Matrix.Offset;}
		}
		public Polygon2d SetPosition(Vector2d v) {
			Matrix.Offset=v;

			foreach(Vector2d p in Points){
				v.Add(v);
			}

			//reset bounds and center
			SetCenter(null);
			SetBounds(null);

			return this;
		}
		public Polygon2d SetPosition(double x, double y) {
			return SetPosition(new Vector2d(x,y));
		}
		public Polygon2d SetPosition(double n) {
			return SetPosition(n);
		}
		public Polygon2d Move (Vector2d v) {
			Matrix.Offset.Add(v);
			return this;
		}
		public Polygon2d Move(double x, double y) {
			return Move(new Vector2d(x,y));

		}
		public Polygon2d Move (double n) {
			return Move(new Vector2d(n));
		}
		public double X { 
			get {
				return Bounds.Points[0].X;
			}
		}
		public double Y { 
			get {
				return Bounds.Points[0].Y;
			}
		}
	
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



	}

		

	/*	private Vector2d v_center=null;
		public Vector2d Center {
			get {
				if( v_center!=null ) {
					return v_center;
				}
				Vector2d[] r=Geometry2d.PolygonBounds(Points);
				v_center=new Vector2d(r[0].X+r[1].X/2, r[0].Y+r[1].Y/2);
				return v_center;
			}
			set {
				this.v_center=value;
			}
		}
		public Vector2d[] Points;
		public Segment2d[] Segments;
		private Rectangle2d r_bounds = new Rectangle2d();
		public Rectangle2d Bounds {
			get {
				return Geometry2d.PolygonBounds(Points);
			}
		}

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

		public void SetOffset (double x, double y) {
			SetOffset(new Vector2d(x,y));
		}
		public void SetOffset(Vector2d v) {
			Vector2d r=new Vector2d();
			if(Points.Length>0){
				r = v.Difference(Points[0]);
				Points[0].SetOffset(v);
				for(int i=1;i<Points.Length;i++){
					Vector2d e = Points[i];
					e.Add(r);
				}
			}
			Center=null;

		}
*/

}
