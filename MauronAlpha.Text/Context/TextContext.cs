using System;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Context {
	
	public class TextContext:MauronCode_textComponent,
	IEquatable<TextContext>,
	I_protectable<TextContext>,
	I_instantiable<TextContext> {

		//Constructors
		public TextContext():base() {}
		public TextContext(string textId, int paragraph, int line, int word, int character) {
			ID_txt = textId;
			INT_paragraph = paragraph;
			INT_line = line;
			INT_word = word;
			INT_character = character;
		}
		public TextContext (int paragraph, int line, int word, int character) {
			INT_paragraph=paragraph;
			INT_line=line;
			INT_word=word;
			INT_character=character;
		}

		//Booleans
		public bool Equals(TextContext other) {
			return ID_txt == other.TextId
			&& INT_paragraph == other.Paragraph
			&& INT_line == other.Line
			&& INT_word == other.Word
			&& INT_character == other.Character;
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		//Methods
		public TextContext Instance { get {
			return new TextContext(ID_txt, INT_paragraph, INT_line, INT_word, INT_character);
		} }
		public TextContext SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}
		public TextContext SetContext(string id, int paragraph, int line, int word, int character) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetContext)", this, ErrorType_protected.Instance);
			ID_txt = id;
			INT_paragraph = paragraph;
			INT_line = line;
			INT_word = word;
			INT_character = character;
			return this;		
		}
		public TextContext SetTextId(string n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetTextId)", this, ErrorType_protected.Instance);
			ID_txt = n;
			return this;
		}
		public TextContext SetParagraph(int n) {
			if(IsReadOnly)
				throw Error("Is protected!,(SetParagaraph)",this, ErrorType_protected.Instance);
			INT_paragraph = n;
			return this;
		}
		public TextContext SetLine (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetLine)", this, ErrorType_protected.Instance);
			INT_line = n;
			return this;
		}
		public TextContext SetWord(int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetWord)", this, ErrorType_protected.Instance);
			INT_word=n;
			return this;
		}
		public TextContext SetCharacter (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetCharacter)", this, ErrorType_protected.Instance);
			INT_character = n;
			return this;
		}

		public TextContext Add (int paragraph, int line, int word, int character) {
			if( IsReadOnly )
				throw Error("Is protected!,(Add)", this, ErrorType_protected.Instance);
			INT_paragraph+=paragraph;
			INT_line+=line;
			INT_word+=word;
			INT_character+=character;
			return this;
		}
		public TextContext Add (TextContext context) {
			if( IsReadOnly )
				throw Error("Is protected!,(Add)", this, ErrorType_protected.Instance);
			INT_paragraph+=context.Paragraph;
			INT_line+=context.Line;
			INT_word+=context.Word;
			INT_character+=context.Character;
			return this;
		}
		public TextContext Subtract (int paragraph, int line, int word, int character) {
			if( IsReadOnly )
				throw Error("Is protected!,(Subtract)", this, ErrorType_protected.Instance);
			INT_paragraph-=paragraph;
			INT_line-=line;
			INT_word-=word;
			INT_character-=character;
			return this;
		}
		public TextContext Subtract (TextContext context) {
			if( IsReadOnly )
				throw Error("Is protected!,(Subtract)", this, ErrorType_protected.Instance);
			INT_paragraph-=context.Paragraph;
			INT_line-=context.Line;
			INT_word-=context.Word;
			INT_character-=context.Character;
			return this;
		}

		//Index (text)
		private string ID_txt="";
		public string TextId {
			get {
				return ID_txt;
			}	
		}
		public string AsString {
			get {
				return "{"+INT_paragraph+":"+INT_line+":"+INT_word+":"+INT_character+"}";
			}
		}

		//Index int
		private int INT_paragraph = 0;
		public int Paragraph  { get {
			return INT_paragraph;
		} }
		private int INT_line = 0;
		public int Line { get {
			return INT_line;
		} }
		private int INT_word = 0;
		public int Word { get {
			return INT_word;
		} }
		private int INT_character = 0;
		public int Character { get {
			return INT_character;
		}}

	}

}
