using MauronAlpha.HandlingData;

namespace MauronAlpha.Geometry.Geometry2d.Units {

	public class Vector2dList:MauronCode_dataList<Vector2d> {
		
		new public Vector2dList Instance {
			get {
				Vector2dList r = new Vector2dList();
				foreach(Vector2d v in this){
					r.Add(v.Instance);
				}
				return r;			
			}
		}
	}

}