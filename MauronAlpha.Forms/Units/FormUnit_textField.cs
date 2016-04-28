using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;

using MauronAlpha.Forms.DataObjects;
using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.Units {
	
	//A Entity waiting for user input
	public class FormUnit_textField : FormComponent_unit, I_layoutUnit {
		
		//constructor
		public FormUnit_textField():base( FormType_textField.Instance ) {
			DATA_caret = new CaretPosition(DATA_text);
		}

		CaretPosition DATA_caret;
		public CaretPosition CaretPosition {
			get {
				if (DATA_caret == null)
					DATA_caret = new CaretPosition(DATA_text);
				return DATA_caret;
			}
		}

		public int CountLines {
			get {
				return DATA_text.Lines.Count;
			}
		}

		Text DATA_text = new Text();
		public Text AsText { get { return DATA_text; } }
		
		public string AsString {
			get {
				return DATA_text.AsString;
			}
		}

		public TextContext CountAsContext {
			get {
				int p = 0;
				int l = 0;
				int w = 0;
				int c = 0;
				foreach (Paragraph pu in DATA_text.Paragraphs) {
					p++;
					foreach (Line lu in pu.Lines) {
						l++;
						foreach (Word wu in lu.Words) {
							w++;
							foreach (Character cu in wu.Characters)
								c++;
						}
					}
				}
				return new TextContext(p, l, w, c);
			}
		}

		//Querying
		public Paragraph ActiveParagraph {
			get {
				return DATA_text.ByIndex(CaretPosition.Paragraph);
			}
		}

		public Line LineByIndex (int index){
			int count = CountLines;
			if (index < 0 || index > count)
				throw Error("Index out of bounds!,{" + index + "},(LineByIndex)", this, ErrorType_bounds.Instance);
			else if (index == count)
				return DATA_text.FirstLine;
			return DATA_text.LineByIndex(index);
		}
		public Line ActiveLine {
			get {
				return ActiveParagraph.ByIndex(CaretPosition.Line);
			}
		}
		public Line LastLine {
			get {
				return DATA_text.LastLine;
			}
		}
		public Line FirstLine {
			get {
				return DATA_text.FirstLine;
			}
		}

		public Word ActiveWord {
			get {
				return ActiveLine.ByIndex(CaretPosition.Word);
			}
		}

		public Character ActiveCharacter {
			get {
				return ActiveWord.ByIndex(CaretPosition.Character);
			}
		}

		public MauronCode_dataList<Line> Lines {
			get {
				return DATA_text.Lines;
			}
		}

		//Booleans
		public bool IsEmpty {
			get {
				return DATA_text.IsEmpty;
			}
		}

		public bool HasLine(int n) {
			return DATA_text.HasLineAtIndex(n);
		}
		public bool AddAsLines(string text) {
			Lines ll = new Lines(text);
			if (!IsEmpty)
				LastLine.TryEnd();
			Paragraph p = DATA_text.LastChild;
			foreach (Line l in ll) {
				if (p.IsEmpty)
					p.TryAdd(l);
				else if (p.HasParagraphBreak) {
					p = p.Next;
					p.TryAdd(l);
				}
				else
					p.TryAdd(l);
			}
			DATA_caret.SetContext(DATA_text.Edit);
			return true;
		}
		public bool Insert(TextContext context, Character unit) {
			if (unit.IsParagraphBreak)
				return InsertParagraphBreak(context, unit);
			if (unit.IsLineBreak)
				return InsertLineBreak(context, unit);
			if (unit.IsWhiteSpace)
				return InsertWhiteSpace(context, unit);
			if (unit.IsTab)
				return InsertTab(context, unit);
			return InsertRegularCharacter(context,unit);
		}

		public bool RemoveNextCharacter() {
			if (DATA_text.IsEmpty)
				return false;
			ContextQuery query = new ContextQuery(DATA_text, CaretPosition.Context);
			Character c = query.ForceCharacter();
			Character target = null;
			if (!c.TryStepRight(ref target))
				return false;
			return RemoveCharacterAtContext(target.Context);
		}
		public bool RemovePreviousCharacter() {
			if (DATA_text.IsEmpty)
				return false;
			ContextQuery query = new ContextQuery(DATA_text, CaretPosition.Context);
			Character c = query.ForceCharacter();
			Character target = null;
			if (!c.TryStepLeft(ref target))
				return false;
			return RemoveCharacterAtContext(target.Context);
		}
		public bool RemoveCharacterAtContext(TextContext context) {
			if (DATA_text.IsEmpty)
				return false;
			Word w = null;
			if(!DATA_text.TryWordByContext(CaretPosition.Context,ref w))
				return false;
			if (w.Count <= 1)
				return RemoveWordAtContext(w.Context);
			w.Remove(context.Character);
			return true;
		}
		public bool RemoveWordAtContext(TextContext context) {
			Word w = null;
			if(!DATA_text.TryWordByContext(context, ref w))
				return false;
			if (w.Parent.Count <= 1)
				return RemoveLineAtContext(context);
			Line l = w.Parent;
			if (w.IsLineBreak) {
				Line nextL = null;
				if (l.TryNext(ref nextL)) {
					//we are removing a paragraph break
					if (nextL.IsParagraphBreak) {
						l.Parent.Remove(nextL.Index);
						l.Text.TryInlineMergeAtIndex(l.Parent.Index);
						return true;
					}
				}
				l.Remove(w.Index);
				l.Parent.TryInlineMergeAtIndex(l.Index);
				return true;
			}
			if (w.IsUtility) {
				Word nextW = null;
				Word prevW = null;
				if(!w.TryPrevious(ref prevW)) {
					l.Remove(w.Index);
					return true;
				}
				if (prevW.IsUtility) {
					l.Remove(w.Index);
					return true;
				}
				if (w.TryNext(ref nextW)) {
					if (nextW.IsUtility) {
						l.Remove(w.Index);
						return true;
					}
					l.Remove(w.Index);
					l.TryInlineMergeAtIndex(prevW.Index);
					return true;
				}
				l.Remove(w.Index);
				return true;
			}
			l.Remove(w.Index);
			return true;
		}
		public bool RemoveLineAtContext(TextContext context) {
			if (DATA_text.IsEmpty)
				return false;
			Line l = null;
			if (!DATA_text.TryLineByContext(CaretPosition.Context, ref l))
				return false;
			Paragraph p = l.Parent;
			if (p.Count <= 1) {
				p.Parent.Remove(p.Index);
				return true;
			}
			if (l.IsParagraphBreak) {
				p.Remove(l.Index);
				p.Parent.TryInlineMergeAtIndex(p.Index);
				return true;
			}
			Line nextL = null;
			if (!l.TryNext(ref nextL)) {
				p.Remove(l.Index);
				return true;
			}
			if (nextL.IsParagraphBreak) {
				p.Remove(nextL.Index);
				p.Parent.TryInlineMergeAtIndex(p.Index);
				return true;
			}
			p.Remove(l.Index);
			return true;
		}

		public bool InsertLineBreak() {
			InsertLineBreak(CaretPosition.Context, Characters.LineBreak);
			return true;
		}
		public bool InsertParagraphBreak() {
			return InsertParagraphBreak(CaretPosition.Context, Characters.ParagraphBreak);
		}
		public bool InsertTab() {
			return InsertTab(CaretPosition.Context, Characters.Tab);
		}
		public bool InsertWhiteSpace() {
			return InsertWhiteSpace(CaretPosition.Context, Characters.WhiteSpace);
		}
		public bool InsertRegularCharacter(Character unit) {
			if (unit.IsUtility)
				return false;
			return Insert(CaretPosition.Context, unit);
		}

		private bool InsertParagraphBreak(TextContext context, Character unit) {
			if (!unit.IsParagraphBreak)
				return false;

			//Solve the current context
			ContextQuery query = ContextQuery.SolveToEdit(DATA_text, context);
			context = query.Context;

			Paragraph paragraph = query.ForceParagraph(true);
			context = query.Context;

			Line line = query.ForceLine(true);
			context = query.Context;

			Paragraph newP;

			//Line is ParagraphBreak
			if (line.IsParagraphBreak) {
				//left
				if (context.Character <= 0) {
					paragraph.Remove(line.Index);
					paragraph.TryAdd(new Line(unit));
					newP = new Paragraph(line);
					newP.FixParagraphEnd();
					DATA_text.TryAdd(newP, paragraph.Index + 1);
				}
				//right
				else { 
					newP = new Paragraph(new Line(unit));
					newP.FixParagraphEnd();
					DATA_text.TryAdd(newP, paragraph.Index + 1);
				}
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//Line is empty
			Line prevL = null;
			if (line.IsEmpty) {
				//Fix ParagraphEnd
				if (line.TryPrevious(ref prevL)) {
					if (!prevL.HasLineBreak)
						prevL.TryAdd(Words.LineBreak);
				}
				else {
					line.TryAdd(Words.LineBreak);
					paragraph.TryAdd(new Line(unit));
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				line.TryAdd(new Word(unit));
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//Find word
			Word word = query.ForceWord(true);
			Lines splitL;
			Line nextL = null;
			Paragraph nextP = null;

			//word is a linebreak
			if (word.IsLineBreak) {
				//left
				if (context.Character <= 0) {
					line.Remove(word.Index);
					line.TryAdd(Words.LineBreak);
					splitL = paragraph.SplitAt(line.Index + 1);
					splitL.InsertValueAt(0, new Line(word));
					paragraph.TryAdd(new Line(unit));

					if (splitL.HasParagraphBreak) {
						newP = new Paragraph(splitL);
						DATA_text.TryAdd(newP, paragraph.Index + 1);
					}
					else if (!paragraph.TryNext(ref nextP)) {
						newP = new Paragraph(splitL);
						DATA_text.TryAdd(newP);
					}else
						nextP.Insert(splitL, 0);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				//right
				if (!line.TryNext(ref nextL)) { 
					paragraph.TryAdd(new Line(unit));
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				//middle
				splitL = paragraph.SplitAt(line.Index);
				paragraph.TryAdd(new Line(unit));
				if (splitL.HasParagraphBreak) {
					newP = new Paragraph(splitL);
					newP.FixParagraphEnd();
					DATA_text.TryAdd(newP, paragraph.Index + 1);
				}
				else if (!paragraph.TryNext(ref nextP))
					DATA_text.TryAdd(new Paragraph(splitL), paragraph.Index + 1);
				else { 
					nextP.Insert(splitL, 0);
					nextP.TryInlineMergeAtIndex(splitL.Count - 1);
				}
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//word is empty
			if (word.IsEmpty) {
				if (!line.TryPrevious(ref prevL)) {
					word.TryAdd(Characters.LineBreak);
					paragraph.TryAdd(new Line(unit));
				}
				//this should not happen
				else if (!prevL.HasLineBreak)
					prevL.TryAdd(Words.LineBreak);
				else
					word.TryAdd(unit);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}
			Words splitW;
			Word prevW = null;

			//left
			if (context.Character <= 0) {
				//start of line
				if (!word.TryPrevious(ref prevW)) {
					//no previous line, prepend empty p
					if (!line.TryPrevious(ref prevL)) {
						newP = new Paragraph(new Line(unit));
						newP.FixParagraphEnd();
						DATA_text.TryAdd(newP, paragraph.Index + 1);
						DATA_caret.SetContext(unit.Edit);
						RequestRender();
						return true;
					}
					//split at current line
					splitL = paragraph.SplitAt(line.Index);
					paragraph.TryAdd(new Line(unit));

					//self-contained
					if (splitL.HasParagraphBreak) {
						newP = new Paragraph(splitL);
						newP.FixParagraphEnd();
						DATA_text.TryAdd(newP, paragraph.Index + 1);
					}
					else if (!paragraph.TryNext(ref nextP))
						nextP = paragraph.Next;
					else { 
						nextP.Insert(splitL, 0);
						nextP.TryInlineMergeAtIndex(splitL.Count - 1);
					}
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}

				splitW = line.SplitAt(word.Index);
				splitL = paragraph.SplitAt(line.Index + 1);

				if (!prevW.IsLineBreak)
					line.TryAdd(Words.LineBreak);
				paragraph.TryAdd(new Line(unit));

				//still need to deal with splitL (can be empty) and splitW 
				//merge splitW into splitL
				if (splitW.HasLineBreak)
					splitL.InsertValueAt(0, new Line(splitW));
				else if (splitL.IsEmpty)
					splitL.Add(new Line(splitW));
				else {
					nextL = splitL.FirstElement;
					nextL.Insert(splitW, 0);
					nextL.TryInlineMergeAtIndex(splitW.Count - 1);
				}
				if (!paragraph.TryNext(ref nextP))
					nextP = paragraph.Next;
				nextP.Insert(splitL, 0);
				nextP.TryInlineMergeAtIndex(splitL.Count - 1);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}
			//right
			if (context.Character >= word.Count) {
				splitW = line.SplitAt(word.Index + 1);
				splitL = paragraph.SplitAt(line.Index + 1);
				line.TryAdd(Words.LineBreak);
				paragraph.TryAdd(new Line(unit));
				//No split stuff
				if (splitL.IsEmpty && splitW.IsEmpty) {
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				//merge splitW into splitL
				if (splitW.HasLineBreak)
					splitL.InsertValueAt(0, new Line(splitW));
				else if (splitL.IsEmpty)
					splitL.Add(new Line(splitW));
				else {
					nextL = splitL.FirstElement;
					nextL.Insert(splitW, 0);
					nextL.TryInlineMergeAtIndex(splitW.Count - 1);
				}
				if (!paragraph.TryNext(ref nextP))
					nextP = paragraph.Next;
				nextP.Insert(splitL, 0);
				nextP.TryInlineMergeAtIndex(splitL.Count - 1);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}
			//middle
			Characters splitC = word.SplitAt(context.Character);
			splitW = line.SplitAt(word.Index + 1);
			splitL = paragraph.SplitAt(line.Index + 1);
			line.TryAdd(Words.LineBreak);
			paragraph.TryAdd(new Line(unit));
			//merge splitC into splitW
			if (splitW.IsEmpty)
				splitW.Add(new Word(splitC));
			else {
				Word firstW = splitW.FirstElement;
				if (firstW.IsUtility)
					splitW.InsertValueAt(0, new Word(splitC));
				else
					firstW.Insert(splitC, 0);
			}
			//merge splitW into splitL
			if (splitW.HasLineBreak)
				splitL.InsertValueAt(0, new Line(splitW));
			else if (splitL.IsEmpty)
				splitL.Add(new Line(splitW));
			else {
				nextL = splitL.FirstElement;
				nextL.Insert(splitW, 0);
				nextL.TryInlineMergeAtIndex(splitW.Count - 1);
			}
			if (!paragraph.TryNext(ref nextP))
				nextP = paragraph.Next;
			nextP.Insert(splitL, 0);
			nextP.TryInlineMergeAtIndex(splitL.Count - 1);
			DATA_caret.SetContext(unit.Edit);
			RequestRender();
			return true;
		}
		private bool InsertLineBreak(TextContext context, Character unit) {
			if (!unit.IsLineBreak)
				return false;

			ContextQuery query = ContextQuery.SolveToEdit(DATA_text, context);
			context = query.Context;

			Paragraph paragraph = query.ForceParagraph(true);
			Line line = query.ForceLine(true);

			if (line.IsEmpty) {
				line.TryAdd(new Word(unit));
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			if (line.IsParagraphBreak) {
				//left
				if (context.Character <= 0)
					paragraph.Insert(new Line(unit), line.Index);
				else { 
					//right
					paragraph = paragraph.Next;
					paragraph.Insert(new Line(unit), 0);
				}
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}
			Word word = line.ByIndex(context.Word);

			if (word.IsEmpty) { 
				word.TryAdd(unit);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			Words splitW;
			Line nextL = null;
			Line newL;

			//left
			if (context.Character <= 0) {
				splitW = line.SplitAt(word.Index);
				line.Insert(new Word(unit), word.Index);

				if (!line.TryNext(ref nextL)) { 
					paragraph.TryAdd(new Line(splitW));
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}

				//there is a next line
				if (nextL.IsParagraphBreak) {
					newL = new Line(splitW);
					if (!newL.HasLineBreak)
						newL.TryAdd(Words.LineBreak);
					paragraph.Insert(newL, line.Index + 1);
				}
				//no next line, split Words has LineBreak
				else if (splitW.HasLineBreak) {
					newL = new Line(splitW);
					paragraph.Insert(newL, line.Index + 1);
				}
				else { 
					int count = splitW.Count;
					nextL.Insert(splitW, 0);
					nextL.TryInlineMergeAtIndex(count - 1);
				}
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//right
			if (context.Character >= word.Count) {
				if (word.IsLineBreak) { 
					paragraph.Insert(new Line(unit), line.Index + 1);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				splitW = line.SplitAt(word.Index + 1);
				line.TryAdd(new Word(unit));
				if (!line.TryNext(ref nextL))
					paragraph.TryAdd(new Line(splitW));
				else if (nextL.IsParagraphBreak) {
					newL = new Line(splitW);
					if (!newL.HasLineBreak)
						newL.TryAdd(Words.LineBreak);
					paragraph.Insert(newL, line.Index + 1);
				}
				else if (splitW.HasLineBreak) {
					newL = new Line(splitW);
					paragraph.Insert(newL, line.Index + 1);
				}
				else { 
					int count = splitW.Count;
					nextL.Insert(splitW, 0);
					nextL.TryInlineMergeAtIndex(count - 1);
				}
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//middle
			Characters splitC = word.SplitAt(context.Character);
			splitW = line.SplitAt(word.Index + 1);
			line.TryAdd(new Word(unit));

			//resolve splitW
			if (splitW.IsEmpty) {
				//do nothing
			}
			else if (!line.TryNext(ref nextL)) {
				//no next line
				nextL = new Line(splitW);
				paragraph.TryAdd(nextL);
			}
			else {
				if (splitW.HasLineBreak) {
					nextL = new Line(splitW);
					paragraph.Insert(nextL, line.Index + 1);
				}
				else if (nextL.IsParagraphBreak) {
					nextL = new Line(splitW);
					nextL.TryAdd(Words.LineBreak);
				}
				else {
					int count = splitW.Count;
					nextL.Insert(splitW, 0);
					nextL.TryInlineMergeAtIndex(count - 1);
				}
			}
			//resolve SplitC
			word = nextL.FirstChild;
			if (word.IsUtility)
				nextL.TryAdd(new Word(splitC));
			else
				word.Insert(splitC, 0);
			DATA_caret.SetContext(unit.Edit);
			RequestRender();
			return true;
		}
		private bool InsertWhiteSpace(TextContext context, Character unit) {
			if (!unit.IsWhiteSpace)
				return false;

			ContextQuery query = ContextQuery.SolveToEdit(DATA_text, context);
			Line l = query.ForceLine(true);

			context = query.Context;
			Paragraph paragraph = query.ForceParagraph(true);
			Line line = query.ForceLine(true);

			if (line.IsEmpty) {
				line.TryAdd(new Word(unit));
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			Word word = query.ForceWord(true);
			Word prevW = null;
			bool foundPrevW;

			//determine direction, left
			if (context.Character <= 0) {
				foundPrevW = word.TryPrevious(ref prevW);
				if (foundPrevW)
					word.Parent.Insert(new Word(unit), word.Index);
				else if (word.IsLineBreak)
					word.Parent.Insert(new Word(unit), 0);
				else if (word.IsParagraphBreak) {
					paragraph.FixParagraphEnd();
					line = paragraph.ByIndex(line.Index - 1);
					line.Insert(new Word(unit), line.LastChild.Index);
				}
				else
					line.Insert(new Word(unit), 0);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//right
			int count = word.Count;
			if (context.Character >= count) {
				if (word.IsParagraphBreak) {
					paragraph = paragraph.Next;
					paragraph.FirstChild.Insert(new Word(unit), 0);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				bool foundNextL;
				Line nextL = null;
				if (word.IsLineBreak) {
					foundNextL = line.TryNext(ref nextL);
					if (!foundNextL) {
						nextL = new Line(unit);
						paragraph.TryAdd(nextL);
					}
					nextL.Insert(new Word(unit), 0);
				}
				else
					line.Insert(new Word(unit), word.Index + 1);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//middle
			Characters splitC = word.SplitAt(context.Character);
			Word newW = new Word(unit);
			line.Insert(newW, word.Index + 1);
			Word nextW = null;
			if (newW.TryNext(ref nextW) && !newW.IsUtility)
				newW.Insert(splitC, 0);
			else
				line.Insert(new Word(splitC), newW.Index + 1);
			DATA_caret.SetContext(unit.Edit);
			RequestRender();
			return true;
		}
		private bool InsertTab(TextContext context, Character unit) {
			if (!unit.IsTab)
				return false;

			ContextQuery query = ContextQuery.SolveToEdit(DATA_text, context);

			context = query.Context;
			Paragraph paragraph = query.ForceParagraph(true);
			Line line = query.ForceLine(true);

			if (line.IsEmpty) { 
				line.TryAdd(new Word(unit));
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			Word word = query.ForceWord(true);
			Word prevW = null;
			bool foundPrevW;

			//determine direction, left
			if (context.Character <= 0) {
				foundPrevW = word.TryPrevious(ref prevW);
				if (foundPrevW) 
					word.Parent.Insert(new Word(unit), word.Index);
				else if (word.IsLineBreak)
					word.Parent.Insert(new Word(unit), 0);
				else if (word.IsParagraphBreak) {
					paragraph.FixParagraphEnd();
					line = paragraph.ByIndex(line.Index - 1);
					line.Insert(new Word(unit), line.LastChild.Index);
				}
				else
					line.Insert(new Word(unit), 0);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//right
			int count = word.Count;
			if (context.Character >= count) {
				if (word.IsParagraphBreak) {
					paragraph = paragraph.Next;
					paragraph.FirstChild.Insert(new Word(unit), 0);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				bool foundNextL;
				Line nextL = null;
				if (word.IsLineBreak) {
					foundNextL = line.TryNext(ref nextL);
					if (!foundNextL) {
						nextL = new Line(unit);
						paragraph.TryAdd(nextL);
					}
					nextL.Insert(new Word(unit), 0);
				}
				else
					line.Insert(new Word(unit), word.Index + 1);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//middle
			Characters splitC = word.SplitAt(context.Character);
			Word newW = new Word(unit);
			line.Insert(newW, word.Index + 1);
			Word nextW = null;
			if (newW.TryNext(ref nextW) && !newW.IsUtility)
				newW.Insert(splitC, 0);
			else
				line.Insert(new Word(splitC), newW.Index + 1);
			DATA_caret.SetContext(unit.Edit);
			RequestRender();
			return true;
		}
		private bool InsertRegularCharacter(TextContext context, Character unit) {
			ContextQuery query = ContextQuery.SolveToEdit(DATA_text,context);
			Word w = query.ForceWord(true);
			Debug(query.Context.AsString);

			if (w.IsEmpty||!w.IsUtility) {
				w.TryAdd(unit,context.Character);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			context = query.Context;

			Word word = query.ForceWord(true);
			Paragraph paragraph = query.Paragraph;
			Line line = query.Line;
			Line newL;

			if (w.IsParagraphBreak) {
				//left
				if (context.Character <= 0) {
					newL = new Line(unit);
					newL.TryAdd(Words.LineBreak);
					paragraph.Insert(newL, line.Index - 1);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				//right
				paragraph = paragraph.Next;
				line = paragraph.FirstChild;
				word = line.FirstChild;
				if (word.IsUtility) { 
					line.Insert(new Word(unit), 0);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				word.TryAdd(unit, 0);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}
			

			Word prevW = null;

			//Word is a lBreak
			if (word.IsLineBreak) {
				//left
				if (context.Character <= 0) {
					if (word.TryPrevious(ref prevW)) {
						if (prevW.IsUtility)
							line.Insert(new Word(unit), word.Index);
						else
							prevW.TryAdd(unit);
						DATA_caret.SetContext(unit.Edit);
						RequestRender();
						return true;
					}
					line.Insert(new Word(unit), word.Index);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				//right
				line = line.Next;
				if (line.IsParagraphBreak) {
					newL = new Line(unit);
					newL.TryAdd(Words.LineBreak);
					line.Parent.Insert(newL, line.Index);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}
				word = line.FirstChild;
				if (word.IsUtility)
					line.Insert(new Word(unit), 0);
				else
					word.TryAdd(unit, 0);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//Word is utility
			if (word.IsUtility) {
				//left
				if (context.Character <= 0) {
					if (word.TryPrevious(ref prevW)) {
						if (prevW.IsUtility)
							line.Insert(new Word(unit), word.Index);
						else
							prevW.TryAdd(unit);
						DATA_caret.SetContext(unit.Edit);
						RequestRender();
						return true;
					}
					line.Insert(new Word(unit), word.Index);
					DATA_caret.SetContext(unit.Edit);
					RequestRender();
					return true;
				}

				//right
				word = word.Next;
				if (word.IsUtility)
					line.Insert(new Word(unit), word.Index);
				else
					word.TryAdd(unit, 0);
				DATA_caret.SetContext(unit.Edit);
				RequestRender();
				return true;
			}

			//word is not a utility
			word.TryAdd(unit, context.Character);
			DATA_caret.SetContext(unit.Edit);
			RequestRender();
			return true;
		}


		//Methods
		public void SetText(string text) {
			DATA_text.Clear();
			CaretPosition.Reset();
			Word w = DATA_text.LastWord;
			foreach (char c in text) {
				Character newC = new Character(c);
				if (newC.IsParagraphBreak) {
					if (w.IsEmpty && w.Index <= 0)
						w.TryAdd(newC);
					else
						w.Parent.Next.TryAdd(Words.ParagraphBreak);
					w.Paragraph.FixParagraphEnd();
					w = w.Paragraph.Next.FirstWord;
				}
				else if (newC.IsLineBreak) {
					if (w.IsEmpty)
						w.TryAdd(newC);
					else
						w.Parent.TryAdd(Words.LineBreak);
					w = w.Parent.Next.FirstChild;
				}
				else if (w.IsUtility) {
					if (w.IsEmpty)
						w.TryAdd(newC);
					w.Parent.TryAdd(new Word(newC));
					w = w.Next;
				}
				else
					w.TryAdd(newC);
			}
			DATA_caret.SetContext(DATA_text.Edit);
		}
		public void SetPosition(float x, float y) {
			Position.Set(x, y);
		}

		//debug
		public I_debugInterface DebugInterface;

		public void Debug(string msg) {
			if (DebugInterface == null)
				return;
			DebugInterface.SubmitDebugMessage("{FormUnit_textField} "+msg);
		}
	}

	//Form Description
	public sealed class FormType_textField : Layout2d_unitType {
		#region singleton
		private static volatile FormType_textField instance = new FormType_textField();
		private static object syncRoot = new System.Object();

		//constructor singleton multithread safe
		static FormType_textField ( ) { }
		public static Layout2d_unitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance = new FormType_textField();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "textField"; } }

		public override bool CanHaveChildren {
			get { return true; }
		}
		public override bool CanHaveParent {
			get { return true; }
		}
		public override bool CanHide {
			get { return true; }
		}
		public override bool IsDynamic {
			get { return true; }
		}
	}

}