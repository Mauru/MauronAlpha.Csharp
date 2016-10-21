using System;
namespace TestGeneric.cs {
	class Program {
		static void Main(string[] args) {

			int[] index = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int count = 10;
			int attempt = 11;

			Print(11 % count);

			Pause();

		}

		static void Print(double n) {
			System.Console.WriteLine("" + n);
		}

		static void Print(string message) {
			System.Console.WriteLine(message);
		}
		static void Pause() {
			System.Console.ReadKey();
		}

	}
}
