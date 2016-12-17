namespace TestingGeometry {
	using MauronAlpha;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Utility;
	using MauronAlpha.Geometry.Geometry3d.Transformation;
	using MauronAlpha.Geometry.Geometry3d.Units;

	class Program {

		static void Main(string[] args) {

			Segment2d s = new Segment2d(0, 0, 20, -20);
			Print(s.AngleDegree);
			Pause();

		}

		static void Main_old(string[] args) {
			Print(3 % 1);
			Matrix3d m = Matrix3d.RotationZDegree(360);
			Vector2d v = new Vector2d(2, 4);
			Print(m.ApplyTo(v).AsString);
			Matrix3d stretch = Matrix3d.Scale(10, 9, 8);
			Print(stretch.ApplyTo(1,1,1).AsString);

			Pause();
		}

		static void Print(double n) {
			System.Console.WriteLine(""+n);
		}

		static void Print(string message) {
			System.Console.WriteLine(message);
		}
		static void Pause() {
			System.Console.ReadKey();
		}

		static void Print(Vector2d vector) {
			System.Console.WriteLine(vector.AsString);
		}
		static void Print(Vector3d vector) {
			System.Console.WriteLine(vector.AsString);
		}

		static void Print(Matrix3d m) {
			System.Console.WriteLine(m[0, 0] + " " + m[0, 1] + " " + m[0, 2] + " " + m[0, 3]);
			System.Console.WriteLine(m[1, 0] + " " + m[1, 1] + " " + m[1, 2] + " " + m[1, 3]);
			System.Console.WriteLine(m[2, 0] + " " + m[2, 1] + " " + m[2, 2] + " " + m[2, 3]);
			System.Console.WriteLine(m[3, 0] + " " + m[3, 1] + " " + m[3, 2] + " " + m[3, 3]);
		}


	}
}
