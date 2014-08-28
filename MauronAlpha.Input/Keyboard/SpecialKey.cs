using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard {
	public class SpecialKey:MauronCode_dataObject {

		//constructor
		public SpecialKey(
			string name,
			Delegate_keyScript action
		):base(DataType_maintaining.Instance){
			SetName(name);
		}

		//The scripted action
		public delegate void Delegate_keyScript(string code);

		//name
		private string STR_name;
		public string Name { get { return STR_name; } }
		public SpecialKey SetName(string name){
			STR_name=name;
			return this;
		}

	}
}
