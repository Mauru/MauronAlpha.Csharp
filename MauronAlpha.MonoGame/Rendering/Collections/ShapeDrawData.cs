namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Geometry;

	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Utility;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.HandlingData;

	/// <summary> Instructions for drawing a triangulated 2dPolygon. </summary>
	public class ShapeDrawCall : MonoGameComponent {

		public ShapeDrawCall(VertexPositionColor[] vertices, int vertexCount): base() {
			_vertexPositionColor = vertices;
			_triangleCount = vertexCount / 3;
			_vertexCount = vertexCount;
			_mode = VertexShaderMode.VertexPositionColor2d;
		}
		public ShapeDrawCall(VertexPosition[] vertices, int vertexCount): base() {
			_vertexPosition = vertices;
			_triangleCount = vertexCount / 3;
			_vertexCount = vertexCount;
			_mode = VertexShaderMode.VertexPosition2d;
		}

		VertexPositionColor[] _vertexPositionColor;
		public VertexPositionColor[] VertexPositionColor { get { return _vertexPositionColor; } }

		VertexPosition[] _vertexPosition;
		public VertexPosition[] VertexPosition { get { return _vertexPosition; } }

		int _triangleCount;
		public int TriangleCount { get { return _triangleCount; } }

		int _vertexCount;
		public int VertexCount { get { return _vertexCount; } }

		VertexShaderMode _mode;
		public VertexShaderMode VertexShaderMode { get {
			return _mode;
		} }

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

		//Utility methods
		public static VertexBuffer CreateVertexBuffer(GraphicsDevice device, VertexPositionColor[] data, int count) {
			VertexBuffer buffer = new VertexBuffer(device, typeof(VertexPositionColor), count, BufferUsage.WriteOnly);
			buffer.SetData<VertexPositionColor>(data);
			return buffer;
		}
		public static VertexBuffer CreateVertexBuffer(GraphicsDevice device, VertexPosition[] data, int count) {
			VertexBuffer buffer = new VertexBuffer(device, typeof(VertexPosition), count, BufferUsage.WriteOnly);
			buffer.SetData<VertexPosition>(data);
			return buffer;
		}
		public static VertexPositionColor[] CreateVertexPositionColor(int count, TriangleList2d triangles, Color[] colors) {
			VertexPositionColor[] result = new VertexPositionColor[count];
			int index = 0;
			int colorIndex = 0;

			foreach (I_polygonShape2d shape in triangles) {
				colorIndex = 0;
				foreach (Vector2d pt in shape.Points.Reverse()) {
					VertexPositionColor process = new VertexPositionColor() {
						Color = colors[colorIndex],
						Position = new Microsoft.Xna.Framework.Vector3(pt.FloatX, pt.FloatY, 0)
					};

					colorIndex++;
					result[index] = process;
					index++;
				}
			}

			return result;
		}


		public static Color[] WhiteVertexColors {
			get { return new Color[3] { Color.White, Color.White, Color.White }; }
		}

		public static ShapeDrawCall CreateFromShape(I_polygonShape2d shape, Color[] colors) {
			Triangulator2d tool = new Triangulator2d();
			Vector2dList points = shape.Points;

			//TODO: still have to make triangle thing static
			TriangleList2d triangles = tool.Triangulate(points);
			int triangleCount = triangles.Count;
			int vertexCount = triangleCount * 3;
			VertexPositionColor[] vtp = ShapeDrawCall.CreateVertexPositionColor(vertexCount, triangles, colors);
			ShapeDrawCall data = new ShapeDrawCall(vtp, vertexCount);
			data.SetBounds(Polygon2dBounds.FromPoints(points));
			return data;
		}
		public static ShapeDrawCall CreateFromShape(I_polygonShape2d shape, Color[] colors, Matrix2d matrix) {
			Triangulator2d tool = new Triangulator2d();
			Vector2dList points = shape.Points;

			points = matrix.ApplyToCopy(points);

			//TODO: still have to make triangle thing static
			TriangleList2d triangles = tool.Triangulate(points);
			int triangleCount = triangles.Count;
			int vertexCount = triangleCount * 3;
			VertexPositionColor[] vtp = ShapeDrawCall.CreateVertexPositionColor(vertexCount, triangles, colors);
			ShapeDrawCall data = new ShapeDrawCall(vtp, vertexCount);
			data.SetBounds(Polygon2dBounds.FromPoints(points));
			return data;
		}
		public static ShapeDrawCall CreateFromVector2dList(Vector2dList points, Color[] colors) {
			Triangulator2d tool = new Triangulator2d();

			//TODO: still have to make triangle thing static
			TriangleList2d triangles = tool.Triangulate(points);
			int triangleCount = triangles.Count;
			int vertexCount = triangleCount * 3;
			VertexPositionColor[] vtp = ShapeDrawCall.CreateVertexPositionColor(vertexCount, triangles, colors);
			ShapeDrawCall data = new ShapeDrawCall(vtp, vertexCount);
			data.SetBounds(Polygon2dBounds.FromPoints(points));
			return data;
		}
		public static ShapeDrawCall CreateFromVector2dList(Vector2dList points, Color color) {
			return CreateFromVector2dList(points, new Color[3] { color, color, color });
		}
		public static ShapeDrawCall CreateFromVector2dList(Vector2dList points, System.Nullable<Color> color, Polygon2dBounds bounds) {
			ShapeDrawCall result;
			if (color == null)
				result = CreateFromVector2dList(points, ShapeDrawCall.WhiteVertexColors);
			else
				result = CreateFromVector2dList(points, color.Value);

			result.SetBounds(bounds);
			return result;
		}

		public static Polygon2dBounds CalculateBounds(ShapeDrawCall data) {
			if (data.VertexShaderMode.Equals(VertexShaderMode.VertexPosition2d))
				return CalculateBounds(data.VertexPosition);
			else
				return CalculateBounds(data.VertexPositionColor);
		}
		public static Polygon2dBounds CalculateBounds(VertexPositionColor[] data) {
			Vector2d min = null;
			Vector2d max = null;
			Vector3 p;
			foreach (VertexPositionColor v in data) {
				p = v.Position;
				if (min == null) {
					min = new Vector2d(p.X, p.Y);
					max = new Vector2d(p.X, p.Y);
				}
				else {
					if (min.X > p.X)
						min.SetX(p.X);
					if (min.Y > p.Y)
						min.SetY(p.Y);
					if (max.X < p.X)
						max.SetX(p.X);
					if (max.Y < p.Y)
						max.SetY(p.Y);
				}
			}
			return Polygon2dBounds.FromMinMax(min, max);
		}
		public static Polygon2dBounds CalculateBounds(VertexPosition[] data) {
			Vector2d min = null;
			Vector2d max = null;
			Vector3 p;
			foreach (VertexPosition v in data) {
				p = v.Position;
				if (min == null) {
					min = new Vector2d(p.X, p.Y);
					max = new Vector2d(p.X, p.Y);
				}
				else {
					if (min.X > p.X)
						min.SetX(p.X);
					if (min.Y > p.Y)
						min.SetY(p.Y);
					if (max.X < p.X)
						max.SetX(p.X);
					if (max.Y < p.Y)
						max.SetY(p.Y);
				}
			}
			return Polygon2dBounds.FromMinMax(min, max);
		}

		public static VertexPosition[] CreateVertexPostionData(Vector2dList points, ref int vertexCount) {
			Triangulator2d tool = new Triangulator2d();
			TriangleList2d list = tool.Triangulate(points);
			vertexCount = list.Count * 3;
			VertexPosition[] result = ShapeDrawCall.CreateVertexPositionData(list, vertexCount);
			return result;
		}
		public static VertexPosition[] CreateVertexPositionData(TriangleList2d list, int vertexCount) {
			VertexPosition[] result = new VertexPosition[vertexCount];
			int index = 0;

			foreach (I_polygonShape2d shape in list) {
				foreach (Vector2d pt in shape.Points.Reverse()) {
					VertexPosition process = new VertexPosition() {
						Position = new Microsoft.Xna.Framework.Vector3(pt.FloatX, pt.FloatY, 0)
					};
					result[index] = process;
					index++;
				}
			}

			return result;
		}

		//Debug functions
		public static string DebugVertexPositionColor(VertexPositionColor[] a) {
			string result = "";
			foreach (VertexPositionColor p in a)
				result += "[" + p.Position.X + "," + p.Position.Y + "]";
			return result;
		}
		public static string DebugTriangleList(MauronCode_dataList<Polygon2d> a) {
			string result = "";
			foreach (Polygon2d p in a)
				result += ShapeDrawCall.DebugPolygonShape(p);
			return result;
		}
		public static string DebugPolygonShape(I_polygonShape2d p) {
			string result = "";
			foreach (Vector2d v in p.Points)
				result += "[" + v.X + "," + v.Y + "]";
			return result;
		}

	}
}