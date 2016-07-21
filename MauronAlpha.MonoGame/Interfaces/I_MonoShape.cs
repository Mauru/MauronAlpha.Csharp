using MauronAlpha.MonoGame.Geometry;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Transformation;
using MauronAlpha.Geometry.Geometry2d.Collections;

namespace MauronAlpha.MonoGame.Interfaces {
	
	public interface I_MonoShape {
		ShapeDefinition Definition { get; }

		Vector2d Center { get; }
		Polygon2dBounds Bounds { get; }
		Matrix2d Matrix { get; }

	}

}
