using MauronAlpha.HandlingData;
using MauronAlpha.Geometry.Geometry3d.Units;
using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.MonoGame.HexGrid.Units {
	
	public class Grid:HexGridComponent {
		MauronCode_dataReference<Vector3d, Hex> DATA_map = new MauronCode_dataReference<Vector3d, Hex>();

		int INT_rows = 0;
		int INT_columns = 0;
		public int Rows {
			get {
				return INT_rows;
			}
		}
		public int Columns {
			get {
				return INT_columns;
			}
		}

		public Grid() : base() { }
		public Grid(int rows, int columns):base() {
			INT_rows = rows;
			INT_columns = columns;
			Populate();
		}
		public Grid(Vector2d size) : this( size.IntY, size.IntX) { }

		public void Populate() {	
			Hex source = new Hex();

			for (int indexRow = 0; indexRow < Rows; indexRow++) {
				for (int indexColumn = 0; indexColumn < Columns; indexColumn++) {
					Vector3d coordinates = source.Traverse(indexRow, indexColumn);
					Hex hex = new Hex(this, coordinates);
					DATA_map.SetValue(coordinates, hex);
				}				
			}
		}

		public Vector3d HexToCube(Vector2d hex) {
			double x = hex.X;
			double z = hex.Y;
				double y = -x-z;
			return new Vector3d(x,y,z);
		}

		public Hex Hex(int x, int y, int z) {
			Hex hex;
			bool contains = DATA_map.TryFind(new Vector3d(x,y,z), out hex);
			if (!contains)
				return new Hex(x, y, z).NotFound;
			return hex;
		}
		public bool Contains(int x, int y, int z) {
			return DATA_map.ContainsKey(new Vector3d(x, y, z));
		}

		public bool TryFind(Vector3d location, out Hex result) {
			return DATA_map.TryFind(location, out result);
		}

	}

	public class DynamicGrid : Grid {

		MauronCode_dataReference<Vector3d, Hex> DATA_map = new MauronCode_dataReference<Vector3d, Hex>();

		public DynamicGrid() : base() { }

		public static Vector3d StartLocation {
			get {
				return new Vector3d();
			}
		}

		public Hex Start {

			get {

				Hex start = null;
				if (!DATA_map.TryFind(DynamicGrid.StartLocation, out start)) {
					start = new Hex(this, DynamicGrid.StartLocation);
					DATA_map.SetValue(DynamicGrid.StartLocation, start);
				}
				return start;

			}

		}

		public bool TryFind(Vector3d location, out Hex result) {
			return DATA_map.TryFind(location, out result);
		}

	}

}
