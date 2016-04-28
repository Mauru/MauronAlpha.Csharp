namespace MauronAlpha.Input.Keyboard.Units {
	
	
	public class KeyCommand:KeyboardComponent{

		public string Name = "unnamed command";
		public KeyPress Key;
		public delegate void DELEGATE_Command();
		public DELEGATE_Command Command;

		public KeyCommand(KeyPress key, DELEGATE_Command command):base() {
			Key = key;
			Command = command;
		}

		public bool Equals(KeyPress key) {
			return Key.Equals(key);
		}
	}


}
