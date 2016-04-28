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

	}
}
