using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.Units {
	
	public class Paragraph:TextUnit {

		public override TextUnitType UnitType {
			get { return TextUnitTypes.Paragraph; }
		}

		//constructors
		public Paragraph()	: base(TextUnitTypes.Paragraph) {}
		public Paragraph(Line line)	: this() {
			TryAdd(line);
		}
		public Paragraph(Lines data): this() {
			data.Reverse();
			foreach (Line unit in data)
				TryAdd(unit);
			
		}
		public Paragraph(Words data): this() {
			Line newL = new Line(Words);
			TryAdd(newL);
		}
		public Paragraph(string text) : this() {
			SetText(text);
		}

		//Context
		public TextContext Context {
			get {
				return Parent.Context.Copy.SetParagraph(Index);
			}
		}
		public TextContext CountAsContext {
			get {
				int cl = 0;
				int cw = 0;
				int cc = 0;
				foreach (Line l in Lines) {
					cl++;
					foreach (Word w in l.Words) {
						cw++;
						cc += w.Count;
					}
				}
				return new TextContext(1, cl, cw, cc);
			}
		}
		public TextContext Start {
			get {
				return new TextContext();
			}
		}
		public TextContext End {
			get {
				if (IsEmpty)
					return new TextContext();
				return LastChild.End;
			}
		}
		public TextContext Edit {
			get {
				if (IsEmpty)
					return Context.Copy;
				Line l = LastChild;
				if (l.IsEmpty)
					return l.Context.Copy;
				if (l.IsParagraphBreak)
					return new TextContext(Index + 1, 0, 0, 0);
				if (l.HasLineBreak)
					return new TextContext(Index, l.Index+1, 0, 0);
				Word w = LastWord;
				if (w.IsEmpty)
					return w.Context.Copy;
				if (w.IsUtility)
					return new TextContext(Index, l.Index, w.Index + 1, 0);
				return w.LastChild.Context.Copy.Add(0, 0, 0, 1);
			}
		}
		
		public Encoding Encoding {
			get {
				if (HasParent)
					return Parent.Encoding;
				return MauronAlpha.TextProcessing.DataObjects.Encoding.Instance;
			}
		}

		public string AsString {
			get {
				string result = "";
				foreach (Line l in Lines)
					result += l.AsString;
				return result;
			}
		}

		public int Index;
		public int Count {
			get {
				return Lines.Count;
			}
		}
		public int CountCharacters() {
			int result = 0;
			foreach (Line child in Lines)
				result += child.CountCharacters();
			return result;
		}
		public int CountWords() {
			int result = 0;
			foreach (Line child in Lines)
				result += child.Count;
			return result;
		}

		public bool HasParent {
			get {
				if (DATA_parent == null)
					return false;
				return true;
			}
		}
		public bool IsEmpty {
			get {
				return Lines.IsEmpty;
			}
		}
		public bool HasIndex(int index) {
			if (IsEmpty)
				return false;
			return Lines.ContainsKey(index);
		}
		public bool HasParagraphBreak {
			get {
				if (IsEmpty)
					return false;
				if (LastChild.IsParagraphBreak)
					return true;
				return false;
			}
		}

		public bool Remove(int index) {
			if (IsEmpty)
				return false;
			if (index < 0)
				return false;
			int count = Count;
			if (index >= count)
				return false;
			Lines.RemoveByKey(index);
			Lines.UnShiftIndex(index,this);
			return true;
		}
		public bool Allows(Line line) {
			if (IsEmpty)
				return true;
			if (LastChild.IsParagraphBreak)
				return false;
			return true;
		}
		public bool TryAdd(Line line) {
			if (!Allows(line))
				return false;
			if (!IsEmpty && FirstChild.IsEmpty)
				Lines.RemoveByKey(0);
			line.SetParent(this, Count);
			Lines.Add(line);
			return true;
		}
		public bool TryAdd(Lines lines) {
			if (HasParagraphBreak)
				return false;
			foreach (Line l in lines) {
				l.SetParent(this, Count);
				Lines.Add(l);
			}
			return true;
		}
		public bool Insert(Line unit, int index) {
			if (index < 0)
				index = 0;
			if (index > Count)
				index = Count;
			if (IsEmpty) {
				Lines.Add(unit);
				unit.SetParent(this, 0);
				return true;
			}

			Lines.ShiftIndex(index, this);
			Lines.InsertValueAt(index, unit);
			unit.SetParent(this, index);
			return true;
		}
		public bool Insert(Lines data, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;
			if (index == count && index > 0 && LastChild.IsParagraphBreak)
				return false;
			if (index < count && count > 0 && data.HasParagraphBreak)
				return false;

			int offset = index;
			Lines shift = Lines.Range(index);
			foreach (Line l in data) {
				Lines.InsertValueAt(offset, l);
				l.SetParent(this, offset);
				offset++;
			}
			count = Words.Count;
			foreach (Line l in shift)
				l.SetParent(this, l.Index + count);
			return true;
		}
		public bool TryPrevious(ref Paragraph result) {
			if (Index <= 0)
				return false;
			result = Parent.ByIndex(Index - 1);
			return true;
		}
		public bool TryNext(ref Paragraph result) {
			if (DATA_parent == null)
				return false;
			return Parent.TryIndex(Index+1,ref result);
		}
		public bool TryIndex(int index, ref Line result) {
			if (IsEmpty)
				return false;
			if (index < 0)
				return false;
			int count = Count;
			if (index >= count)
				return false;
			result = Lines.Value(index);
			return true;
		}

		//Attempt to merge line at index - return true if merge took place
		public bool TryInlineMergeAtIndex(int index) {
			if (IsEmpty)
				return false;

			Line target = null;
			Line next = null;

			bool foundTarget = TryIndex(index, ref target);
			bool foundNext = TryIndex(index, ref next);

			if (!foundTarget || !foundNext)
				return false;

			if (target.HasLineBreak)
				return false;

			if (next.IsParagraphBreak)
				return false;

			Words words = next.Words;

			Remove(next.Index);
			foreach (Word w in words)
				target.TryAdd(w);

			return true;			
		}

		//makes sure a paragraphBreak is preceded by a linebreak
		public bool FixParagraphEnd() {
			if (IsEmpty)
				return false;
			if (!LastChild.IsParagraphBreak)
				return false;
			int count = Count;
			if (count == 1) {
				Insert(new Line(Words.LineBreak), count - 2);
				return true;
			}
			Line preLast = ByIndex(count - 2);
			if (preLast.HasLineBreak)
				return false;
			preLast.TryAdd(Words.LineBreak);
			return true;

		}

		private Text DATA_parent;
		public Text Parent {
			get {
				if (DATA_parent == null) {
					Text unit = new Text();
					DATA_parent = unit;
					unit.TryAdd(this);
				}
				return DATA_parent;
			}
		}

		public Paragraph Next {
			get {
				if (!Parent.HasIndex(Index + 1))
					return Parent.NewChild;
				return Parent.ByIndex(Index + 1);
			}
		}

		public Lines LinesUntilParagraphBreak {
			get {
				Lines result = new Lines();
				foreach (Line u in Lines) {
					if (!u.IsParagraphBreak)
						result.Add(u);
					else
						break;
				}
				return result;
			}
		}
		public Lines ChildrenAfterIndex(int index) {
			return Lines.Range(index + 1);
		}

		public Lines ChildrenBeforeIndex(int index) {
			return Lines.Range(0, index);
		}

		public Lines ChildrenByRange(int start, int end) {
			if (start < 0)
				start = 0;
			if (end < 0)
				end = 0;
			return Lines.Range(start, end);
		}

		public Paragraphs LookRight {
			get {
				if (!HasParent)
					return new Paragraphs();
				return Parent.ChildrenAfterIndex(Index);
			}
		}
		public Paragraphs LookLeft {
			get {
				if (!HasParent)
					return new Paragraphs();
				return Parent.ChildrenBeforeIndex(Index);
			}
		}




		//Split the line at index
		public Lines SplitAt(int index) {
			if (index <= 0)
				return new Lines(Lines.RemoveByRange(0));
			if (index >= Count)
				return new Lines();
			return new Lines(Lines.RemoveByRange(index));
		}
		public Lines Lines = new Lines();
		public Line LastChild { 
			get{
				if (IsEmpty)
					return NewChild;
				return Lines.LastElement;
			}			
		}
		public Line NewChild {
			get {
				Line unit = new Line();
				unit.SetParent(this, Count);
				Lines.Add(unit);
				return unit;
			}
		}
		public Line FirstChild {
			get {
				if (IsEmpty)
					return NewChild;
				return Lines.FirstElement;
			}
		}
		public Line ByIndex(int index) {
			if(IsEmpty)
				return NewChild;
			Line result = null;
			if (index < 0)
				index = 0;
			if (TryIndex(index, ref result))
				return result;
			return LastChild;
		}

		public Words Words {
			get {
				Words result = new Words();
				foreach (Line unit in Lines)
					result.AddValuesFrom(unit.Words);
				return result;

			}
		}
		public Word LastWord {
			get {
				if (IsEmpty)
					return NewChild.LastChild;
				return LastChild.LastChild;
			}
		}
		public Word FirstWord {
			get {
				return FirstChild.FirstChild;
			}
		}
		public Word WordByIndex(int index) {
			if (IsEmpty)
				return FirstWord;
			if (index < 0)
				index = 0;
			if (index >= CountWords())
				return LastWord;

			int offset = 0;
			foreach (Line child in Lines) {
				int count = child.Count;
				if (offset + count >= index)
					return child.ByIndex(index - offset);
				offset += count;
			}
			return LastWord;
		}

		public Characters Characters {
			get {
				Characters result = new Characters();
				foreach (Line unit in Lines)
					result.AddValuesFrom(unit.Characters);
				return result;
			}
		}
		public Character LastCharacter {
			get {
				return LastChild.LastCharacter;
			}
		}
		public Character FirstCharacter {
			get {
				return FirstChild.FirstCharacter;
			}
		}
		public Character CharacterByIndex(int index) {
			if (IsEmpty)
				return FirstCharacter;
			if (index < 0)
				index = 0;
			if (index >= CountCharacters())
				return LastCharacter;

			int offset = 0;
			foreach (Line child in Lines) {
				int count = child.CountCharacters();
				if (offset + count >= index)
					return child.CharacterByIndex(index - offset);
				offset += count;
			}

			return LastCharacter;
		}

		public void Clear() {
			Lines = new Lines();
		}
		public void SetParent(Text unit, int index) {
			DATA_parent = unit;
			Index = index;
		}
		public void SetText(string text) {
			Clear();

			Characters chars = new Characters(text);
			Words words = new Words(chars);
			Lines lines = new Lines(words);
			Paragraphs paragraphs = new Paragraphs(lines);
			
			if (paragraphs.IsEmpty) return;
			Paragraph p = paragraphs.Shift;
			foreach (Line l in p.Lines)
				TryAdd(l);
			if (paragraphs.IsEmpty) return;

			int count = paragraphs.Count;
			Parent.Insert(paragraphs, Index + 1);
			Parent.TryInlineMergeAtIndex(Index + count - 1);
		}
	}

	public class TextUnitType_paragraph : TextUnitType {

		public override string Name {
			get {
				return "Paragraph";
			}
		}
		public static TextUnitType_paragraph Instance {
			get {
				return new TextUnitType_paragraph();
			}
		}

	}

}
