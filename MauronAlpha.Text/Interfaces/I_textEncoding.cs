﻿using System;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Interfaces {
	
	//Helps TextUnits interpret different character encodings
	public interface I_textEncoding:IEquatable<I_textEncoding> {
		
		string Name { get; }

		I_textEncoding SetTextToString(TextUnit_text unit, string text);

		char EmptyCharacter { get; }
		char WhiteSpace { get; }
		char Tab { get; }
		char Paragraph { get; }
		char NewLine { get; }
		char ZeroWidth { get; }

		bool IsEmptyCharacter(TextUnit_character unit);
		bool IsWhiteSpace(TextUnit_character unit);
		bool IsTab(TextUnit_character unit);
		bool IsParagraph(TextUnit_character unit);
		bool IsNewLine (TextUnit_character unit);
		bool IsZeroWidth(TextUnit_character unit);

		bool EndsLine( TextUnit_character unit );
		bool EndsLine( TextUnit_word unit );

	}


}
