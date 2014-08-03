using MauronAlpha;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Geometry {

	//base class for Geometry components
	public abstract class GeometryComponent : MauronCode_dataObject {
		public GeometryComponent ( ) : base(DataType_object.Instance) { }
	}

}