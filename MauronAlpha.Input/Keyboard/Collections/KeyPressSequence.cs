using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.Input.Keyboard.Collections {

	//A Class that tracks KeyPresses
	public class KeyPressSequence:MauronCode_dataList<KeyPress> {
	
		//Constructor
		public KeyPressSequence():base() {}

		public string AsString {
			get {
				string result = "";
				foreach( KeyPress key in this){
					if( !key.IsModifier )
						result+= key.Key;
								
				}
				return result;
			}
		}

	}

}