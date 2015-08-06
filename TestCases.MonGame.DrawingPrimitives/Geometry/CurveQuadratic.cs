using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.HandlingData;

using System;

namespace MauronAlpha.MonoGame.Geometry {
	public class CurveQuadratic:MonoGameComponent {

		MauronCode_dataList<Vector2d> DATA_points;
		public MauronCode_dataList<Vector2d> Points { get { return DATA_points; } }

		public CurveQuadratic(Vector2d start, Vector2d end, Vector2d ctrl, int resolution) {
			MauronCode_dataList<Vector2d> result = new MauronCode_dataList<Vector2d>();
			for (int step = 0; step < resolution; step++) {

				double x = Math.Pow(1 - step, 2) * start.X +
					(1 - step) * 2 * step * end.X +
					step * step * ctrl.X;
				double y = Math.Pow(1 - step, 2) * start.Y +
					(1 - step) * 2 * step * end.Y +
					step * step * ctrl.Y;

				Vector2d point = new Vector2d(x,y);
				result.Add(point);
			}

			DATA_points = result;

		}

	}
}
