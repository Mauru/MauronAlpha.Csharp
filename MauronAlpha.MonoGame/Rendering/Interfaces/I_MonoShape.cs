namespace MauronAlpha.MonoGame.Interfaces {

	using Microsoft.Xna.Framework.Graphics;


	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Transformation;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Geometry;
	
	public interface I_MonoShape:I_Renderable {
		ShapeDefinition Definition { get; }

		Vector2d Center { get; }
		Matrix2d Matrix { get; }

		VertexBuffer GenerateVertexBuffer(long time);
		TriangulationData TriangulationData { get; }

	}

}
