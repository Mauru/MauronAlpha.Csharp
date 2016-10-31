namespace MauronAlpha.MonoGame.Rendering.Collections {

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Geometry;

	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Utility;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.HandlingData;

	/// <summary> Holds the result of a polygon-triangulation </summary>
	public class TriangulationData:MonoGameComponent {

		public TriangulationData(VertexPositionColor[] vertices, int vertexCount): base() {
			_vertices = vertices;
			_triangleCount = vertexCount / 3;
			_vertexCount = vertexCount;
		}
		
		VertexPositionColor[] _vertices;
		public VertexPositionColor[] Vertices { get { return _vertices; } }

		int _triangleCount;
		public int TriangleCount { get { return _triangleCount; } }

		int _vertexCount;
		public int VertexCount { get { return _vertexCount; } }


		//Utility methods
		public static VertexBuffer CreateVertexBuffer(GraphicsDevice device, VertexPositionColor[] data, int count) {
			VertexBuffer buffer = new VertexBuffer(device, typeof(VertexPositionColor), count, BufferUsage.WriteOnly);
			buffer.SetData<VertexPositionColor>(data);
			return buffer;
		}
		public static VertexPositionColor[] CreateVertexPositionColor(int count, MauronCode_dataList<Polygon2d> triangles, Color[] colors) {
			VertexPositionColor[] result = new VertexPositionColor[count];
			int index = 0;
			int colorIndex = 0;

			foreach (I_polygonShape2d shape in triangles) {
				colorIndex = 0;
				foreach(Vector2d pt in shape.Points.Reverse()) {
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

		public static TriangulationData CreateFromShape(I_polygonShape2d shape, Color[] colors) {
			Triangulator2d tool = new Triangulator2d();
			//TODO: still have to make triangle thing static
			MauronCode_dataList<Polygon2d> triangles = tool.Triangulate(shape.Points);
			int triangleCount = triangles.Count;
			int vertexCount = triangleCount * 3;
			VertexPositionColor[] vtp = TriangulationData.CreateVertexPositionColor(vertexCount, triangles, colors);
			TriangulationData data = new TriangulationData(vtp,vertexCount);
			return data;
		}

		//Debug functions
		public static string DebugVertexPositionColor(VertexPositionColor[] a){
			string result = "";
			foreach (VertexPositionColor p in a)
				result += "[" + p.Position.X + "," + p.Position.Y + "]";
			return result;
		}
		public static string DebugTriangleList(MauronCode_dataList<Polygon2d>a) {
			string result = "";
			foreach (Polygon2d p in a)
				result += TriangulationData.DebugPolygonShape(p);
			return result;
		}
		public static string DebugPolygonShape(I_polygonShape2d p) {
			string result = "";
			foreach (Vector2d v in p.Points)
				result += "[" + v.X + "," + v.Y + "]";
			return result;
		}
		public string AsString {
			get {
				string result = "{"+TriangleCount+"}";
				foreach (VertexPositionColor v in _vertices)
					result += "[" + v.Position.X + "," + v.Position.X + "]";
				return result;
			}
		}
	}

}
