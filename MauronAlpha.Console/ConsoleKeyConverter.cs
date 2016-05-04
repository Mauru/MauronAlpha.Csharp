using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Input.Keyboard.Units;

namespace MauronAlpha.Console {

	public class ConsoleKeyConverter:ConsoleComponent {

		public ConsoleKeyConverter() : base() { }

		public KeyPress ConvertConsoleKey(ConsoleKeyInfo key) {
			//SpecialKeys
			if (key.Key.Equals(ConsoleKey.Enter))
				return new KeyPress(SpecialKeys.Enter,ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.Escape))
				return new KeyPress(SpecialKeys.Escape, ListModifiers(key));

			if (key.Key.Equals(ConsoleKey.Spacebar))
				return new KeyPress(SpecialKeys.Space, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.Tab))
				return new KeyPress(SpecialKeys.Tab, ListModifiers(key));

			if (key.Key.Equals(ConsoleKey.Backspace))
				return new KeyPress(SpecialKeys.Backspace, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.Delete))
				return new KeyPress(SpecialKeys.Delete, ListModifiers(key));

			if (key.Key.Equals(ConsoleKey.PageUp))
				return new KeyPress(SpecialKeys.PageUp, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.PageDown))
				return new KeyPress(SpecialKeys.PageUp, ListModifiers(key));

			if (key.Key.Equals(ConsoleKey.Home))
				return new KeyPress(SpecialKeys.Home, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.End))
				return new KeyPress(SpecialKeys.End, ListModifiers(key));

			if (key.Key.Equals(ConsoleKey.LeftArrow))
				return new KeyPress(SpecialKeys.LeftArrow, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.RightArrow))
				return new KeyPress(SpecialKeys.RightArrow, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.UpArrow))
				return new KeyPress(SpecialKeys.UpArrow, ListModifiers(key));
			if (key.Key.Equals(ConsoleKey.DownArrow))
				return new KeyPress(SpecialKeys.DownArrow, ListModifiers(key));

			//All other cases
			return new KeyPress(Char.ToLower(key.KeyChar), ListModifiers(key));						
		}

		public MauronCode_dataList<KeyModifier> ListModifiers(ConsoleKeyInfo key) {
			MauronCode_dataList<KeyModifier> result = new MauronCode_dataList<KeyModifier>();
			if((key.Modifiers & ConsoleModifiers.Alt) != 0)
				result.Add(KeyModifiers.Alt);
			if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
				result.Add(KeyModifiers.Shift);
			if ((key.Modifiers & ConsoleModifiers.Control) != 0)
				result.Add(KeyModifiers.Ctrl);
			return result;
		}
	
	}
}
