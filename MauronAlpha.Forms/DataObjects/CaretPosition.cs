using System;
using MauronAlpha.HandlingData;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.DataObjects;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.DataObjects {

	//The cursor position in a Console
	public class CaretPosition:MauronCode_dataObject {
		
		//constructor
		private CaretPosition() : base(DataType_object.Instance) {}
		public CaretPosition(I_caretSource source) : this() {
			DATA_text = source.AsText;
		}
		public CaretPosition(Text source) : this() {
			DATA_text = source;
		}

		private Text DATA_text;

		private TextContext DATA_context;
		public TextContext Context { 
			get{
				if(DATA_context == null)
					DATA_context = new TextContext( 0, 0, 0, 0);
				return DATA_context;
			}
		}

		public string AsString { get { return Context.AsString; } }

		public Vector2d AsVector {
			get{
				if (DATA_text == null)
					return new Vector2d();
				return Convert_TextContext_Vector2d(Context, DATA_text);
			}
		}
		private Vector2d Convert_TextContext_Vector2d(TextContext context, Text text) {
			if (text.IsEmpty)
				return new Vector2d();
			Vector2d result = new Vector2d();
			Line line = text.LineByContext(context);
			Paragraph paragraph = line.Parent;
			result.SetY(line.Index);
			result.SetX(line.VisualLength);

			if (paragraph.Index > 0) {
				paragraph.TryPrevious(ref paragraph);
				result.Add(0, paragraph.Count);
				while (paragraph.TryPrevious(ref paragraph))
					result.Add(0, paragraph.Count);
			}

			return result;
		}

		//INT:Position (proxies Context)
		public int Paragraph {
			get {
				return Context.Paragraph;
			}
			set {
				Context.SetParagraph(value);
			}
		}
		public int Line {
			get {
				return Context.Line;
			}
			set {
				Context.SetLine(value);
			}
		}
		public int Word { 
			get { 
				return Context.Word;
			}
			set {
				Context.SetWord(value);
			}
		}
		public int Character { 
			get { 
				return Context.Character;
			}
			set {
				Context.SetCharacter(value);
			}
		}

		//Methods
		public CaretPosition Add(int p, int l, int w, int c) {
			Paragraph += p;
			Line += l;
			Word += w;
			Character += c;
			return this;
		}
		public CaretPosition Set(int p, int l, int w, int c) {
			Paragraph = p;
			Line = l;
			Word = w;
			Character = c;
			return this;
		}
		public CaretPosition SetCharacter(int n) {
			Character = n;
			return this;
		}
		public CaretPosition SetWord(int n) {
			Word = n;
			return this;
		}
		public CaretPosition SetLine(int n) {
			Line = n;
			return this;
		}
		public CaretPosition SetParagraph(int n) {
			Paragraph = n;
			return this;
		}
		
		//Methods
		public void Reset() {
			DATA_context = new TextContext();
		}
	
		public bool SetContext(TextContext context) {
			DATA_context = context;
			return true;
		}
		public bool MoveToEdit() {
			if (DATA_text == null)
				return true;
			Word last = DATA_text.LastWord;
			if (last.IsEmpty)
				return SetContext(last.Context);
			if (last.IsParagraphBreak)
				return SetContext(last.Paragraph.Next.Context);
			if (last.IsLineBreak) {
				Line nextL = last.Parent.Next;
				if (nextL.TryNext(ref nextL) && nextL.IsParagraphBreak)
					return SetContext(nextL.Parent.Next.Context);
				return SetContext(nextL.Context);
			}
			if (last.IsUtility)
				return SetContext(last.Next.Context);
			return SetContext(last.LastChild.Context.Copy.Add(0,0,0,1));
		}
		public bool MoveOneCharacter() {
			if(DATA_text == null)
				return false;
			Word w = DATA_text.WordByContext(Context);
			if (w.IsEmpty)
				return false;
			if (w.IsParagraphBreak) {
				Set(DATA_context.Paragraph + 1, 0, 0, 0);
				return true;
			}
			if (w.IsLineBreak) {
				Set(DATA_context.Paragraph, DATA_context.Line + 1, 0, 0);
				return true;
			}
			if (w.IsUtility) {
				Set(DATA_context.Paragraph, DATA_context.Line, DATA_context.Word + 1, 1);
				return true;
			}
			Add(0, 0, 0, 1);
			return true;
		}

	}

}
