using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;
using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.ConsoleApp {

	//The cursor position in a Console
	public class CaretPosition:MauronCode_dataObject {
		
		//constructor
		public CaretPosition() : base(DataType_object.Instance) {}

		private Vector2d V_position = new Vector2d();
		public Vector2d AsVector {
			get{
				return V_position;
			}
		}

		public int X {
			get {
				return (int) V_position.X;
			}
		}
		public int Y {
			get {
				return (int) V_position.Y;
			}
		}
		
	}

}
