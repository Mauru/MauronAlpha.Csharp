using System.Collections.Generic;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Transformation;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

    //A collection of vectors
	public class Vector2dList:MauronCode_dataList<Vector2d> {
		//constructor
        public Vector2dList() : base() { }

		//Relative Constructor
        public Vector2dList(ICollection<Vector2d> points):this() {
            foreach (Vector2d v in points)
                AddValue(v);
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

		public Vector2dList AddValue( Vector2d v ) {
			base.AddValue( v );
			return this;
		}
		public Vector2dList AddValues( ICollection<Vector2d> values) {
			foreach( Vector2d vector in values )
				Add(vector);
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
		
        public new Vector2dList Instance {
            get {
                Vector2dList result = new Vector2dList();
                foreach (Vector2d v in this)
                    result.Add(v);
                return result;
            }
        }
		public Vector2dList Copy {
			get { 
				Vector2dList result = new Vector2dList();
				foreach (Vector2d v in this)
					result.Add(v);
				return result;
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


		//Return as copy with Offset v
		public Vector2dList CopyWithOffset(Vector2d offset) {
			Vector2dList result = new Vector2dList();
			foreach (Vector2d v in this)
				result.Add(new Vector2d(v.X + offset.X, v.Y + offset.Y));
			return result;
		}

		/// <summary>Same as TryFind[index%count]</summary>
		public bool ByModulo(int index, ref Vector2d result) {
			int count = Count;
			if (count < 1)
				return false;

			int offset = index % count;

			return TryIndex(offset, ref result);
		}

	}

}