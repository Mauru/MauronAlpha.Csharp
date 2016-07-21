using MauronAlpha.HandlingData;

using MauronAlpha.Geometry.Geometry2d.Utility;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MauronAlpha.MonoGame.Collections {


	public class TriangleList:MonoGameComponent {

		//Properties
		private MauronCode_dataList<Polygon2d> DATA_triangles = new MauronCode_dataList<Polygon2d>();

		//Constructors
		public TriangleList(MauronCode_dataList<Polygon2d> list):base() {
			DATA_triangles = list;
		}
		public TriangleList(Polygon2d poly):base() {
			Triangulator2d tool = new Triangulator2d();
			DATA_triangles = tool.Triangulate(poly);
		}
		public TriangleList(Triangle2d[] triangles) : base() {
			DATA_triangles = new MauronCode_dataList<Polygon2d>(triangles);
		}

		//Proxy Properties
		public int Count { get { return DATA_triangles.Count; } }

		//Methods
		public VertexPositionColor[] AsPositionColor {
			get {
				int count = DATA_triangles.Count * 3;
				VertexPositionColor[] vertices = new VertexPositionColor[count];
				int triIndex = 0;
				foreach (Polygon2d poly in DATA_triangles) {

					vertices[triIndex].Position = ConvertVector(poly.Point(0));
					vertices[triIndex].Color = Color.Red;
					triIndex++;
					vertices[triIndex].Position = ConvertVector(poly.Point(1));
					vertices[triIndex].Color = Color.Green;
					triIndex++;
					vertices[triIndex].Position = ConvertVector(poly.Point(2));
					vertices[triIndex].Color = Color.Yellow;
					triIndex++;

				}

				return vertices;

			}
		}

		public Vector3 ConvertVector(Vector2d v) {
			return new Vector3((float)v.X, (float)v.Y, 0F);
		}

	}

}
