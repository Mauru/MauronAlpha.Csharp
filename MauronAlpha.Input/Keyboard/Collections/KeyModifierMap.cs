using MauronAlpha.HandlingData;
using MauronAlpha.Input.Keyboard.Units;

namespace MauronAlpha.Input.Keyboard.Collections {
	public class KeyModifierMap:MauronCode_dataMap<KeyModifier> {

		public KeyModifierMap()
			: base() {

				SetValue("Shift", KeyModifiers.Shift);
				SetValue("Ctrl", KeyModifiers.Ctrl);
				SetValue("Alt", KeyModifiers.Alt);

		}
	}
}
