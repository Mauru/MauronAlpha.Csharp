using System;
using MauronAlpha.Interfaces;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;

using MauronAlpha.Input.Keyboard.Units;


namespace MauronAlpha.Input.Keyboard.Collections {

	//A Class that tracks KeyPresses
	public class KeyPressSequence:KeyboardComponent,
	I_protectable<KeyPressSequence>,
	IEquatable<KeyPressSequence> {
	
		//Constructor
		public KeyPressSequence():base() {}

		//String
		public string AsString {
			get {
				string result = "";
				foreach( KeyPress key in DATA_input){
					if( !key.IsModifier )
						result+= key.Key;
								
				}
				return result;
			}
		}

		//The DATA
		private MauronCode_dataTree<EventUnit_timeStamp, KeyPress> DATA_input=new MauronCode_dataTree<EventUnit_timeStamp, KeyPress>();

		public MauronCode_dataList<KeyPress> AsList { 
			get {
				return DATA_input.ValuesAsList;
			}
		}

		//Booleans
		private bool B_isReadOnly = false;
		public bool IsReadOnly { get { return B_isReadOnly; } }
		public bool IsEmpty {
			get { return DATA_input.CountKeys == 0; } 
		}
		public bool Equals(KeyPressSequence other) {
			if(Id.Equals(other.Id))
				return true;
			MauronCode_dataList<KeyPress> values = DATA_input.ValuesAsList;
			return values.Equals(other.AsList);
		}

		//Methods
		public KeyPressSequence SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}
		public KeyPressSequence Add(KeyPress key) {
			if(IsReadOnly)
				throw Error("Is protected!,(Add)",this, ErrorType_protected.Instance);
			EventUnit_timeStamp time = SystemClock.TimeStamp;
			DATA_input.SetValue(time, key, true);
			return this;
		}

		//KeyPresses
		public KeyPress FirstElement { 
			get {
				return DATA_input.ValuesAsList.FirstElement;
			}
		}
	}

}