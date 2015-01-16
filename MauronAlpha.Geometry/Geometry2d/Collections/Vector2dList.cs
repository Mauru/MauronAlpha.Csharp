using System.Collections.Generic;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Transformation;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

    //A collection of vectors
	public class Vector2dList:MauronCode_dataList<Vector2d> {

        //Constructor
		public Vector2dList( bool isOrdered )
			: base() {
				B_isOrdered = isOrdered;
		}

        public Vector2dList() : this(false) { }

		//Relative Constructor
        public Vector2dList(ICollection<Vector2d> points):this() {
            foreach (Vector2d v in points) {
                AddValue(v.Instance, false);
            }
			B_isOrdered = false;
        }
		public Vector2dList( ICollection<Vector2d> points, bool b_isOrdered )
			: this() {
			foreach( Vector2d v in points ) {
				AddValue( v.Instance, false );
			}
			B_isOrdered = b_isOrdered;
		}

		public new string AsString {
			get {
				string result= "[";
				foreach(Vector2d vector in Data ) {
					result+=vector.AsString;
				}
				result+=']';
				return result;
			}
		}

		public new ICollection<KeyValuePair<int, Vector2d>> KeyValuePairs {
			get {
				return base.KeyValuePairs;
			}
		}

		public Vector2dList AddValue( Vector2d v, bool reorderIndex ) {
			base.AddValue( v );
			if( reorderIndex ) {
				B_isOrdered = false;
			}
			return this;
		}
		public Vector2dList AddValues( ICollection<Vector2d> values, bool reorderIndex ) {
			foreach( Vector2d vector in values ) {
				AddValue( vector, false );
			}
			if( reorderIndex )
				B_isOrdered = false;

			return this;
		}

		//Order a list by its Vectors from topleft to bottomright we go X,XY> : //WARNING! We are loosing the long/int precision here!
		public Vector2dList Ordered_asLRTB {
			get {
				if( IsReadOnly )
					throw Error( "Protected!,(Ordered_asLRTB)", this, ErrorType_protected.Instance );
				base.Sort( 0, 3, Vector2d.Compare );
				return this;
			}
		}

		//Modify Content
		public Vector2dList Bulk_Add( Vector2d v ) {
			if( IsReadOnly ) {
				throw Error( "Protected!,(Add)", this, ErrorType_protected.Instance );
			}
			foreach( Vector2d p in this ) {
				p.Add( v );
			}
			return this;
		}
		public Vector2dList Bulk_Subtract( Vector2d v ) {
			if( IsReadOnly ) {
				throw Error( "Protected!,(Subtract)", this, ErrorType_protected.Instance );
			}

			foreach( Vector2d p in this ) {
				p.Subtract( v );
			}
			return this;
		}
		public Vector2dList Bulk_Multiply( Vector2d v ) {
			if( IsReadOnly ) {
				throw Error( "Protected!,(Multiply)", this, ErrorType_protected.Instance );
			}

			foreach( Vector2d p in this ) {
				p.Multiply( v );
			}
			return this;
		}
		public Vector2dList Bulk_Divide( Vector2d v ) {
			if( IsReadOnly ) {
				throw Error( "Protected!,(Divide)", this, ErrorType_protected.Instance );
			}

			foreach( Vector2d p in this ) {
				p.Divide( v );
			}
			return this;
		}

		//ReadOnly
		public new Vector2dList SetIsReadOnly( bool state ) {
			base.SetIsReadOnly( state );
			return this;
		}
		
        //Clone the vectors in the list
		public Vector2dList Cloned {
			get {
				Vector2dList r = new Vector2dList( B_isOrdered );
				foreach(Vector2d v in this){
					r.AddValue( v.Instance, B_isOrdered == false );
				}
				return r;			
			}
		}
        public new Vector2dList Instance {
            get {
                Vector2dList result = new Vector2dList();
                foreach (Vector2d v in this) {
                    result.AddValue(v);
                }
                return result;
            }
        }

		//has the list been ordered?
		private bool B_isOrdered = false;
		public bool IsOrdered {
			get {
				return B_isOrdered;
			}
		}

		//Modify Points so first point is at 0,0 return the offset as Matrix object
		public Matrix2d Bulk_OffsetToVector_matrix( Vector2d v ) {
			if(IsReadOnly)
				throw Error("Protected!,(Bulk_OffsetToVector_matrix",this,ErrorType_protected.Instance);
			if(IsEmpty) 
				throw NullError("VectorList is empty!,(Bulk_OffsetToVector_matrix)",this,typeof(Vector2d));
			
			Vector2d offset = v.Difference( base.Value( 0 ) );

			if( offset.IsZero )
				return new Matrix2d();

			ICollection<KeyValuePair<int,Vector2d>> data_asPair = KeyValuePairs;

			foreach( KeyValuePair<int,Vector2d> member in data_asPair ) {
				SetValue(member.Key, Value(member.Key).Subtract(offset));
			}

			return new Matrix2d().SetOffset( offset );
		}

	}

}