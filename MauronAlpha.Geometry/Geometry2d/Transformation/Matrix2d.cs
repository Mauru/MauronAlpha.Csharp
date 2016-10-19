using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Collections;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Geometry.Geometry2d.Transformation {

	//Keeps track of all applied transforms to a series of geometrical data
	public class Matrix2d : GeometryComponent2d {

		//Constructor
		public Matrix2d():base() {}

		public static Matrix2d Identity {
			get {
				return new Matrix2d();
			}
		}

		//CP Instance
		public Matrix2d Instance {
			get {
				Matrix2d instance = new Matrix2d();
				instance.SetOffset( Offset );
				instance.SetRotation( Rotation );
				instance.SetScale( Scale );
				instance.SetShear( Shear );
				instance.SetTranslation( Translation );
				return instance;
			}
		}

		//BOOL Equals
		public bool Equals( Matrix2d other ) {
			

			if( !V_offset.Equals( other.Offset ) )
				return false;

			if( INT_rotation != other.Rotation )
				return false;

			if( !V_scale.Equals( other.Scale ) )
				return false;

			if( !V_shear.Equals( other.Shear ) )
				return false;

			if( !V_translation.Equals( other.V_translation ) )
				return false;

			return true;

		}
		
		//RET Reset
		public Matrix2d Reset() {
			V_offset = new Vector2d();
			V_scale = new Vector2d();
			V_shear = new Vector2d();
			V_translation = new Vector2d();
			INT_rotation = 0;

			return this;
		}


//Properties

		#region BOOL Readonly  GET, SET<RET>
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public Matrix2d SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}
		#endregion

		#region double Rotation GET, SET<RET,E>
		protected double INT_rotation=0;
		public double Rotation {
			get {	
				return INT_rotation;
			}
		}
		public Matrix2d SetRotation(double rotation){
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetRotation)", this, ErrorType_protected.Instance);
			}
			INT_rotation=rotation;
			return this;
		}
		#endregion
		
		#region VECTOR Scale GET, SET<RET,E>
		protected Vector2d V_scale=new Vector2d(1);
		public Vector2d Scale { get { return V_scale; } }
		public Matrix2d SetScale (Vector2d v) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetScale)", this, ErrorType_protected.Instance);
			}
			V_scale=v;
			return this;
		}
		#endregion
		
		#region VECTOR Shear GET, SET<RET,E>
		protected Vector2d V_shear=new Vector2d(0);
		public Vector2d Shear { 
			get { return V_shear; }
		}
		public Matrix2d SetShear(Vector2d v){
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetShear)", this, ErrorType_protected.Instance);
			}
			V_shear=v;
			return this;
		}
		#endregion

		#region VECTOR Translation GET, SET<RET, E>
		protected Vector2d V_translation=new Vector2d(0);
		public Vector2d Translation { get { return V_translation; } } 
		public Matrix2d SetTranslation(Vector2d v) {
			if( IsReadOnly ) {
				throw Error("Is Protected!,(SetTranslation)", this, ErrorType_protected.Instance);
			}
			V_translation=v;
			return this;
		}
		#endregion
       
        #region VECTOR Offset GET, SET<RET, E> - might call this the origin
        private Vector2d V_offset;
        public Vector2d Offset {
            get {
                if (V_offset == null) {
                    V_offset = new Vector2d();
                }
                return V_offset;
            }
        }
        public Matrix2d SetOffset(Vector2d v) {
            if (IsReadOnly) {
                throw Error("Protected!,(SetOffset)", this, ErrorType_protected.Instance);
            }
            V_offset = v;
            return this;
        }
        #endregion

//Modifiers


		//reset any modifications
		public Polygon2d RemoveFrom (Polygon2d p) {
			Vector2d center = p.Center;

			Vector2d scale_modifier = new Vector2d( 1 ).Divide( Scale );

			foreach( Vector2d v in p.Points ) {

				//Reset Rotation
				v.Rotate( center, Rotation*-1 );

				//Scale
				v.Transform( center, scale_modifier );

				//Offset
				v.Subtract( Translation );
			}

			return p;
		}
		public Vector2dList RemoveFrom( Vector2dList pp ) {

			Polygon2d p = new Polygon2d( pp );

			Vector2d center = p.Center;

			Vector2d scale_modifier = new Vector2d(1).Divide( Scale );

			foreach( Vector2d v in pp ) {

				//Reset Rotation
				v.Rotate(center, Rotation*-1);

				//Scale
				v.Transform( center, scale_modifier );

				//Offset
				v.Subtract(Translation);

			}

			return pp;
		}

		//apply modifications
		public Polygon2d ApplyTo (Polygon2d p) {

			Vector2d center= p.Center;

			foreach( Vector2d v in p.Points ) {

				//Reset Rotation
				v.Rotate(center, Rotation);

				//Scale
				v.Transform(center, Scale);

				//Offset
				v.Add(Translation);

			}

			return p;
		}
		public Vector2dList ApplyTo( Vector2dList pp ) {

			Polygon2d p = new Polygon2d( pp );

			Vector2d center = p.Center;

			foreach( Vector2d v in pp ) {

				//Reset Rotation
				v.Rotate(center, Rotation);

				//Scale
				v.Transform(center, Scale);

				//Offset
				v.Add(Translation);

			}

			return pp;
		}

		//Modify a matrix
		public Matrix2d Add( Matrix2d matrix ) {
			if( IsReadOnly )
				throw Error( "Protected!,(Add)", this, ErrorType_protected.Instance );
			SetRotation( Rotation+matrix.Rotation );
			SetScale( Scale.Add( matrix.Scale ) );
			SetTranslation( Translation.Add( matrix.Translation ) );
			SetShear( Shear.Add( matrix.Shear ) );
			return this;
		}
		public Matrix2d Subtract( Matrix2d matrix ) {
			if( IsReadOnly )
				throw Error( "Protected!,(Subtract)", this, ErrorType_protected.Instance );
			SetRotation( Rotation-matrix.Rotation );
			SetScale( Scale.Subtract( matrix.Scale ) );
			SetTranslation( Translation.Subtract( matrix.Translation ) );
			SetShear( Shear.Subtract( matrix.Shear ) );
			return this;
		}
		public Matrix2d Multiply( Matrix2d matrix ) {
			if( IsReadOnly )
				throw Error( "Protected!,(Multiply)", this, ErrorType_protected.Instance );
			SetRotation( Rotation*matrix.Rotation );
			SetScale( Scale.Multiply( matrix.Scale ) );
			SetTranslation( Translation.Multiply( matrix.Translation ) );
			SetShear( Shear.Multiply( matrix.Shear ) );
			return this;
		}
		public Matrix2d Divide( Matrix2d matrix ) {
			if( IsReadOnly )
				throw Error( "Protected!,(Divide)", this, ErrorType_protected.Instance );
			if(matrix.Rotation!=0)
				SetRotation( Rotation/matrix.Rotation );
			if(!matrix.Scale.IsZero)
				SetScale( Scale.Divide( matrix.Scale ) );
			if( !matrix.Translation.IsZero )
				SetTranslation( Translation.Divide( matrix.Translation ) );
			if( !matrix.Shear.IsZero )
				SetShear( Shear.Divide( matrix.Shear ) );
			return this;
		}
		public Matrix2d Difference( Matrix2d matrix ) {
			Matrix2d result=new Matrix2d();
			result.SetRotation( Rotation-matrix.Rotation );
			result.SetScale( Scale.Difference( matrix.Scale ) );
			result.SetTranslation( Translation.Difference( matrix.Translation ) );
			result.SetShear( Shear.Difference( matrix.Shear ) );
			return result;
		}
	}

}
