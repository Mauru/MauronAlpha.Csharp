using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.DataObjects {

	//The cursor position in a Console
	public class CaretPosition:MauronCode_dataObject {
		
		//constructor
		private CaretPosition() : base(DataType_object.Instance) {}
		public CaretPosition(I_caretSource source) : this() {
			CARET_source = source.Content;
		}
		public CaretPosition(TextUnit_text source) : this() {
			CARET_source = source;
		}

		private TextUnit_text CARET_source;

		private TextContext DATA_context;
		public TextContext Context { 
			get{
				if(DATA_context == null)
					DATA_context = new TextContext();
				return DATA_context;
			}
		}

		public string AsString { get { return Context.AsString; } }

		public Vector2d AsVector {
			get{
				if (CARET_source == null)
					return new Vector2d();
				return Convert_TextContext_Vector2d(Context, CARET_source);
			}
		}
		private Vector2d Convert_TextContext_Vector2d(TextContext context, TextUnit_text text) {
			if (text.IsEmpty)
				return new Vector2d();
			TextUnit_paragraph paragraph;
			int y = 0;
			for (int index = 0; index < context.Paragraph; index++) {
				paragraph = text.ParagraphByIndex(index);
				y += paragraph.ChildCount;
			}
			paragraph = text.ParagraphByIndex(context.Paragraph);
			TextUnit_line line = paragraph.ChildByIndex(context.Line);

			int x = 0;
			TextUnit_word word;
			for (int index = 0; index < context.Word; index++) {
				word = line.WordByIndex(index);
				x += word.ChildCount;
			}
			return new Vector2d(x + context.Character, y + context.Line);

		}

		//INT:Position (proxies Context)
		public int Paragraph {
			get {
				return Context.Paragraph;
			}
		}
		public int Line {
			get {
				return Context.Line;
			}
		}
		public int Word { 
			get { 
				return Context.Word;
			} 
		}
		public int Character { 
			get { 
				return Context.Character;
			}
		}

		//Methods
		public CaretPosition Add(TextContext context) {
			if (CARET_source == null) {
				Context.Add(context);
				return this;
			}
			TextContext result = Context.Instance.Add(context);
			//Paragraph
			TextUnit_paragraph paragraph;
			if (!CARET_source.HasChildAtIndex(result.Paragraph))
				paragraph = (result.Paragraph<0)? CARET_source.FirstChild:CARET_source.LastChild;
			else
				paragraph = CARET_source.ChildByIndex(result.Paragraph);
			//Line
			TextUnit_line line;
			if (!paragraph.HasChildAtIndex(result.Line))
				line = (result.Line < 0) ? paragraph.FirstChild : paragraph.LastChild;
			else
				line = paragraph.ChildByIndex(result.Line);
			//Word
			TextUnit_word word;
			if (!line.HasChildAtIndex(result.Line))
				word = (result.Word < 0) ? line.FirstChild : line.LastChild;
			else
				word = line.ChildByIndex(result.Word);
			//Character
			TextUnit_character character;
			if (!word.HasChildAtIndex(result.Character))
				character = (result.Character < 0) ? word.FirstChild : word.LastChild;
			else
				character = word.ChildByIndex(result.Character);
			DATA_context = character.Context;
			return this;
		}
		public CaretPosition Add(int paragraph, int line, int word, int character) {
			return Add(new TextContext(paragraph, line, word, character));
		}
	}

}
