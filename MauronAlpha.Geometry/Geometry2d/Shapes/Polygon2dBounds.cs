using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Interfaces;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Shapes;

using MauronAlpha.Geometry.Geometry2d.Transformation;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

public class Polygon2dBounds : GeometryComponent2d, I_polygonShape2d {

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

	public Vector2d Min {
		get {
			if( V_min == null ) {
				throw NullError( "Invalid or empty polygon!,(Min)", this, typeof( Vector2d ) );
			}
			return V_min.Instance.SetIsReadOnly( true );
		}
	}
	public Vector2d Max {
		get {
			if( V_max == null ) {
				throw NullError( "Invalid or empty polygon!,(Max)", this, typeof( Vector2d ) );
			}
			return V_max.Instance.SetIsReadOnly( true );
		}
	}
	public Vector2d Center {
		get {
			if( V_center == null ) {
				throw NullError( "Invalid or empty polygon!,(Max)", this, typeof( Vector2d ) );
			}
			return V_center.Instance.SetIsReadOnly( true );
		}
	}
	public Vector2d Size {
		get {
			return Min.Difference( Max );
		}
	}

	//Instancing
	public Polygon2dBounds Instance {
		get {
			return new Polygon2dBounds(V_min, V_max, V_center);
		}
	}

	#region I_polygonShape2d Members

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

	public bool Equals( I_polygonShape2d other ) {
		return TransformedPoints.Equals(other.TransformedPoints);
	}

	public bool IsReadOnly {
		get {
			return false;
		}
	}

	public I_polygonShape2d Cloned {
		get {
			return Instance;
		}
	}

	#endregion
}
}

/*
public Polygon2dBounds Instance {
	get {
		Polygon2dBounds instance = new Polygon2dBounds( V_center, V_min, V_max );
		return instance;
	}
}

	//Default properties
	private Vector2d V_min;
	private Vector2d V_max;
	private Vector2d V_center;
	private Vector2d V_size;









public Vector2d Size {
	get {
		if( V_size == null ) {
			V_size = TopLeft.Difference( BottomRight ).Normalized;
		}
		return V_size;
	}
}
public Vector2d TopLeft {
	get {
		return V_min.SetIsReadOnly( true );
	}
}
public Vector2d BottomRight {
	get {
		return V_max.SetIsReadOnly( true );
	}
}
public Vector2d Center {
	get {
		if( V_center == null ) {
			V_center = TopLeft.Instance.Add( TopLeft.Difference( BottomRight ).Divide( 2 ) );
		}
		return V_center.SetIsReadOnly( true );
	}
}

public Rectangle2d AsRectangle {
	get {
		return new Rectangle2d( V_min, V_max.Difference( V_min ) );
	}
}
public string AsString {
	get {
		return AsRectangle.AsString;
	}
}

 * */
