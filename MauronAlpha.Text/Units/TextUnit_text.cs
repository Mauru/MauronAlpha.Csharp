using System;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;

using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//Base class of a text
	public class TextUnit_text:TextComponent_unit,
	I_textUnit {
		
		//constructor
		public TextUnit_text():base(TextUnitType_text.Instance) {
			
		}
		public TextUnit_text(string text):this() {
			Encoding.StringToUnit(text, this);
		}

		public TextUnit_text Copy {
			get {
				TextUnit_text copy = new TextUnit_text();
				foreach (TextUnit_paragraph unit in Children)
					copy.InsertChildAtIndex(copy.ChildCount, unit, false);
				copy.ReIndex(0, true, false);
				return copy;
			}
		}

		//Context
		public override TextContext Context {
			get {
				if(DATA_context==null)
					DATA_context = new TextContext(Id,0,0,0,0);
				return DATA_context.SetIsReadOnly(true);
			}
		}
		public override TextContext CountAsContext {
			get {
				if(IsEmpty)
					return new TextContext();
				TextContext result = new TextContext(ChildCount,0,0,0);
				foreach(I_textUnit unit in DATA_children)
					result.Add(unit.CountAsContext);
				return result.SetIsReadOnly(true);
			}
		}

		//int Index
		public override int Index { 
			get { 
				return 0; 
			} 
		}

		//Booleans
		public bool HasLineAtIndex(int n) {
			if (ChildCount == 0)
				return false;
			int index = 0;
			foreach (TextUnit_paragraph paragraph in Children) {
				index += paragraph.ChildCount;
				if (index > n)
					return true;
			}
			return false;
		}

		//Indexing
		public new TextUnit_paragraph ChildByIndex( int index ) {
			if( index < 0 || index >= ChildCount )
				throw Error( "Index out of bounds!,{"+index+"},(ChildByIndex)", this, ErrorType_index.Instance );

			return ( TextUnit_paragraph ) DATA_children.Value( index );
		}
		public TextUnit_paragraph ParagraphByIndex (int n) {
			return ChildByIndex(n);
		}
		public TextUnit_paragraph FirstChild { get {
			if (ChildCount>0)
				return ChildByIndex(0);
			else if (IsReadOnly)
				throw Error("Is protected!,(FirstChild)", this, ErrorType_protected.Instance);
			TextUnit_paragraph result = new TextUnit_paragraph();
			InsertChildAtIndex(0,result,true);
			return ChildByIndex(0);
		} }
		public TextUnit_paragraph LastChild {
			get {
				if (ChildCount == 0)
					return FirstChild;
				return ChildByIndex(ChildCount - 1);
			}
		}
		
		public TextUnit_line LineByIndex(int index) {
			int offset = 0;
			TextContext count = CountAsContext;

			if( index < 0 || index >= count.Line)
				throw Error( "Index out of Bounds!,{"+index+"},(LineByIndex)", this, ErrorType_index.Instance );

			foreach(TextUnit_paragraph unit in DATA_children) {
				TextContext unit_count = unit.CountAsContext;
				if( offset + unit_count.Line > index )
					return unit.ChildByIndex( index - offset );

				offset += unit_count.Line;
			}

			throw Error( "No unit found!,{"+index+"},(LineByIndex)", this, ErrorType_index.Instance );
		}
		public TextUnit_line NewLine {
			get {
				if (IsReadOnly)
					throw Error("Is protected!,(NewLine)", this, ErrorType_protected.Instance);
				return LastChild.NewLine;
			}
		}
		public TextUnit_line AsNewLine(string text) {
			return NewLine.SetText(text);			
		}

		public TextUnit_word WordByIndex( int index ) {
			int offset = 0;
			TextContext count = CountAsContext;
			if(index < 0 || index >= count.Word)
				throw Error( "Index out of Bounds!,{"+index+"},(WordByIndex)", this, ErrorType_index.Instance );

			foreach(TextUnit_paragraph unit in DATA_children) {
				TextContext unit_count = unit.CountAsContext;
				if( offset + unit_count.Word > index )
					return unit.WordByIndex( index - offset );

				offset += unit_count.Word;
			}

			throw Error( "Index out of Bounds!,{"+index+"},(WordByIndex)", this, ErrorType_index.Instance );	

		}
		public TextUnit_character CharacterByIndex( int index ) {
			TextContext count = CountAsContext;
			if( index < 0 || index > count.Character )
				throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );

			int offset = 0;

			foreach( TextUnit_paragraph unit in DATA_children ) {
				TextContext unit_count = unit.CountAsContext;

				if( index < offset + unit_count.Character )
					return unit.CharacterByIndex( index - offset );

				offset += unit_count.Character;
			}

			throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );
		}

		public MauronCode_dataList<TextUnit_word> Words { get {
			MauronCode_dataList<TextUnit_word> result = new MauronCode_dataList<TextUnit_word>();
			foreach (TextUnit_paragraph paragraph in Children)
				result.AddValuesFrom(paragraph.Words);
			return result;
		} }
		public MauronCode_dataList<TextUnit_line> Lines { get {
			MauronCode_dataList<TextUnit_line> result = new MauronCode_dataList<TextUnit_line>();
			foreach (TextUnit_paragraph paragraph in Children)
				result.AddValuesFrom(paragraph.Lines);
			return result;
		} }

		//Special methods
		public TextUnit_text InsertUnitAtContext(TextContext context, I_textUnit unit, bool reIndex) {
			if (IsReadOnly)
				throw Error("Is protected!,(InsertUnitAtContext)", this, ErrorType_protected.Instance);
			//start with the paragraph
			if (context.Paragraph > ChildCount)
				throw Error("Paragraph out of bounds!,{" + context.Paragraph + "},(InsertUnitAtContext)", this, ErrorType_bounds.Instance);
			//new Paragraph
			if (context.Paragraph == ChildCount)
				InsertChildAtIndex(ChildCount, new TextUnit_paragraph(), false);
			TextUnit_paragraph paragraph = ChildByIndex(context.Paragraph);

			//line
			if (context.Line > paragraph.ChildCount)
				throw Error("Line out of bounds!,{" + context.Line + "},(InsertUnitAtContext)", this, ErrorType_bounds.Instance);
			//new line
			if (context.Line == paragraph.ChildCount)
				paragraph.InsertChildAtIndex(paragraph.ChildCount, new TextUnit_line(), false);
			TextUnit_line line = paragraph.ChildByIndex(context.Line);

			//word
			if (context.Word > line.ChildCount)
				throw Error("Word out of bounds!,{" + context.Word + "},(InsertUnitAtContext)", this, ErrorType_bounds.Instance);
			//new word
			if (context.Word == line.ChildCount)
				line.InsertChildAtIndex(context.Word, new TextUnit_word(), false);
			TextUnit_word word = line.ChildByIndex(context.Word);

			//character
			if (context.Character > word.ChildCount)
				throw Error("Character out of bounds!,{" + context.Character + "},(InsertUnitAtContext)", this, ErrorType_bounds.Instance);

			//Insert all characters into word
			MauronCode_dataList<I_textUnit> characters = unit.Characters;
			foreach (I_textUnit character in characters)
				word.InsertChildAtIndex(word.ChildCount, character, false);

			if (reIndex)
				word.ReIndex(context.Character, true,true);

			return this;
		}
		public TextUnit_text SetText(string text) {
			if (IsReadOnly)
				throw Error("Is protected!,(SetText)", this, ErrorType_protected.Instance);
			Encoding.StringToUnit(text,this);
			return this;
		}
		public TextUnit_text RemoveCharacterAtContext(TextContext context) {
			if (IsReadOnly)
				throw Error("Is protected!", this, ErrorType_protected.Instance);
			TextUnit_paragraph paragraph = ParagraphByIndex(context.Paragraph);
			TextUnit_line line = paragraph.LineByIndex(context.Line);
			TextUnit_word word = line.WordByIndex(context.Word);
			word.RemoveChildAtIndex(context.Character, true);
			return this;
		}
		public TextUnit_text AppendText(string text, bool newLine) {
			if (IsReadOnly)
				throw Error("Is protected!,(AppendText)", this, ErrorType_protected.Instance);
			if (newLine)
				AsNewLine(text);
			else
				LastChild.LastChild.AppendText(text, false);
			return this;				
		}
		public TextUnit_text PrependText(string text, bool newLine) {
			if (IsReadOnly)
				throw Error("Is protected!,(PrependText)", this, ErrorType_protected.Instance);
			TextUnit_text txt = new TextUnit_text(text);
			if (newLine)
				foreach (TextUnit_line line in txt.Lines)
					FirstChild.InsertChildAtIndex(0, line, false);
			else 
				FirstChild.FirstChild.PrependText(text, false);
			ReIndex(0, true, true);	
			return this;	
		}
	}

	//Description of a text unit 
	public class TextUnitType_text:TextUnitType,
	I_textUnitType {
		
		public override string Name { 
			get { 
				return "text"; 
			} 
		}

		public override bool CanHaveParent {
			get { 
				return false; 
			}
		}
	
		public static TextUnitType_text Instance { 
			get {
				return new TextUnitType_text();
			} 
		}

		public override I_textUnit New {
			get {
				return new TextUnit_paragraph();
			}
		}

		public override I_textUnitType ParentType {
			get {
				return TextUnitType_text.Instance;
			}
		}
		public override I_textUnitType ChildType {
			get {
				return TextUnitType_paragraph.Instance;
			}
		}

	}
}
