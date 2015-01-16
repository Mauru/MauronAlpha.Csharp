using MauronAlpha.Geometry.Geometry2d.Collections;

namespace MauronAlpha.Geometry.Geometry2d.Interfaces {
	
	interface I_polygon2d<T_shape>:I_polygonShape2d {

		Segment2dList Segments { get; }

	}

}
