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
	
	public class ShapeBuffer:List<I_polygonShape2d>{
		List<TriangulationData> _triangulationData;
		public List<TriangulationData> TriangulatedObjects {
			get {
				if (_triangulationData == null)
					_triangulationData = new List<TriangulationData>();
				return _triangulationData;
			}
		}
		public bool IsTriangulated {
			get {
				if (_triangulationData == null)
					return false;
				return true;
			}
		}

		/// <summary> Triangulation Process - Call this during the Initialization Phase since there is currently no "update" call if new elements are added afterwards.</summary>
		public int Triangulate(GameRenderer renderer, Color[] colors, Vector2d offset) {

			if (_triangulationData == null)
				_triangulationData = new List<TriangulationData>();

			int count = 0;

			//Triangulation Process
			foreach (I_polygonShape2d shape in this) {
				TriangulationData data = TriangulationData.CreateFromShape(renderer,shape,colors,offset);
				_triangulationData.Add(data);
				count++;
			}

			return count;
		}
		public int Triangulate(GameRenderer renderer, Color color) {
			Color[] colors = new Color[3] { color, color, color };
			return Triangulate(renderer, colors, Vector2d.Zero);
		}

		public static ShapeBuffer Empty { get { return new ShapeBuffer(); } }

	}

}