using System;
using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.ConsoleApp {
	
	//The Window of the console
	public class ConsoleApp_window:SystemInterface {

		#region Constructor
		public ConsoleApp_window(MauronConsole console){
			SetTarget(console);
		}
		#endregion

		#region The related MauronConsole
		private MauronConsole C_target;
		public MauronConsole Target {
			get {
				if( C_target==null ) {
					NullError("Console can not be null!,(Target)", this, typeof(MauronConsole));
				}
				return C_target;
			}
		}
		public ConsoleApp_window SetTarget (MauronConsole target) {
			C_target=target;
			return this;
		}
		#endregion

		#region Set the window title
		public ConsoleApp_window SetTitle(string title){
			System.Console.Title=title;
			return this;
		}
		#endregion

		#region The WindowSize
		public Vector2d Size {
			get {
				return new Vector2d(System.Console.WindowWidth,System.Console.WindowHeight).SetIsReadOnly(true);
			}
		}
		#endregion
		#region The Window Position
		public Vector2d Position {
			get {
				return new Vector2d(System.Console.WindowLeft,System.Console.WindowTop).SetIsReadOnly(true);
			}
		}
		#endregion

		#region The Buffer area
		public Vector2d BufferArea {
			get {
				return new Vector2d(System.Console.BufferWidth,System.Console.BufferHeight).SetIsReadOnly(true);
			}
		}
		#endregion
	
	}
}