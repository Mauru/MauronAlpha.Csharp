using System;

namespace MauronAlpha.Text {

	public interface I_textDisplay {
		I_textDisplay WriteLine (TextComponent text);
		I_textDisplay Write (TextComponent text);
		TextBuffer TextBuffer { get; }
	}

}