using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard {

	public class KeyPress:MauronCode_dataObject {
		
		//constructor
		public KeyPress():base(DataType_object.Instance){}

		//Alt key
		private bool B_isAltKeyDown=false;
		public bool IsAltKeyDown { get {return B_isAltKeyDown; } }
		public KeyPress SetIsAltKeyDown(bool status){
			B_isAltKeyDown=status;
			return this;
		}

		//Shift key
		private bool B_isShiftKeyDown=false;
		public bool IsShiftKeyDown { get { return B_isShiftKeyDown; } }
		public KeyPress SetIsShiftKeyDown(bool status){
			B_isShiftKeyDown=status;
			return this;
		}

		//CTRL key
		private bool B_isCtrlKeyDown=false;
		public bool IsCtrlKeyDown { get { return B_isCtrlKeyDown; } }
		public KeyPress SetIsCtrlKeyDown (bool status) {
			B_isCtrlKeyDown=status;
			return this;
		}

		//Character
		private char CHAR_key;
		public char Key {
			get {
				return CHAR_key;
			}
		}
		public KeyPress SetKey(char key){
			CHAR_key=key;
			return this;
		}

	}

}