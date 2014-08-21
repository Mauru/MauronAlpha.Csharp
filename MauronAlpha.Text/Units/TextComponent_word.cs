using MauronAlpha.HandlingData;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Text.Units {

	//A word
	public class TextComponent_word:TextComponent, I_textComponent<TextComponent_word>, IEquatable<TextComponent_word> {
		
		//constructor
		public TextComponent_word(TextComponent_line parent, TextContext context, MauronCode_dataList<TextComponent_character> characters) {}
	
		private MauronCode_dataList<TextComponent_character> Characters;
	}
}