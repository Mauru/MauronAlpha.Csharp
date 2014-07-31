using MauronAlpha.HandlingData;

namespace MauronAlpha.Geometry.Geometry2d.Units {

	public class Vector2dList:MauronCode_dataList<Vector2d> {
		public Vector2dList Instance {
			get {
				Vector2dList ret = new Vector2dList(this);
				return ret;
			}
		}
		public Vector2dList(){}
		public Vector2dList(Vector2dList list) {}
	}

}