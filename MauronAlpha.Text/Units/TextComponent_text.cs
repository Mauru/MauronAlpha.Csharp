using System.Collections.Generic;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Utility;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {

	//A complete text
	public class TextComponent_text:TextComponent {

		private MauronCode_dataList<TextComponent_line> Lines=new MauronCode_dataList<TextComponent_line>();

		#region Adding to a text
		public TextComponent_text AddLine(TextComponent_line line){
			Lines.AddValue (line);
			return this;
		}
		public TextComponent_text AddLineAtContext(TextContext context){
			TextComponent_line line = new TextComponent_line (this, context);
			Lines.InsertValueAt (context.LineOffset, line);

			//offset the context
			MauronCode_dataList<TextComponent_line> lines=Lines.Range(context.LineOffset+1);
			foreach(TextComponent_line l in lines){
				l.OffsetContext(new TextContext(1));
			}
			return this;
		}
		public TextComponent_text AddString (string str) {

			//create char list
			MauronCode_dataList<char> characters=new MauronCode_dataList<char>(str.ToCharArray());

			//definbe the line and word in the text
			TextComponent_line line;
			TextComponent_word word;
			if( LineCount<1 ) {
				line=new TextComponent_line(this, new TextContext(1));
				AddLine(line);
			}
			line=LastLine;

			if( line.WordCount<1 ) {
				word=new TextComponent_word(line, new TextContext(1, 1));
				line.AddWord(word);
			}
			word=line.LastWord;

			for( int i=0; i<characters.Count; i++ ) {
				line=LastLine;
				char c=characters.Value(i);
				word=line.LastWord;

				#region last word ended line, start new

				if( word.EndsLine ) {

					line=new TextComponent_line(
						this,
						new TextContext(
							LineCount,
							0,
							0
						)
					);
					AddLine(line);

					word=new TextComponent_word(
						line,
						new TextContext(
							LineCount-1,
							line.WordCount,
							0
						)
					);
					line.AddWord(word);
				}
				#endregion

				#region last word is complete start new

				if( word.IsComplete ) {
					word=new TextComponent_word(
						line,
						new TextContext(
							LineCount-1,
							line.WordCount,
							0
						)
					);
					line.AddWord(word);
				}

				#endregion

				TextComponent_character ch=new TextComponent_character(
												 word,
												 new TextContext(
													LineCount-1,
													line.WordCount-1,
													word.CharacterCount
												 ),
												c
											 );
				word.AddCharacter(ch);
			}

			return this;
		}
		#endregion

		#region Getting portions of a text

		#region Lines
		public TextComponent_line FirstLine{
			get {
				if( LineCount<1 ) {
					Error("Invalid Line {0}", this, ErrorType_index.Instance);
				}
				return Lines.FirstElement;
			}
		}
		public TextComponent_line LineByContext(TextContext context){
			if (!Lines.ContainsKey (context.LineOffset)) {
				Error("Invalid Line {"+context.LineOffset+"}", this, ErrorType_index.Instance);
			}
			return Lines.Value (context.LineOffset);
		}
		public TextComponent_line LastLine {
			get { 
				if (Lines.Count < 1) {
					Error ("Lines is empty #f{LastLine}", this, ErrorType_index.Instance);
				}
				return Lines.LastElement;
			}
		}
		#endregion

		#region Words

		public TextComponent_word FirstWord {
			get {
				return FirstLine.FirstWord;
			}
		}
		public TextComponent_word WordByContext(TextContext context){
			TextComponent_line line = LineByContext(context);
			return line.WordByContext(context);
		}
		public TextComponent_word LastWord {
			get {
				return LastLine.LastWord;
			}
		}

		#endregion

		#region Character

		public TextComponent_character FirstCharacter {
			get {
				return FirstLine.FirstWord.FirstCharacter;
			}
		}
		public TextComponent_character CharacterByContext (TextContext context) {
			TextComponent_line line=LineByContext(context);
			TextComponent_word word=line.WordByContext(context);
			return word.CharacterByContext(context);
		}
		public TextComponent_character LastCharacter {
			get {
				return LastLine.LastWord.LastCharacter;
			}
		}

		#endregion

		#endregion

		#region Counting the content
		public int LineCount {
			get {
				return Lines.Count;
			}
		}
		public int WordCount {
			get {
				int result=0;
				foreach(TextComponent_line line in Lines){
					result+=line.WordCount;
				}
				return result;
			}
		}
		public int CharacterCount {
			get {
				int result=0;
				foreach( TextComponent_line line in Lines ) {
					result+=line.CharacterCount;
				}
				return result;
			}
		}
		#endregion

	}

}