using System;

using MauronAlpha.HandlingData;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace TestCases.Windows {
	
    class Program {

		static void Main (string[] args) {

            Console.WriteLine( " - Program Start - " );

			Rectangle2d rect = new Rectangle2d(0, 0, 20, 20);
			Console.WriteLine( rect.Points.Count );
			Console.WriteLine( rect.TransformedPoints.AsString );

            Console.WriteLine( " - Program End - " );
			Console.ReadKey();
		}

	}

}
