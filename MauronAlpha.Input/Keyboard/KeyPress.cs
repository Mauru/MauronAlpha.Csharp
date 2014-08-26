using MauronAlpha.HandlingData;
using MauronAlpha.Events;


namespace MauronAlpha.Input.Keyboard {

	public class KeyPress:MauronCode_dataObject {
		
		//constructor
		public KeyPress():base(DataType_object.Instance){}

		#region Alt key
		private bool B_isAltKeyDown=false;
		public bool IsAltKeyDown { get {return B_isAltKeyDown; } }
		public KeyPress SetIsAltKeyDown(bool status){
			B_isAltKeyDown=status;
			return this;
		}
		#endregion
		#region Shift key
		private bool B_isShiftKeyDown=false;
		public bool IsShiftKeyDown { get { return B_isShiftKeyDown; } }
		public KeyPress SetIsShiftKeyDown(bool status){
			B_isShiftKeyDown=status;
			return this;
		}
		#endregion
		#region CTRL key
		private bool B_isCtrlKeyDown=false;
		public bool IsCtrlKeyDown { get { return B_isCtrlKeyDown; } }
		public KeyPress SetIsCtrlKeyDown (bool status) {
			B_isCtrlKeyDown=status;
			return this;
		}
		#endregion

		#region The character code
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
		#endregion

		#region Special Keys
		//Special Key
		private SpecialKey KEY_specialKey;
		public SpecialKey SpecialKey { get { return KEY_specialKey; } }
		public KeyPress SetSpecialKey(SpecialKey key) { KEY_specialKey=key; return this; }

		//Figure out if a Keypress is a special Key
		public bool CheckIsSpecialKey(KeyboardMap map, KeyPress input){
			foreach (SpecialKey key in map) {
				if(key.Equals(input)){
					input.SetSpecialKey(key);
					return true;
				}
			}
			return false;
		}
		#endregion				
	
	}

}