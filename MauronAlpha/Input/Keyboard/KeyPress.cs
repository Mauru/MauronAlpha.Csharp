using MauronAlpha.HandlingData;
using MauronAlpha.Events;
using MauronAlpha.Events.Data;

namespace MauronAlpha.Input.Keyboard {

	public class KeyPress:MauronCode_dataObject, I_eventSender, I_eventReceiver {
		
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

		//Special Keys
		private bool B_IsSpecialKey=false;
		public bool IsSpecialKey { get { return B_IsSpecialKey; } }
		public KeyPress SetIsSpecialKey(bool status) {
			B_IsSpecialKey=status;
			return this;
		}

		//Special Key
		private SpecialKey KEY_specialKey;
		public SpecialKey SpecialKey { get { return KEY_specialKey; } }
		public KeyPress SetSpecialKey(SpecialKey key) { KEY_specialKey=key; return this; }

		//Figure out if a Keypress is a special Key
		public static bool Check_SpecialAction(KeyboardMap map, KeyPress input){
			foreach (SpecialKey key in map) {
				if(key.Equals(input)){
					input.SetSpecialKey(key);
					SendEvent(EventClock, "SpecialKey", EventData.New.SetValue("SpecialKey", input));
					return true;
				}
			}
			return false;
		}
	
		#region I_eventSender
		//The event clock
		private MauronCode_eventClock CLOCK_events;
		public MauronCode_eventClock EventClock {
			get { 
				if(CLOCK_events==null) {
					Error("Event Clock can not be null", this);
				}
				return CLOCK_events; }
		}
		public KeyPress SetEventClock(MauronCode_eventClock clock){
			CLOCK_events=clock;
			return this;
		}
		public I_eventSender SendEvent (MauronCode_eventClock clock, string code, EventData data) {
			clock.SubmitEvent(new MauronCode_event(clock,this,code,data));
		}
		#endregion

		#region I_eventReceiver
		public I_eventReceiver SubscribeToEvents ( ) {
			return this;
		}
		public I_eventReceiver ReceiveEvent (MauronCode_event e) {
			e.Execute(this);
		}
		public static bool IsEventCondition (MauronCode_event e) {
			return false;
		}
		#endregion
	


}

}