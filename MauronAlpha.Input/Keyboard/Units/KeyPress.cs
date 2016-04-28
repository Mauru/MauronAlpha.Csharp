using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Events;

using MauronAlpha.Input.Keyboard.Collections;

namespace MauronAlpha.Input.Keyboard.Units {

	public class KeyPress:KeyboardComponent,
	IEquatable<KeyPress> {

		public static KeyPress None {
			get {
				return new KeyPress();
			}
		}

		//constructor
		public KeyPress():base(){}
		public KeyPress(char key) : this() {
			CHAR_key = key;
		}
		public KeyPress(char key, MauronCode_dataList<KeyModifier> modifiers) : this(key) {
			DATA_modifiers = modifiers;
		}
		public KeyPress(SpecialKey key)	: this() {
			KEY_function = key;
		}
		public KeyPress(SpecialKey key, MauronCode_dataList<KeyModifier> modifiers)	: this(key) {
			DATA_modifiers = modifiers;
		}

		private SpecialKey KEY_function = SpecialKeys.None;
		public SpecialKey Function { get { return KEY_function; } }

		private MauronCode_dataList<KeyModifier> DATA_modifiers = new MauronCode_dataList<KeyModifier>();
		public MauronCode_dataList<KeyModifier> Modifiers { get { return DATA_modifiers; } }

		//Copy
		public KeyPress Copy {
			get {
				if (IsFunction)
					return new KeyPress(Function, Modifiers);
				return new KeyPress(Char, Modifiers);
			}
		}

		//string description
		public string AsString {
			get {
				string result = "";
				foreach (KeyModifier modifier in Modifiers)
					result += "[" + modifier.Name + "]";
				if (IsFunction)
					return result += "[" + Function.Name + "]";
				return result += "[" + CHAR_key + "]";
			}
		}

		//Character Code
		private char CHAR_key = '\u0000';
		public char Char {
			get {
				return CHAR_key;
			}
		}

		//Booleans
		public bool Equals(KeyPress other) {
			if (!Modifiers.Equals_unsorted(other.Modifiers))
				return false;
			if (IsFunction != other.IsFunction)
				return false;
			else if (IsFunction)
				return Function.Equals(other.Function);
			return Char == other.Char;
		}
		
		//Boolean : Special Key
		public bool IsFunction {
			get {
				return !Function.Equals(SpecialKeys.None);
			}
		}
		public bool IsEnter {
			get {
				return KEY_function.Equals(SpecialKeys.Enter);
			}
		}
		public bool IsSpace {
			get {
				return KEY_function.Equals(SpecialKeys.Space);
			}
		}

		//Boolean Modifiers
		public bool IsAltKeyDown { get { return Modifiers.ContainsValue(KeyModifiers.Alt); } }
		public bool IsShiftKeyDown { get { return Modifiers.ContainsValue(KeyModifiers.Shift); } }
		public bool IsCtrlKeyDown { get { return Modifiers.ContainsValue(KeyModifiers.Ctrl); } }

		public bool IsModifier { 
			get {
				return Modifiers.Count > 0;
			}
		}
		public bool IsEmpty {
			get {
				return !IsFunction && Char == '\u0000' && !IsModifier;
			}
		}
	}

}