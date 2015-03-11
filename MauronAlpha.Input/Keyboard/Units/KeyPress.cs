using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Events;


namespace MauronAlpha.Input.Keyboard.Units {

	public class KeyPress:KeyboardComponent,
	IEquatable<KeyPress> {
		
		//constructor
		public KeyPress():base(){}

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
		public bool Equals(KeyPress other) {
			if(Id.Equals(other.Id))
				return true;
			return B_isFunction == other.IsFunction
			&& B_isAltKeyDown == other.IsAltKeyDown
			&& B_isCtrlKeyDown == other.IsCtrlKeyDown
			&& B_isShiftKeyDown == other.IsShiftKeyDown
			&& CHAR_key.Equals(other.Key);
		}
		
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