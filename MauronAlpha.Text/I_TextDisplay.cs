using System;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text {

	public interface I_textDisplay {
		I_textDisplay WriteLine (TextComponent text);
		I_textDisplay Write (TextComponent text);
		TextComponent_text TextBuffer { get; }
	}

}