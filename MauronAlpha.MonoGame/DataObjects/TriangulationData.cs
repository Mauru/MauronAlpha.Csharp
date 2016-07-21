using Microsoft.Xna.Framework.Graphics;
using MauronAlpha.MonoGame.Collections;
using MauronAlpha.MonoGame.Geometry;

namespace MauronAlpha.MonoGame.DataObjects {

	public class TriangulationData:MonoGameComponent {

		public TriangulationData() : base() { }

		public VertexBuffer VertexBuffer;
		public TriangleList Triangles;
		public VertexPositionColor[] Vertices;
		public PolyShape Polygon;

	}
}
