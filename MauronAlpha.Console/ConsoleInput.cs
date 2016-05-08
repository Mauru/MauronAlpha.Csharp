using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using System;
using MauronAlpha.Events.Collections;

using MauronAlpha.Input.Keyboard.Events;

namespace MauronAlpha.Console {

	public class ConsoleInput : ConsoleComponent, I_sender<Event_keyUp> {
		Subscriptions<Event_keyUp> Subscriptions = new Subscriptions<Event_keyUp>(); 
		ConsoleKeyConverter Converter = new ConsoleKeyConverter();

		KeyPress DATA_lastKey = KeyPress.None;

		public void Listen() {
			ConsoleKeyInfo input = System.Console.ReadKey(true);
			DATA_lastKey = Converter.ConvertConsoleKey(input);

			Subscriptions.ReceiveEvent(new Event_keyUp(this, DATA_lastKey));
		}

		public KeyPress LastKey {
			get {
				return DATA_lastKey;
			}
		}

		public Character KeyToCharacter(KeyPress key) {
			if (key.IsFunction)
				return ParseSpecialKey(key);
			if (key.Modifiers.ContainsValue(KeyModifiers.Shift))
				return new Character(Char.ToUpper(key.Char));
			return new Character(key.Char);
		}
		static Character ParseSpecialKey(KeyPress key) {
			if (key.Function.Equals(SpecialKeys.Enter)) {
				if (key.Modifiers.ContainsValue(KeyModifiers.Shift))
					return Characters.ParagraphBreak;
				return Characters.LineBreak;
			}
			if (key.Function.Equals(SpecialKeys.Space))
				return Characters.WhiteSpace;
			if (key.Function.Equals(SpecialKeys.Tab))
				return Characters.Tab;
			return Characters.Empty;
		}

		public void Subscribe(I_subscriber<Event_keyUp> s) {
			Subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<Event_keyUp> s) {
			Subscriptions.Remove(s);
		}
	}
}
