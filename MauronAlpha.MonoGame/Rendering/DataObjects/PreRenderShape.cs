namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class PreRenderShape : MonoGameComponent {

		public PreRenderShape(Vector2dList baseShapeAlignedTopLeft): base() {
			_baseShape = baseShapeAlignedTopLeft;
			_baseBounds = new Polygon2dBounds(baseShapeAlignedTopLeft);

			if (!_baseBounds.Min.IsZero) {
				Vector2d offset = _baseBounds.Min.Copy.Inverted;
				_baseShape.Offset(offset);
				_baseBounds.Offset(offset);
			}

			int count = 0;
			_vertices = ShapeDrawCall.CreateVertexPostionData(_baseShape, ref count);

			_mode = VertexShaderMode.VertexPosition2d;
			_transform = TransformationData2D.AlignedTopLeft(_baseBounds.Center);
		}

		Vector2dList _baseShape;
		Polygon2dBounds _baseBounds;

		Vector2dList _transformedShape;
		Polygon2dBounds _transformedBounds;

		int _vertexCount;
		VertexPosition[] _vertices;
		VertexPositionColor[] _colorVertices;

		VertexShaderMode _mode;
		public VertexShaderMode ShaderMode { get { return _mode; } }

		Color _color;

		TransformationData2D _transform;
	}

}