using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Geometry.Geometry2d.Collections {

	//A list of segments in 2d coordinate space
	public class Segment2dList : MauronCode_dataList<Segment2d> {

		public new Segment2dList AddValue(Segment2d segment) {
			base.AddValue(segment);
			return this;
		}

		public static Segment2dList Empty { get { return new Segment2dList(); } }
	}
}