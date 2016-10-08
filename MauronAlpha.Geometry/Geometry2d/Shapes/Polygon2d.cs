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
		public Polygon2d() : base(ShapeType_polygon.Instance) {}

		public Polygon2d( Vector2dList points ) : this() {

			Initialize(points);
		
		}

		public Polygon2d( Polygon2d s )	: base( ShapeType_polygon.Instance ) {
			FromShape( s );
		}
		
		//Initialize a Shape
		public Polygon2d Initialize(Vector2dList points) {
			DATA_points = points;
			ResetTempStates();
			return this;
		}

		public Polygon2d ResetTempStates() {
			DATA_points_transformed = null;
			DATA_segments = null;
			SHAPE_bounds = null;
			return this;
		}

		//Segments - Built on request
		private Segment2dList DATA_segments;
		public virtual Segment2dList Segments {
			get {
				if(DATA_segments==null)
					DATA_segments = BuildSegments(TransformedPoints);
				return DATA_segments;
			}
		}
		public static Segment2dList BuildSegments( Vector2dList points ) {
			int count = points.Count;

			Segment2dList result = new Segment2dList();

			if( count < 1 )
				return result;
			if( count == 1 )
				return result.AddValue( new Segment2d( points.Value( 0 ), points.Value( 0 ) ) );

			for( int i=0; i<count; i++ ) {
				Vector2d v_current=points.Value( i );
				if( i%2==0&&points.ContainsKey( i+1 ) ) {
					Vector2d v_next = points.Value( i + 1 );
					Segment2d s=new Segment2d( v_current, v_next );
					result.AddValue( s );
				}
			}

			//final segment
			Vector2d v_last = points.Value( count - 1 );
			Vector2d v_first = points.Value( 0 );
			Segment2d segment=new Segment2d( v_last, v_first );
			return result.AddValue( segment );
		}

		//Points, aligned to 0,0 - no matrix applied
		private Vector2dList DATA_points_transformed;

		//snapshot of the points, as applied with a matrix and offset
		internal  Vector2dList DATA_points = new Vector2dList();
		public Vector2dList Points {
			get {
				return DATA_points.SetIsReadOnly(IsReadOnly);
			}
		}
		
		//Points with the matrix applied
		public Vector2dList TransformedPoints {
			get {
				if(DATA_points_transformed==null)
					DATA_points_transformed = Matrix.ApplyTo( Points );
				return DATA_points_transformed;
			}
		}

		//Return a superbasic representation of the polygonBounds
		Polygon2dBounds SHAPE_bounds;
		public override Polygon2dBounds Bounds {
			get {
				if( SHAPE_bounds == null )
					SHAPE_bounds = new Polygon2dBounds( TransformedPoints );

				//need to apply matrix here
				return SHAPE_bounds.SetIsReadOnly(true);
			}
		}
		public void SetBounds(Polygon2dBounds bounds) {
			SHAPE_bounds = bounds;
		}

		//Matrix
		private Matrix2d M_matrix;
		public override Matrix2d Matrix {
			get {
				if( M_matrix==null )
					M_matrix=new Matrix2d();
				return M_matrix.SetIsReadOnly( IsReadOnly );
			}
		}

		//Read Only
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		
		//Equals
		public bool Equals( Polygon2d other ) {
			if(Id.Equals(other.Id))
				return true;
			if (!M_matrix.Equals(other.Matrix))
				return false;
			if(!DATA_points.Equals(other.Points))
				return false;
			return true;

		}
		public bool Equals( I_polygonShape2d other ) {
			if(Id.Equals(other.Id))
				return true;
			if (!M_matrix.Equals(other.Matrix))
				return false;
			if(!DATA_points.Equals(other.Points))
				return false;
			return true;
		}

		public I_polygonShape2d Cloned {
			get {
				return new Polygon2d().FromShape(this);
			}
		}

		//return a copy of this polygon with matrix applied
		public Polygon2d SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}

		public Polygon2d SetPoints (Vector2dList points) {
			if( IsReadOnly )
				throw Error("Is Protected!, (SetPoints)", this, ErrorType_protected.Instance);

			Initialize(points);
			//Matrix2d matrix = DATA_points.Bulk_OffsetToVector_matrix( new Vector2d(0) );
			//Matrix.Add( matrix );

			return this;
		}
		
		public Polygon2d Instance {
			get {
				return new Polygon2d( this );
			}
		}
		public Polygon2d FromShape( Polygon2d shape ) {
			if( IsReadOnly )
				throw Error( "Is Protected!,(FromShape)", this, ErrorType_protected.Instance );

			DATA_points = shape.Points;
			M_matrix = shape.Matrix;

			if (shape.CalculatedTransform)
				DATA_points_transformed = shape.TransformedPoints;
			if (shape.CalculatedBounds)
				SHAPE_bounds = shape.Bounds;
			if (shape.CalculatedSegments)
				DATA_segments = shape.Segments;

			M_matrix = shape.Matrix;

			return this;
		}

		//Check what states have been calculated
		public bool CalculatedTransform {
			get {
				return DATA_points_transformed != null;
			}
		}
		public bool CalculatedBounds {
			get {
				return SHAPE_bounds != null;
			}
		}
		public bool CalculatedSegments {
			get {
				return DATA_segments != null;
			}
		}


		//Apply Transformations
		public Polygon2d SetMatrix (Matrix2d m) {
			if( IsReadOnly )
				throw Error("Is Protected!,(SetMatrix)", this, ErrorType_protected.Instance);

			M_matrix=m;
			ResetTempStates();

			return this;
		}

		public Polygon2d SetRotation( double n ) {
			if( IsReadOnly )
				throw Error( "Is Protected!,(SetRotation)", this, ErrorType_protected.Instance );

			ResetTempStates();

			//update the matrix
			Matrix.SetRotation( n );
			return this;
		}
		public Polygon2d Rotate( double n ) {
			return SetRotation( n+Rotation );
		}

		public Polygon2d SetScale(Vector2d v) {
			if( IsReadOnly )
				throw Error("Is Protected!,(SetScale)", this, ErrorType_protected.Instance);
			ResetTempStates();

			Matrix.SetScale(v);
			return this;
		}
		public Polygon2d SetScale(double x, double y) {
			return SetScale(new Vector2d(x,y));
		}
		public Polygon2d SetScale(double n) {
			return SetScale(new Vector2d(n));
		}
		
		public Polygon2d SetPosition(Vector2d v) {
			if( IsReadOnly )
				throw Error("Is Protected!,(SetPosition)", this, ErrorType_protected.Instance);
			//Update Matrix
			ResetTempStates();
			Matrix.SetTranslation(v);
			return this;
		}
		public Polygon2d SetPosition(double x, double y) {
			return SetPosition(new Vector2d(x,y));
		}
		public Polygon2d SetPosition(double n) {
			return SetPosition(new Vector2d(n));
		}

		public Polygon2d Move( Vector2d v ) {
			if( IsReadOnly )
				throw Error( "Is Protected!,(Move)", this, ErrorType_protected.Instance );
			ResetTempStates();
			//Update matrix
			Matrix.Translation.Add( v );
			return this;
		}
		public Polygon2d Move( double x, double y ) {
			return Move( new Vector2d( x, y ) );
		}
		public Polygon2d Move( double n ) {
			return Move( new Vector2d( n ) );
		}

		//X,Y
		public double X {
			get {
				return Matrix.Translation.X;
			}
		}
		public double Y {
			get {
				return Matrix.Translation.Y;
			}
		}
		public double Rotation { 
			get { 
				return Matrix.Rotation; 
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

		public string AsString {
			get {
				string result = "{";
				for( int i=0; i < Points.Count; i++ ) {
					Vector2d p = Points.Value( i );
					result += "[" + ( i+1 ) + ":" + p.AsString + "]";
				}
				return result + "}";
			}
		}

		public Vector2d Scale { 
			get { return Matrix.Scale; } 
		}
		public Vector2d Size { 
			get{ 
				return Bounds.Size;
			}
		}
		public override Vector2d Position {
			get { return Matrix.Translation; }
		}
		public Vector2d Center {
			get {
				return Bounds.Center;
			}
		}
		public Vector2d Point(int index) {
			return DATA_points.Value(index);
		}

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
		}

		public int CountPoints {
			get { return DATA_points.Count; }
		}
		public int CountSegments {
			get {
				return Segments.Count;
			}
		}
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