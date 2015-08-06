using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Interfaces;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Shapes;

using MauronAlpha.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Transformation;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

public class Polygon2dBounds : GeometryComponent2d, I_polygonShape2d, I_protectable<Polygon2dBounds> {

	//Constructors
	private Polygon2dBounds():base() {}
	//Relative constructors
	public Polygon2dBounds( I_polygonShape2d parentShape ) : this(parentShape.Points) {
	}

	//Absolute constructors
	private Polygon2dBounds( Vector2d center, Vector2d min, Vector2d max )
		: this() {

		V_min = min;
		V_max = max;
		V_center = min.Difference( V_max );

	}
	public Polygon2dBounds( Vector2dList points )
		: this() {

		Vector2d min = null;
		Vector2d max = null;

		foreach( Vector2d v in points ) {
			if( min == null ) {
				min = new Vector2d( v );
				max = new Vector2d( v );
			} else {
				if( v.X < min.X )
					min.SetX( v.X );
				if( v.Y < min.Y )
					min.SetY( v.Y );
				if( v.X > max.X )
					max.SetX( v.X );
				if( v.Y > max.Y )
					max.SetY( v.Y );
			}
		}
		if( min == null ) {
			throw NullError( "Invalid or empty polygon!,(Constructor)", this, typeof( Vector2d ) );
		}

		V_min = min;
		V_max = max;
		V_center = min.Difference( V_max );

	}

	//properties
	Vector2d V_center;
	Vector2d V_min;
	Vector2d V_max;

	private bool B_isReadOnly = false;
	public bool IsReadOnly { get { return B_isReadOnly; } }
	public Polygon2dBounds SetIsReadOnly(bool state) {
		B_isReadOnly = state;
		return this;
	}
	public bool Equals(I_polygonShape2d other) {
		if (Id.Equals(other.Id))
			return true;
		return Points.Equals(other.Points);
	}

	public Vector2d Min {
		get {
			if( V_min == null )
				throw NullError( "Invalid or empty polygon!,(Min)", this, typeof( Vector2d ) );
			return V_min.SetIsReadOnly( true );
		}
	}
	public Vector2d Max {
		get {
			if( V_max == null )
				throw NullError( "Invalid or empty polygon!,(Max)", this, typeof( Vector2d ) );
			return V_max.SetIsReadOnly( true );
		}
	}
	public Vector2d Center {
		get {
			if( V_center == null )
				throw NullError( "Invalid or empty polygon!,(Max)", this, typeof( Vector2d ) );

			return V_center.SetIsReadOnly( true );
		}
	}
	public Vector2d Size {
		get {
			return Min.Difference( Max ).SetIsReadOnly(true);
		}
	}

	//Instancing
	public Polygon2dBounds Instance {
		get {
			return new Polygon2dBounds(V_min, V_max, V_center);
		}
	}

	public Transformation.Matrix2d Matrix {
		get {
			return new Matrix2d().SetIsReadOnly(true);
		}
	}

	public Vector2dList TransformedPoints {
		get {
			return Points;
		}
	}

	public Vector2dList Points {
		get {
			return new Vector2dList(new Vector2d[4]{
				V_min,
				V_max.Instance.SetY(V_min.Y),
				V_max,
				V_min.Instance.SetY(V_max.Y)
			},true);
		}
	}

	public ShapeType ShapeType {
		get {
			return ShapeType_polygon.Instance;
		}
	}

	public Polygon2dBounds Bounds {
		get {
			return this;
		}
	}



	public I_polygonShape2d Cloned {
		get {
			return Instance;
		}
	}
}
}

