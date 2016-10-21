﻿using System.Collections.Generic;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Transformation;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

    //A collection of vectors
	public class Vector2dList:MauronCode_dataList<Vector2d> {

        //Constructor
		public Vector2dList( bool isOrdered ): base() {
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
		public Vector2dList( ICollection<Vector2d> points, bool b_isOrdered ): this() {
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
			if( reorderIndex )
				B_isOrdered = false;
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

		//Modify Content
		public Vector2dList Offset(Vector2d v) {
			foreach (Vector2d vector in this)
				vector.Add(v);
			return this;
		}

		public Vector2dList AddAll( Vector2d v ) {
			foreach( Vector2d p in this )
				p.Add( v );
			return this;
		}
		public Vector2dList SubtractAll( Vector2d v ) {
			if( IsReadOnly ) {
				throw Error( "Protected!,(Subtract)", this, ErrorType_protected.Instance );
			}

			foreach( Vector2d p in this ) {
				p.Subtract( v );
			}
			return this;
		}
		public Vector2dList MultiplyAll( Vector2d v ) {
			if( IsReadOnly ) {
				throw Error( "Protected!,(Multiply)", this, ErrorType_protected.Instance );
			}

			foreach( Vector2d p in this ) {
				p.Multiply( v );
			}
			return this;
		}
		public Vector2dList DivideAll( Vector2d v ) {
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
		public Vector2dList Copy {
			get { return Instance; }
		}

		//has the list been ordered?
		private bool B_isOrdered = false;
		public bool IsOrdered {
			get {
				return B_isOrdered;
			}
		}

		public Vector2dList MinMax {
			get {
				Vector2d min = Vector2d.Zero;
				Vector2d max = Vector2d.Zero;
				foreach (Vector2d v in this) {
					if(v.X < min.X)
						min.SetX(v.X);
					if(v.X > max.X)
						max.SetX(v.X);
					if(v.Y < min.Y)
						min.SetY(v.Y);
					if(v.Y > max.Y)
						max.SetY(v.Y);
				}
				return new Vector2dList() { min, max };
			}
		}


		/// <summary>Same as TryFind[index%count]</summary>
		public bool ByModulo(int index, ref Vector2d result) {
			int count = Count;
			if (count < 1)
				return false;

			int offset = index % count;

			return TryIndex(offset, ref result);
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