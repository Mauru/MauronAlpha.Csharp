using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MauronAlpha.MonoGame.Test {
	public class TestTriangle {
		public VertexPositionColor[] vertices;

		public void Setup() {
			vertices = new VertexPositionColor[3];

			vertices[0].Position = new Vector3(-0.5f, -0.5f, 0f);
			vertices[0].Color = Color.Red;
			vertices[1].Position = new Vector3(0, 0.5f, 0f);
			vertices[1].Color = Color.Green;
			vertices[2].Position = new Vector3(0.5f, -0.5f, 0f);
			vertices[2].Color = Color.Yellow;
		}

		public VertexPositionColor[] Vertices {
			get { return vertices; }
		}
	}
}
