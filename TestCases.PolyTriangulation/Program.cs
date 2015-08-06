using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Utility;
using MauronAlpha.Geometry.Geometry2d.Collections;

namespace TestCases.PolyTriangulation {
	class Program {
		static void Main(string[] args) {
			Rectangle2d rect = new Rectangle2d(0, 0, 200, 200);
			System.Console.WriteLine(rect.AsString);
			Triangulator2d tool = new Triangulator2d();
			MauronCode_dataList<Polygon2d> list = tool.Triangulate(rect);
			foreach (Polygon2d poly in list)
				System.Console.WriteLine(poly.AsString);
			System.Console.ReadKey();
		}
	}
}
