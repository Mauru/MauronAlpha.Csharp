using MauronAlpha.Geometry.Geometry3d.Units;
using MauronAlpha.MonoGame.HexGrid.Collections;
using System;

namespace MauronAlpha.MonoGame.HexGrid.Units {
	
	public class Hex:HexGridComponent {

		public bool IsEmpty = true;
		bool IsEven {
			get {
				if (Coordinates.Y == 0)
					return true;
				return Coordinates.Y % 2 == 0;
			}
		}
		bool B_notFound = false;		
		bool IsNotFound { get { return B_notFound; } }
		

		public Hex SetNotFound(bool state) {
			B_notFound = state;
			return this;
		}

		public Hex() : base() {
			Neighbors = new HexNeighbors(this);
		}
		public Hex(Vector3d position) : this() {
			DATA_coordinates = position;
		}
		public Hex(int x, int y, int z) : this(new Vector3d(x, y, z)) { }
		public Hex(Grid grid):base() {
			DATA_grid = grid;
			Neighbors = new HexNeighbors(this); 
		}
		public Hex(Grid grid, Vector3d position):this(grid) {
			DATA_coordinates = position;
		}

		public Hex NotFound {
			get {
				SetNotFound(true);
				return this;
			}
		}

		Vector3d DATA_coordinates = new Vector3d();
		public Vector3d Coordinates { get { return DATA_coordinates; } }
		public double X {
			get {
				return  DATA_coordinates.X;
			}
		}
		public double Y {
			get {
				return DATA_coordinates.Y;
			}
		}
		public double Z {
			get {
				return DATA_coordinates.Z;
			}
		}

		Grid DATA_grid;
		public Grid Grid { get { return DATA_grid; } }

		public HexNeighbors Neighbors;

		public Vector3d Traverse(int x, int y) {
			Hex hex = this;
			for (int oX = 0; oX < x; oX++)
				hex = new Hex(hex.Neighbors.Right);
			for(int oY=0;oY<y;y++) {
				if (hex.IsEven)
					hex = new Hex(hex.Neighbors.BottomRight);
				else
					hex = new Hex(hex.Neighbors.BottomLeft);
			}
			return hex.Coordinates;
		}

		public double Distance(Hex other) {
			return (Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z)) / 2;
		}
	}

}
