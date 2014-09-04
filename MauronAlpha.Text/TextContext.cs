using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

using System;

namespace MauronAlpha.Text {

	public class TextContext:MauronCode_dataObject, IEquatable<TextContext> {

		//region constructors
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

		#region cloning, instantiating
		public TextContext Instance {
			get {
				return TextContext.New.SetLineOffset(LineOffset).SetWordOffset(WordOffset).SetCharacterOffset(CharacterOffset);
			}
		}
		public static TextContext New {
			get {
				return new TextContext();
			}
		}
		#endregion

		#region Context information
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
		#endregion

		#region Static context
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
		#endregion

		#region Manipulate the context
		public TextContext Add(TextContext context){
			return Add(context.LineOffset,context.WordOffset,context.CharacterOffset);
		}
		public TextContext Add(int line, int word, int character){
			SetLineOffset(LineOffset+line);
			SetWordOffset(WordOffset+word);
			SetCharacterOffset(CharacterOffset+character);
			return this;
		}
		#endregion

		#region IEquatable<TextContext>
		bool IEquatable<TextContext>.Equals (TextContext other) {
			return 
			LineOffset==other.LineOffset
			&& WordOffset==other.WordOffset
			&& CharacterOffset==other.CharacterOffset;
		}
		#endregion

		public string AsString {
			get {
				return "{'"+LineOffset+"','"+WordOffset+"'+'"+CharacterOffset+"'}";
			}
		}
	
		public bool IsStart{
			get {
				return this.Equals(Start);
			}
		}
		public bool IsEndOf(TextComponent_text text){
			TextContext textContext = AbsoluteContextToText(text,this);
			// TextContext context;
		}

		/// <summary>
		/// Evalutes a context relative to a TextComponent, turning it to absolute numbers
		/// </summary>
		public static TextContext AbsoluteContextToText(TextComponent_text text,TextContext context){
			
			
			//the line offset
			if(text.IsEmpty){
				return new TextContext(0,0,0);
			}

			TextContext modified=new TextContext(0,0,0);
			int newline=0;
			int newChar=0;
			int newWord=0;

			//1: characteroffset
			if(context.CharacterOffset<0){
				newChar=text.LastCharacter.Context.CharacterOffset+context.LineOffset;
				if(newChar<0){
				}
				
			}

			if(context.LineOffset<0){
				int newLine=text.LineCount+context.LineOffset;
				if(newLine<0){
					newLine=0;
				}				
			}

		}
	}

}