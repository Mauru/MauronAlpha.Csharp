using MauronAlpha.MonoGame.HexGrid.Units;
using MauronAlpha.Geometry.Geometry3d.Units;
using MauronAlpha.HandlingData;
using System;

namespace MauronAlpha.MonoGame.HexGrid.Collections {
	public class HexLine:HexGridComponent {

		public HexLine(Hex start, Hex end):base() {
			
			MauronCode_dataList<Vector3d> result = new MauronCode_dataList<Vector3d>();
			double distance = start.Distance(end);
			double offsetStart = 0;
			double offsetEnd = distance + 1;
			while(offsetStart < offsetEnd) {
				double offset  = offsetStart++;
				double steps = 1.0 / Math.Max(1,distance) * offset;
				Vector3d interpolated = Round(Interpolate(start,end, steps));
				result.Add(interpolated);

			}
			DATA_members = result;
		}

		public Vector3d Interpolate(Hex start, Hex end, double steps) {

			double x = start.X + (end.X-start.X) * steps;
			double y = start.Y + (end.Y-start.Y) * steps;
			double z = start.Z + (end.Z-start.Z) * steps;

			return new Vector3d(x, y, z);
		}

		public Vector3d Round(Vector3d cube) {
			double rx = Math.Round(cube.X);
			double ry = Math.Round(cube.Y);
			double rz = Math.Round(cube.Z);
			double x_diff = Math.Abs(rx - cube.X);
			double y_diff = Math.Abs(ry - cube.Y);
			double z_diff = Math.Abs(rz - cube.Z);
			if (x_diff > y_diff && x_diff > z_diff) 
				rx = -ry - rz; 
			else if (y_diff > z_diff) 
				ry = -rx - rz; 
			else 
				rz = -rx - ry;
			return new Vector3d(rx, ry, rz);
		}

		public Hex Start;
		public Hex End;

		MauronCode_dataList<Vector3d> DATA_members;
		public MauronCode_dataList<Vector3d> Members { 
			get {
				return DATA_members;
			}
		}

	}
}
