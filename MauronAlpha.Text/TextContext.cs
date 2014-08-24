﻿using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

using System;

namespace MauronAlpha.Text {

	public class TextContext:MauronCode_dataObject, IEquatable<TextContext> {

		//constructor
		public TextContext():base(DataType_maintaining.Instance) {}
		public TextContext(int line, int word, int character):this(){
			SetLineOffset(line);
			SetWordOffset(word);
			SetCharacterOffset(character);
		}
		public TextContext (int line, int word):this() {
			SetLineOffset(line);
			SetWordOffset(word);
		}
		public TextContext (int line):this() {
			SetLineOffset(line);
		}

		private TextComponent_text TXT_source;
		public TextComponent_text Source {
			get {
				if(TXT_source==null){
					SetSource(new TextComponent_text());
				}
				return TXT_source;
			}
		}
		public TextContext SetSource(TextComponent_text source){
			TXT_source=source;
			return this;
		}
		public bool HasSource {
			get {
				return TXT_source==null;
			}
		}

		public TextContext Instance {
			get {
				return TextContext.New.SetSource(Source).SetLineOffset(LineOffset).SetWordOffset(WordOffset).SetCharacterOffset(CharacterOffset);
			}
		}
		public static TextContext New {
			get {
				return new TextContext();
			}
		}

		#region line
		private int INT_lineOffset;
		public int LineOffset {
			get { return INT_lineOffset; }
		}
		public TextContext SetLineOffset(int n) {
			INT_lineOffset=n;
			return this;
		}
		#endregion

		#region word
		private int INT_wordOffset;
		public int WordOffset {
			get { return INT_wordOffset; }
		}
		public TextContext SetWordOffset(int n){
			INT_wordOffset=n;
			return this;
		}
		#endregion

		#region character
		private int INT_characterOffset;
		public int CharacterOffset {
			get { return INT_characterOffset; }
		}
		public TextContext SetCharacterOffset(int n) {
			INT_characterOffset=n;
			return this;
		}
		#endregion

		public static TextContext End {
			get {
				return new TextContext(-1,-1,-1);
			}
		}
		public static TextContext Start {
			get {
				return new TextContext(0, 0, 0);
			}
		}

		public TextContext Add(TextContext context){
			SetLineOffset(LineOffset+context.LineOffset);
			SetWordOffset(WordOffset+context.WordOffset);
			SetCharacterOffset(CharacterOffset+context.CharacterOffset);
			return this;
		}

		#region IEquatable<TextContext>
		bool IEquatable<TextContext>.Equals (TextContext other) {
			if(
				HasSource&&!other.HasSource
				||	!HasSource&&other.HasSource
			){
				return false;
			}
			else if(HasSource&&other.HasSource){
				if(Source!=other.Source){
					return false;
				}
			}
			return 
			LineOffset==other.LineOffset
			&& WordOffset==other.WordOffset
			&& CharacterOffset==other.CharacterOffset;
		}
		#endregion

	}

}