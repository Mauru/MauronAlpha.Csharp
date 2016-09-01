using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.HandlingData;

using System;

namespace MauronAlpha.MonoGame.Geometry {
	
	//A cubic curve
	public class CurveCubic:MonoGameComponent {

		MauronCode_dataList<Vector2d> DATA_points;
		public MauronCode_dataList<Vector2d> Points { get { return DATA_points; } }

		public CurveCubic(Vector2d start, Vector2d end, Vector2d ctrlStart, Vector2d ctrlEnd, int resolution) {
			MauronCode_dataList<Vector2d> result = new MauronCode_dataList<Vector2d>();
			for (int step = 0; step < resolution; step++) {

				double x = Math.Pow(1 - step, 3) * start.X +
					Math.Pow(1 - step, 2) * 3 * step * end.X +
					(1 - step) * 3 * step * step * ctrlStart.X +
					step * step * step  * ctrlEnd.X;

				double y = Math.Pow(1 - step, 3) * start.Y +
					Math.Pow(1 - step, 2) * 3 * step * end.Y +
					(1 - step) * 3 * step * step * ctrlStart.Y +
					step * step * step * ctrlEnd.Y;

				Vector2d point = new Vector2d(x,y);
				result.Add(point);
			}

			DATA_points = result;

		}

	}

}
