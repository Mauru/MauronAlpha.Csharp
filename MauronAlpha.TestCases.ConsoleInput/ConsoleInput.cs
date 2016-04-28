using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using System;

namespace MauronAlpha.TestCases.Console {

	public class ConsoleInput:CodeComponent, I_eventSender {
		ConsoleKeyConverter Converter = new ConsoleKeyConverter();

		public MauronAlpha.Events.EventHandler Events { get { return HANDLER_events; } }
		MauronAlpha.Events.EventHandler HANDLER_events = new MauronAlpha.Events.EventHandler();
		KeyPress DATA_lastKey = KeyPress.None;

		public void Listen() {
			ConsoleKeyInfo input = System.Console.ReadKey(true);
			DATA_lastKey = Converter.ConvertConsoleKey(input);

			SendEvent(Events.GenerateEvent("input", this));
		}

		public I_eventSender SendEvent(Events.Units.EventUnit_event e) {
			Events.SubmitEvent(e, this);
			return this;
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
	}
}
