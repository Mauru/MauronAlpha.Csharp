using Microsoft.Xna.Framework.Graphics;
using MauronAlpha.MonoGame.Collections;
using MauronAlpha.MonoGame.Geometry;

namespace MauronAlpha.MonoGame.Geometry {

	public class TriangulationData:MonoGameComponent {

		public TriangulationData() : base() { }

		public TriangleList Triangles;
		public VertexPositionColor[] Vertices;
		public PolyShape Polygon;
		
		VertexBuffer _vertexBuffer;
		public VertexBuffer GetVertexBuffer(GraphicsDevice device) {
			if(_vertexBuffer == null) { 
				_vertexBuffer = new VertexBuffer(device, typeof(VertexPositionColor), Vertices.Length, BufferUsage.WriteOnly);
				_vertexBuffer.SetData<VertexPositionColor>(Vertices);
			}
			return _vertexBuffer;
		}

	}
}
