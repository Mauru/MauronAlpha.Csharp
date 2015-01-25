using MauronAlpha.ConsoleApp.Interfaces;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;

namespace MauronAlpha.ConsoleApp {
	
	//basically a dataArray for data contained in a console
	public class ConsoleApp_data : ConsoleApp_component,
	I_consoleData {

		public ConsoleApp_data():base() {}

		private KeyPressSequence DATA_activeSequence = new KeyPressSequence();
		public KeyPressSequence ActiveSequence  { 
			get {
				return DATA_activeSequence;	
			}
		}
		public I_consoleData AppendToSequence( KeyPress key ){
			DATA_activeSequence.Add(key);
			return this;
		}



	}
}
