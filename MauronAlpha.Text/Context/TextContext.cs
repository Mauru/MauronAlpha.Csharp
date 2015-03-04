using System;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;

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

		public TextContext InheritValues( I_textUnitType targetType, I_textUnit origin) {
			if( IsReadOnly )
				throw Error("Is protected!,(InheritValues)", this, ErrorType_protected.Instance);

			if( targetType.Equals(TextUnitType_text.Instance) )
				return this;

			else if( targetType.Equals(TextUnitType_paragraph.Instance) ) {
				ID_txt = origin.Context.TextId;
			}

			else if( targetType.Equals(TextUnitType_line.Instance) ) {
				ID_txt = origin.Context.TextId;
				INT_paragraph = origin.Context.Paragraph;
			}

			else if( targetType.Equals(TextUnitType_word.Instance) ) {
				ID_txt=origin.Context.TextId;
				INT_paragraph=origin.Context.Paragraph;
				INT_line=origin.Context.Line;
			}

			else if( targetType.Equals(TextUnitType_character.Instance) ) {
				ID_txt=origin.Context.TextId;
				INT_paragraph=origin.Context.Paragraph;
				INT_line=origin.Context.Line;
				INT_word=origin.Context.Word;
			}

			return this;
				

			
		}
		public TextContext SetValue( I_textUnitType unitType, int value ) {
			if(IsReadOnly)
				throw Error("Is protected!,(SetValue)",this,ErrorType_protected.Instance);
			if(unitType.Equals(TextUnitType_text.Instance))
				return this;

			if( unitType.Equals(TextUnitType_paragraph.Instance) )
				INT_paragraph = value;
			else if( unitType.Equals(TextUnitType_line.Instance) )
				INT_line=value;
			else if( unitType.Equals(TextUnitType_word.Instance) )
				INT_word=value;
			else
				INT_character=value;

			return this;
		}
		public TextContext AddValue( I_textUnitType unitType, int value ) {
			if(IsReadOnly)
				throw Error("Is protected!,(AddValue)",this,ErrorType_protected.Instance);
			SetValue( unitType, Value(unitType)+value);
			return this;
		}

		public MauronCode_dataList<int> AsList {
			get {
				return new MauronCode_dataList<int>(){INT_paragraph, INT_line, INT_word, INT_character};
			}
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
		public int Value(I_textUnitType unitType) {
			if(unitType.Equals(TextUnitType_paragraph.Instance))
				return INT_paragraph;
			if(unitType.Equals(TextUnitType_line.Instance))
				return INT_line;
			if(unitType.Equals(TextUnitType_word.Instance))
				return INT_word;
			if(unitType.Equals(TextUnitType_character.Instance))
				return INT_character;
			return 0;
		}
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
