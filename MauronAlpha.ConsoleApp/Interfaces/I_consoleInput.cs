using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;

namespace MauronAlpha.ConsoleApp.Interfaces {
	
	//Holds any input data for the console	
	public interface I_consoleInput {

		KeyPressSequence ActiveSequence { get; }

		I_consoleInput AppendToSequence (KeyPress key);
	
	}

}
