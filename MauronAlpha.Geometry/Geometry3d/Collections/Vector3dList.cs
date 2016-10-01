namespace MauronAlpha.Geometry.Geometry3d.Collections {
	using MauronAlpha.HandlingData;
	using MauronAlpha.Geometry.Geometry3d.Units;

	public class Vector3dList:MauronCode_dataList<Vector3d> {

		public bool Equals(Vector3dList other) {

			int count = Count;

			if(count != other.Count)
				return false;

			for(int n=0;n<count;n++) {

				Vector3d a = Value(n);
				Vector3d b = other.Value(n);

				if(!a.Equals(b))
					return false;

			}

			return true;

		}

		public Vector3d Min {

			get {

				Vector3d result = null;
				foreach(Vector3d v in this) {

					if(result == null)
						result = v;
					else if(v.CompareTo(result)<0) {
						result = v;
					}

				}

				return result;

			}

		}
		public Vector3d Max {

			get {

				Vector3d result = null;
				foreach(Vector3d v in this) {

					if(result == null)
						result = v;
					else if(v.CompareTo(result)>0) {
						result = v;
					}

				}

				return result;

			}

		}

		public Vector3dList Append(System.Collections.Generic.ICollection<Vector3d> vv) {
			foreach (Vector3d v in vv)
				Add(v);
			return this;
		}

		/* Incomplete
		Vector3dList RotateAroundAxis(Ray3d s, double degree) {

			// 1: translate so axis passes through origin
			// 2: rotate space along x axis so rotate axis lies in xz plane
			// 3: rotate space along y axis so rotate axis lies along z axis
			// 4: perform rotation along z axis
			
			// 5: inverse of 3
			// 6: inverse of 2
			// 7: inverse of 1


		}*/

	}
}
