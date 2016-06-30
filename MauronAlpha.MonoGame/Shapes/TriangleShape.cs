namespace MauronAlpha.MonoGame.Shapes {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.Geometry.Geometry2d.Units;
		using MauronAlpha.MonoGame.Collections;

	public class TriangleShape :MonoGameShape {
		public TriangleShape(GameManager game, float width, float height) : base(game) {
			DATA_pt = new List<Pt>() {
				new Pt(width/2,0),
				new Pt(width,height),
				new Pt(0,height)
			};
		}

		public override Pt Size {
			get { return new ShapeSize(DATA_pt[1]); }
		}

		public override Triangles Triangles {
			get {
				return new Triangles() {
					new Triangle(DATA_pt[0], DATA_pt[1], DATA_pt[2])
				};
			}
		}

		public override Rectangle Bounds {
			get {
				Pt max = DATA_pt[1];
				return new Rectangle(0,0,(float)max.X,(float)max.Y);
			}
		}

		List<Pt> DATA_pt;
		public override List<Pt> Points {
			get { return DATA_pt; }
		}
	}
}
