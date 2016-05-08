using MauronAlpha.HandlingData;
using MauronAlpha.Input.Keyboard.Units;

namespace MauronAlpha.Input.Keyboard.Collections {
	
	public class KeyCommands:MauronCode_dataList<KeyCommand> {

		public void Register(KeyPress key, KeyCommand.DELEGATE_Command command) {
			KeyCommand com = new KeyCommand(key, command);
			base.Add(com);
		}
		public bool Try(KeyPress key, ref KeyCommand command) {
			foreach (KeyCommand com in Data)
				if (com.Equals(key)) {
					command = com;
					return true;
				}
			return false;
		}

	}
}
