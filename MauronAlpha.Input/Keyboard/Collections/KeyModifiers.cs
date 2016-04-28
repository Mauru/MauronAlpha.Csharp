using MauronAlpha.Input.Keyboard.Units;
namespace MauronAlpha.Input.Keyboard.Collections {
	public class KeyModifiers:KeyboardComponent {
		public static MODIFIER_Alt Alt {
			get { return new MODIFIER_Alt(); }
		}
		public static MODIFIER_Ctrl Ctrl {
			get { return new MODIFIER_Ctrl(); }
		}
		public static MODIFIER_Shift Shift {
			get { return new MODIFIER_Shift(); }
		}
	}

	public class MODIFIER_Shift : KeyModifier {
		public override string Name { get { return "Shift"; } }
	}
	public class MODIFIER_Alt : KeyModifier {
		public override string Name { get { return "Alt"; } }
	}
	public class MODIFIER_Ctrl : KeyModifier {
		public override string Name { get { return "Ctrl"; } }
	}
}
