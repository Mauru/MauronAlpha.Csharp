namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.HandlingData;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Utility;

	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.Geometry.Geometry2d.Transformation;
	
	public class ShapeBuffer:List<TriangulationData>{


		public static ShapeBuffer Empty { get { return new ShapeBuffer(); } }

		
		Matrix2d _matrix;
		public Matrix2d Matrix {
			get {
				if (_matrix == null)
					_matrix = Matrix2d.Identity;
				return _matrix;
			}
		}
	}

}