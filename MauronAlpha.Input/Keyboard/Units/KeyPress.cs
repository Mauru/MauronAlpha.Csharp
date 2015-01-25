using MauronAlpha.HandlingData;
using MauronAlpha.Events;


namespace MauronAlpha.Input.Keyboard.Units {

	public class KeyPress:MauronCode_dataObject {
		
		//constructor
		public KeyPress():base(DataType_object.Instance){}

		//Character Code
		private char CHAR_key;
		public char Key {
			get {
				return CHAR_key;
			}
		}
		public KeyPress SetKey(char key){
			CHAR_key = key;
			return this;
		}


		//Booleans
		
		//Boolean : Special Key
		private bool B_isFunction = false;
		public bool IsFunction {
			get {
				return B_isFunction;
			}
		}
		public KeyPress SetIsFunction(bool status) {
			B_isFunction = status;
			return this;
		}		

		//Boolean Modifiers
		private bool B_isAltKeyDown = false;
		public bool IsAltKeyDown { get { return B_isAltKeyDown; } }
		public KeyPress SetIsAltKeyDown (bool status) {
			B_isAltKeyDown = status;
			return this;
		}

		private bool B_isShiftKeyDown = false;
		public bool IsShiftKeyDown { get { return B_isShiftKeyDown; } }
		public KeyPress SetIsShiftKeyDown (bool status) {
			B_isShiftKeyDown = status;
			return this;
		}

		private bool B_isCtrlKeyDown = false;
		public bool IsCtrlKeyDown { get { return B_isCtrlKeyDown; } }
		public KeyPress SetIsCtrlKeyDown (bool status) {
			B_isCtrlKeyDown = status;
			return this;
		}

		public bool IsModifier { 
			get {
				return IsCtrlKeyDown || IsAltKeyDown || IsShiftKeyDown;
			}
		}

	}

}