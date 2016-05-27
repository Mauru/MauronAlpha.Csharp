using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.HexGrid.Units;
using MauronAlpha.Geometry.Geometry3d.Units;

namespace MauronAlpha.MonoGame.HexGrid.Collections {
	public class HexNeighbors:HexGridComponent {

		Grid Grid { get { return Source.Grid; } }
		Hex Source;

		public HexNeighbors(Hex source)	: base() {
			Source = source;
		}

		public Vector3d Right {
			get {
				return Source.Coordinates.Instance.Add(1, 1, 0);
			}
		}
		public Vector3d TopRight {
			get {
				return Source.Coordinates.Instance.Add(1, 0, -1);
			}
		}
		public Vector3d TopLeft {
			get {
				return Source.Coordinates.Instance.Add(0, 1, -1);
			}
		}
		public Vector3d Left {
			get {
				return Source.Coordinates.Instance.Add(-1, 1, 0);
			}
		}
		public Vector3d BottomLeft {
			get {
				return Source.Coordinates.Instance.Add(-1, 0, 1);
			}
		}
		public Vector3d BottomRight {
			get {
				return Source.Coordinates.Instance.Add(0, -1, 1);
			}
		}
		public Vector3d Top {
			get {
				return HexNeighbors.TopOf(Source.Coordinates);
			}
		}
		public Vector3d Bottom {
			get {
				return HexNeighbors.BottomOf(Source.Coordinates);
			}
		}

		public static Vector3d TopRightOf(Vector3d vector) {
			return vector.Instance.Add(1, 0, -1);
		}
		public static Vector3d TopLeftOf(Vector3d vector) {
			return vector.Instance.Add(0, 1, -1);
		}
		public static Vector3d LeftOf(Vector3d vector) {
			return vector.Instance.Add(-1, 1, 0);
		}
		public static Vector3d BottomLeftOf(Vector3d vector) {
			return vector.Instance.Add(-1, 0, 1);
		}
		public static Vector3d BottomRightOf(Vector3d vector) {
			return vector.Instance.Add(0, -1, 1);
		}
		public static Vector3d TopOf(Vector3d vector) {
			return TopRightOf(TopLeftOf(vector));
		}
		public static Vector3d BottomOf(Vector3d vector) {
			return BottomRightOf(BottomLeftOf(vector));
		}
	}
}
