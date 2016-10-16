namespace MauronAlpha.FontParser.Events {
	using MauronAlpha.Events.Units;
	using MauronAlpha.FontParser.DataObjects;

	public class FontLoadEvent : EventUnit_event {

		FontDefinition _font;
		public FontDefinition Font {
			get {
				return _font;
			}
		}

		public FontLoadEvent(FontDefinition font)
			: base("Loaded") {
			_font = font;
		}
	}
}