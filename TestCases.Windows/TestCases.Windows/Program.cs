using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace TestCases.Windows {
	class Program {
		static void Main (string[] args) {
			Rectangle2d rect = new Rectangle2d(0,0,20,20);

			Console.WriteLine(rect.Bounds.Width);
			Console.ReadKey();
		}
	}
}
