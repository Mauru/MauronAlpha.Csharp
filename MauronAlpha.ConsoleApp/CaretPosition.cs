using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

namespace MauronAlpha.ConsoleApp {

	//The cursor position in a Console
	public class CaretPosition:MauronCode_dataObject {
		
		//constructor
		public CaretPosition(TextUnit_text text)
			: base(DataType_object.Instance) {
		}
		
	}

}
