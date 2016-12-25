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

		public ShapeBuffer(): base() {}
		public ShapeBuffer(I_polygonShape2d shape): base() {
			Add(shape);
		}
		public ShapeBuffer(Matrix2d matrix): base() {
			_matrix = matrix;
		}
		public static ShapeBuffer Empty { get { return new ShapeBuffer(); } }

		public void Add(I_polygonShape2d shape) {
			TriangulationData data;
			if(_matrix!=null)
				 data = TriangulationData.CreateFromShape(shape, TriangulationData.WhiteVertexColors, _matrix);
			else
				data = TriangulationData.CreateFromShape(shape, TriangulationData.WhiteVertexColors);
			Add(data);
		}

		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				if (_bounds == null)
					_bounds = CalculateBounds(this);
				return _bounds;
			}
		}
		public void SetBounds(Polygon2dBounds bounds) {
			_bounds = bounds;
		}

		Matrix2d _matrix;
		public Matrix2d Matrix {
			get {
				if (_matrix == null)
					_matrix = Matrix2d.Identity;
				return _matrix;
			}
		}

		public static Polygon2dBounds CalculateBounds(ShapeBuffer buffer) {
			if (buffer.IsEmpty)
				return Polygon2dBounds.Empty;
			Polygon2dBounds bounds;
			Polygon2dBounds result = Polygon2dBounds.Empty;
			Vector2d min = null;
			Vector2d max= null;
			Vector2d m1; Vector2d m2;
			foreach (TriangulationData data in buffer) { 
				bounds = data.Bounds;
				m1 = bounds.Min;
				m2 = bounds.Max;

				if (min == null) {
					min = m1.Copy; max = m2.Copy;
				}
				else {
					if (min.X > m1.X)
						min.SetX(m1.X);
					if (min.Y > m1.Y)
						min.SetY(m1.Y);
					if (max.X < m2.X)
						max.SetX(m2.X);
					if (max.Y < m2.Y)
						max.SetY(m2.Y);
				}				
			}
			return Polygon2dBounds.FromMinMax(min, max);
		}
	}

}