using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard {
	public class SpecialKey:MauronCode_dataObject {

		//constructor
		public SpecialKey(
			string name,
			bool useShift,
			bool useCtrl,
			bool useAlt,
			bool IsLineBreak,
			bool IsSeperator,
			bool IsScripted
		):base(DataType_maintaining.Instance){
			SetName(name);
		}

		//name
		private string STR_name;
		public string Name { get { return STR_name; } }
		public SpecialKey SetName(string name){
			STR_name=name;
			return this;
		}

	}
}
