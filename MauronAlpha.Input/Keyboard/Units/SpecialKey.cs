using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard.Units {


	public class SpecialKey:KeyboardComponent {

		//constructor
		public SpecialKey(string name, KeyScript action):base(){
			SetName(name);
		}

		//The scripted action
		public delegate void KeyScript();

		//name
		private string STR_name;
		public string Name { get { return STR_name; } }
		public SpecialKey SetName(string name){
			STR_name=name;
			return this;
		}

	}
}
