using MauronAlpha.HandlingData;

using MauronAlpha.Input.Keyboard.Units;

namespace MauronAlpha.Input.Keyboard.Collections {

	//A list of Special Keys
	public class SpecialKeyMap:MauronCode_dataMap<SpecialKey> {

		public SpecialKeyMap() {
			SetValue("Enter", SpecialKeys.Enter);
			SetValue("Space", SpecialKeys.Space);
			SetValue("Tab", SpecialKeys.Tab);
			SetValue("Escape", SpecialKeys.Escape);

			SetValue("Delete", SpecialKeys.Delete);
			SetValue("Backspace", SpecialKeys.Backspace);

			SetValue("LeftArrow", SpecialKeys.LeftArrow);
			SetValue("RightArrow", SpecialKeys.RightArrow);
			SetValue("DownArrow", SpecialKeys.DownArrow);
			SetValue("UpArrow", SpecialKeys.UpArrow);

			SetValue("End", SpecialKeys.End);
			SetValue("Home", SpecialKeys.Home);

			SetValue("PageUp", SpecialKeys.PageUp);
			SetValue("PageDown", SpecialKeys.PageDown);
		}
	}

}
