using MauronAlpha.HandlingData;
using MauronAlpha.ErrorHandling;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent, I_textComponent<TextComponent_line> {

		//constructor
		public TextComponent_line (TextComponent_text parent, TextContext context) {}

	}
}