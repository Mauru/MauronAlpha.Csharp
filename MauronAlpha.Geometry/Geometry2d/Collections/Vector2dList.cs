using System.Collections.Generic;
using MauronAlpha.HandlingData;

using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

    //A collection of vectors
	public class Vector2dList:MauronCode_dataList<Vector2d> {

        //Constructor
        private Vector2dList() : base() { }
        public Vector2dList(ICollection<Vector2d> points):this() {
            foreach (Vector2d v in points) {
                Add(v.Instance);
            }
        }

        //Clone the vectors in the list
		public Vector2dList Cloned {
			get {
				Vector2dList r = new Vector2dList();
				foreach(Vector2d v in this){
					r.Add(v.Instance);
				}
				return r;			
			}
		}
        public new Vector2dList Instance {
            get {
                Vector2dList result = new Vector2dList();
                foreach (Vector2d v in this) {
                    result.Add(v);
                }
                return result;
            }
        }
        public new Vector2dList SetIsReadOnly(bool state) {
            base.SetIsReadOnly(state);
            return this;
        }
    }
}