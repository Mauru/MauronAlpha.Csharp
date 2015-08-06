using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.Input.Keyboard.Units {

	public class KeyPress:KeyboardComponent,
	IEquatable<KeyPress> {
		
		//constructor
		public KeyPress():base(){}
		public KeyPress(char key) : this() {
			SetChar(key);
			SetKeyName("" + key);
		}
		public KeyPress(string keyName) : this() {
			SetKeyName(keyName);
		}

		//string description
		public string AsString {
			get {
				string result = "";
				if (IsCtrlKeyDown)
					result += "[CTRL]";
				if (IsAltKeyDown)
					result += "[ALT]";
				if (IsShiftKeyDown)
					result += "[SHIFT]";
				if (IsFunction||KeyName!=null)
					result += "["+KeyName+"]";
				result += CHAR_key;
				return result;
			}
		}

		//Character Code
		private char CHAR_key = '\u0000';
		public char Char {
			get {
				return CHAR_key;
			}
		}
		public KeyPress SetChar(char key) {
			CHAR_key = key;
			return this;
		}
		private string STR_key;
		public string KeyName {
			get {
				return STR_key;
			}
		}
		public KeyPress SetKeyName(string str) {
			STR_key = str;
			return this;
		}

		//Booleans
		public bool Equals(KeyPress other) {
			if (B_isAltKeyDown != other.IsAltKeyDown
			|| B_isCtrlKeyDown != other.IsCtrlKeyDown
			|| B_isShiftKeyDown != other.IsShiftKeyDown)
				return false;
			if (IsFunction && other.IsFunction)
				return KeyName == other.KeyName;
			return Char == other.Char;
		}
		
		//Boolean : Special Key
		public bool IsFunction {
			get {
				return (CHAR_key == '\u0000');
			}
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