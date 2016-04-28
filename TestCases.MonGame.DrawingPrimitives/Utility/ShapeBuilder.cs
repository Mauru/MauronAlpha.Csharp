using Microsoft.Xna.Framework.Graphics;

using MauronAlpha.MonoGame.Collections;
using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.MonoGame.Geometry;

using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Utility;

namespace MauronAlpha.MonoGame.Utility {
	
	public class ShapeBuilder:MonoGameComponent {

		private GraphicsDevice Device;
		private Triangulator2d Triangulator = new Triangulator2d();

		public ShapeBuilder(GraphicsDevice device) : base() {
			Device = device;
		}

		public TriangulationData Triangulate(PolyShape poly) {

			TriangulationData data = new TriangulationData();
			data.Polygon = poly;
			data.Triangles = new TriangleList(poly);
			Debug("Triangles", data.Triangles.Count);
			data.Vertices  = data.Triangles.AsPositionColor;
			Debug("Vertices", data.Vertices.Length);
			data.VertexBuffer = new VertexBuffer(Device , typeof(VertexPositionColor), data.Vertices.Length, BufferUsage.WriteOnly);
			data.VertexBuffer.SetData<VertexPositionColor>(data.Vertices);

			return data;
		}

		private void Debug(string msg) {
			System.Console.WriteLine(msg);
		}

		private void Debug(string msg, double value) {
			System.Console.WriteLine(msg+" : " +value);
		}

		public VertexBuffer ToBuffer(PolyShape poly) {
			return Triangulate(poly).VertexBuffer;
		}
		public VertexBuffer ToBuffer(TriangleList triangles) {
			VertexPositionColor[] vertices = triangles.AsPositionColor;
			VertexBuffer result = new VertexBuffer(Device, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
			result.SetData<VertexPositionColor>(vertices);
			return result;
		}
	}

}
