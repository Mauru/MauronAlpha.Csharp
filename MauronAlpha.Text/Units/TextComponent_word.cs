using MauronAlpha.HandlingData;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Text.Units {

	//A word
	public class TextComponent_word:TextComponent, I_textComponent<TextComponent_word>, IEquatable<TextComponent_word> {
		
		//constructor
		public TextComponent_word() {}
		public TextComponent_word(MauronCode_dataList<TextComponent_character> characters){
			foreach(TextComponent_character c in characters){
				AddCharacter(c);
			}
		}
		public TextComponent_word(string str){
			ICollection<char> characters = str.ToCharArray();
			foreach(char c in characters){
				AddCharacter(new TextComponent_character(c));
			}
		}

		//Instance
		public TextComponent_word Instance {
			get {
				TextComponent_word newWord =new TextComponent_word();
				foreach(TextComponent_character c in Characters){
					newWord.AddCharacter(c.Instance);
				}
				foreach(TextComponent_word w in WordParts){
					newWord.AddWordPart(w.Instance);
				}
				return newWord;
			}
		}
		public static TextComponent_word New {
			get { 
				return new TextComponent_word();
			}

		}
		public TextComponent_character NewChild {
			get { 
				TextComponent_character c = new TextComponent_character (this);
				return c;
			}

		}

		#region Context
		private TextContext DATA_context;
		public TextContext Context {
			get {
				if(DATA_context==null){
					SetContext(new TextContext());
				}
				return DATA_context;
			}
		}
		public TextComponent_word SetContext(TextContext context){
			DATA_context=context;
			return this;
		}
		public bool HasContext {
			get { return DATA_context==null; }
		}
		#endregion

		#region Text, forming the text property

		//Get the line text
		private string STR_text;
		public string Text {
			get {
				return STR_text;
			}
		}
		private TextComponent_word SetText (string txt) {
			STR_text=txt;
			return this;
		}
		private TextComponent_word ConstructText ( ) {
			string txt="";
			foreach( TextComponent_character c in Characters ) {
				txt+=c.Text;
			}
			STR_text=txt;
			return this;
		}

		#endregion

		#region Characters

		//Characters
		private MauronCode_dataList<TextComponent_character> DATA_characters;
		private MauronCode_dataList<TextComponent_character> Characters {
			get {
				if(DATA_characters==null){
					DATA_characters=new MauronCode_dataList<TextComponent_character>();
				}
				return DATA_characters;
			}
		}
		public TextComponent_word SetCharacters(MauronCode_dataList<TextComponent_character> chars){
			DATA_characters=chars;
			SetIsEmpty(Characters.Count<1);
			DATA_wordBreaks=null;
			return this;
		}
		
		public TextComponent_word AddCharacter(TextComponent_character c){
			Characters.AddValue(c);
			c.SetContext (new TextContext (Context.LineOffset,Context.WordOffset,CharacterCount));
			c.SetParent (this);
			SetIsEmpty(false);
			InitializeWordBreaks();
			return this;
		}

		public TextComponent_word RemoveCharacterByIndex(int n){
			if( n>0||n>=CharacterCount ) {
				Error("Character Index out of range {"+n+"}",this);
			}
			Characters.RemoveByKey(n);
			if( CharacterCount==0 ) {
				SetIsEmpty(true);
			}
			InitializeWordBreaks();
			return this;
		}

		public TextComponent_character LastCharacter {
			get {
				if(Characters.Count<1){
					AddCharacter(NewChild);
				}
				return Characters.LastElement;
			}
		}
		public TextComponent_character FirstCharacter {
			get {
				if( Characters.Count<1 ) {
					AddCharacter(NewChild);
				}
				return Characters.FirstElement;
			}
		}
		public TextComponent_character CharacterByIndex(int n){
			if( n<0||n>=CharacterCount ) {
				Error("Index out of bounds {"+n+"}",this);
			}
			return Characters.Value(n);
		}
		public int CharacterCount { get {
			return Characters.Count;
		} }

		#endregion

		//is the word "complete" (i.e. is the last character a whitespace or linebreak)
		public bool IsComplete { get {
			return ((!IsEmpty)&&(!LastCharacter.EndsWord));
		} }

		#region breaking a word into parts (experimental)
		
		//can the word be broken up into seperate word?
		public bool IsBreakable {
			get {
				if(Characters.Count<2){
					return false;
				}
				return	WordBreaks.Count>0;
			}
		}
		public bool IsBroken {
			get {
				return WordParts.Count>0;
			}
		}

		//Get all the word breaks
		private MauronCode_dataIndex<TextComponent_character> DATA_wordBreaks;
		public MauronCode_dataIndex<TextComponent_character> WordBreaks {
			get {
				//Initialize index
				if(DATA_wordBreaks==null){
					InitializeWordBreaks();
				}
				return DATA_wordBreaks;
			}
		}
		private TextComponent_word InitializeWordBreaks() {
			MauronCode_dataIndex<TextComponent_character> wordbreaks=new MauronCode_dataIndex<TextComponent_character>();
			for(int n=0; n<Characters.Count; n++){
				TextComponent_character c = Characters.Value(n);
				if(c.IsWordBreak){
					wordbreaks.SetValue(n,c);
				}
			}
			return SetWordBreaks(wordbreaks);
		}
		private TextComponent_word SetWordBreaks(MauronCode_dataIndex<TextComponent_character> wordbreaks){
			DATA_wordBreaks=wordbreaks;
			return this;
		}
		
		//Get any word-parts (i.e. post-word-break results)
		private MauronCode_dataList<TextComponent_word> DATA_wordParts;
		public MauronCode_dataList<TextComponent_word> WordParts {
			get {
				if(DATA_wordParts==null){
					SetWordParts(new MauronCode_dataList<TextComponent_word>());
				}
				return DATA_wordParts;
			}
		}
		private TextComponent_word SetWordParts(MauronCode_dataList<TextComponent_word> parts){
			DATA_wordParts=parts;
			return this;
		}
		private TextComponent_word AddWordPart(TextComponent_word part){
			WordParts.AddValue(part);
			return this;
		}

		//Break a word
		public TextComponent_word BreakAt(int n){
			if(n==0||n>=Characters.Count){
				return this;
			}
			TextComponent_word newWord = new TextComponent_word(Characters.Range(n, Characters.Count-1));
			AddWordPart(newWord);
			int newCount=CharacterCount-newWord.CharacterCount;
			while( CharacterCount>newCount ) { 
				RemoveCharacterByIndex(n);
			}
			return this;
		}

		#endregion

		//is the word a Line-break?
		public bool EndsLine {
			get {	return LastCharacter.EndsLine; }
		}

		//is the character empty?
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get { return B_isEmpty; }
		}
		private TextComponent_word SetIsEmpty (bool status) {
			B_isEmpty=status;
			return this;
		}
	}
}