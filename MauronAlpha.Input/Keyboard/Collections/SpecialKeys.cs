using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Input.Keyboard.Collections {
	public class SpecialKeys : KeyboardComponent {

		public static MauronCode_dataList<SpecialKey> DirectionalKeys {
			get {
				return new MauronCode_dataList<SpecialKey>(){
					UpArrow, DownArrow, LeftArrow, UpArrow
				};
			}
		}
		public static MauronCode_dataList<SpecialKey> FormatingKeys {
			get {
				return new MauronCode_dataList<SpecialKey>() {
					Enter, Backspace, Delete, Space, Tab
				};
			}
		}

		public static Key_ENTER Enter {
			get {
				return new Key_ENTER();
			}
		}
		public static Key_ESCAPE Escape {
			get { return new Key_ESCAPE(); }
		}
		public static Key_BACKSPACE Backspace {
			get {
				return new Key_BACKSPACE();
			}
		}
		public static Key_SPACE Space {
			get {
				return new Key_SPACE();
			}
		}
		public static Key_TAB Tab {
			get {
				return new Key_TAB();
			}
		}
		public static Key_LEFTARROW LeftArrow {
			get {
				return new Key_LEFTARROW();
			}
		}
		public static Key_RIGHTARROW RightArrow {
			get {
				return new Key_RIGHTARROW();
			}
		}
		public static Key_UPARROW UpArrow {
			get {
				return new Key_UPARROW();
			}
		}
		public static Key_DOWNARROW DownArrow {
			get {
				return new Key_DOWNARROW();
			}
		}
		public static Key_DELETE Delete {
			get {
				return new Key_DELETE();
			}
		}
		public static Key_NONE None {
			get { return new Key_NONE(); }
		}
		public static Key_HOME Home {
			get { return new Key_HOME(); }
		}
		public static Key_END End {
			get { return new Key_END(); }
		}
		public static Key_PAGEUP PageUp {
			get { return new Key_PAGEUP(); }
		}
		public static Key_PAGEDOWN PageDown {
			get { return new Key_PAGEDOWN(); }
		}

		public static bool IsDirectional(SpecialKey key) {
			return DirectionalKeys.ContainsValue(key);
		}
		public static bool IsFormating(SpecialKey key) {
			return FormatingKeys.ContainsValue(key);
		}
	}
	public class Key_ESCAPE : SpecialKey {
		public override string Name {
			get { return "Escape"; }
		}
	}
	public class Key_ENTER:SpecialKey {
		public override string Name {
			get { return "Enter"; }
		}
	}

	public class Key_BACKSPACE:SpecialKey {
		public override string Name {
			get { return "Backspace"; }
		}
	}
	public class Key_DELETE : SpecialKey {
		public override string Name {
			get { return "Delete"; }
		}
	}

	public class Key_SPACE:SpecialKey {
		public override string Name {
			get { return "Space"; }
		}
	}
	public class Key_TAB:SpecialKey {
		public override string Name {
			get { return "Tab"; }
		}
	}

	public class Key_LEFTARROW:SpecialKey {
		public override string Name {
			get { return "LeftArrow"; }
		}
	}
	public class Key_RIGHTARROW:SpecialKey {
		public override string Name {
			get { return "RightArrow"; }
		}
	}
	public class Key_DOWNARROW:SpecialKey {
		public override string Name {
			get { return "DownArrow"; }
		}
	}
	public class Key_UPARROW:SpecialKey {
		public override string Name {
			get { return "UpArrow"; }
		}
	}

	public class Key_NONE : SpecialKey {
		public override string Name {
			get { return "None"; }
		}
	}

	public class Key_HOME : SpecialKey {
		public override string Name {
			get { return "Home"; }
		}
	}
	public class Key_END : SpecialKey {
		public override string Name {
			get { return "End"; }
		}
	}

	public class Key_PAGEUP : SpecialKey {
		public override string Name {
			get { return "PageUp"; }
		}
	}
	public class Key_PAGEDOWN : SpecialKey {
		public override string Name {
			get { return "PageDown"; }
		}
	}
}
