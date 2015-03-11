using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard.Collections {
	
	//Saves a number of sequences
	public class KeyPressSequenceMap:MauronCode_dataList<KeyPressSequence> {
		
		public bool IsSequence(KeyPressSequence sequence){
			return base.ContainsValue(sequence);
		}

	}

}
