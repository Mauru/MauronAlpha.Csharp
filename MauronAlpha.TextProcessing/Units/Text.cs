using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.Units {
	
	public class Text:TextUnit {

		//Constructor
		public Text() : base(TextUnitTypes.Text) { }
		public Text(Paragraphs data) : this() {
			foreach (Paragraph p in data) {
				TryAdd(p);
			}
		}

		public override TextUnitType UnitType {
			get { return TextUnitTypes.Text; }
		}

		public TextContext Context = new TextContext();
		public TextConfiguration Configuration = new TextConfiguration();

		public string AsString {
			get {
				string result = "";
				foreach (Paragraph p in Paragraphs)
					result += p.AsString;
				return result;
			}
		}

		public TextContext CountAsContext {
			get {
				int p = 0;
				int l = 0;
				int w = 0;
				int c = 0;
				foreach (Paragraph pu in Paragraphs) {
					p++;
					foreach (Line lu in pu.Lines) {
						l++;
						foreach(Word wu in lu.Words){
							w++;
							foreach (Character cu in wu.Characters)
								c++;
						}
					}
				}
				return new TextContext(p, l, w, c);
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
					return new TextContext();
				return LastChild.Edit;
			}
		}

		private Encoding DATA_encoding = null;
		public Encoding Encoding {
			get {
				if(DATA_encoding == null)
					return 	MauronAlpha.TextProcessing.DataObjects.Encoding.Instance;
				return DATA_encoding;
			}
		}

		public int Count { get { return Paragraphs.Count; } }
		public int CountCharacters() {
			int result = 0;
			foreach (Paragraph child in Paragraphs)
				result += child.CountCharacters();
			return result;
		}
		public int CountWords() {
			int result = 0;
			foreach (Paragraph child in Paragraphs)
				result += child.CountWords();
			return result;
		}
		public int CountLines() {
			int result = 0;
			foreach (Paragraph child in Paragraphs)
				result += child.Count;
			return result;
		}

		public bool IsEmpty { get { return Paragraphs.IsEmpty; } }
		public bool HasIndex(int index) {
			if (index < 0)
				return false;
			if (index >= Count)
				return false;
			return true;
		}
		public bool HasLineAtIndex(int index) {
			if (index < 0) return false;
			int count = CountLines();
			return index >= count;
		}


		//Conditional Modifiers

		public bool TryAdd(Paragraph unit) {
			if (!IsEmpty && FirstChild.IsEmpty)
				Lines.RemoveByKey(0);
			unit.SetParent(this, Count);
			Paragraphs.Add(unit);
			return true;
		}
		public bool TryAdd(Paragraph unit, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;
			if (IsEmpty) {
				Paragraphs.Add(unit);
				unit.SetParent(this, 0);
				return true;
			}

			Paragraphs.ShiftIndex(index, this);
			Paragraphs.InsertValueAt(index, unit);
			unit.SetParent(this, index);
			return true;
		}
		public bool Remove(int index) {
			if (IsEmpty)
				return false;
			if (index < 0)
				index = 0;
			if (index >= Count)
				return false;
			Paragraphs.UnShiftIndex(index, this);
			Paragraphs.RemoveByKey(index);
			return true;
		}
		public bool TryIndex(int index, ref Paragraph result) {
			if (IsEmpty)
				return false;
			if (index < 0)
				return false;
			int count = Count;
			if (index >= count)
				return false;
			result = ByIndex(index);
			return true;
		}
		public bool TryClosestCharacter(TextContext context, ref Character unit) {
			context = context.Copy.SetMin(0, 0, 0, 0);
			Paragraph p = null;
			if(IsEmpty)
				return false;
			if (!TryIndex(context.Paragraph, ref p)) {
				p = LastChild;
				if (p.IsEmpty) {
					if (!p.TryPrevious(ref p))
						return false;
				}
				unit = p.LastCharacter;
				return true;
			}
			if (p.IsEmpty) {
				if (!p.TryPrevious(ref p))
					return false;
				unit = p.LastCharacter;
				return true;
			}
			Line l = null;
			if (!p.TryIndex(context.Line, ref l)) { 
				unit = p.LastCharacter;
				return true;
			}
			if (l.IsEmpty) {
				if (!l.TryAhead(ref l))
					return false;
				unit = l.LastCharacter;
				return true;
			}
			Word w = null;
			if(!l.TryIndex(context.Word,ref w)) {
				unit = l.LastCharacter;
				return true;
			}
			if (w.IsEmpty) {
				if (!w.TryAhead(ref w))
					return false;
				unit = w.LastChild;
				return true;
			}
			if (!w.TryIndex(context.Character, ref unit)) {
				unit = w.LastChild;
				return true;
			}
			return true;			
		}
		public bool TryCharacterByContext(TextContext context, ref Character unit) {
			Word w = null;
			if (!TryWordByContext(context, ref w))
				return false;
			return w.TryIndex(context.Character, ref unit);
		}
		public bool TryWordByContext(TextContext context, ref Word unit) {
			Line l = null;
			if (!TryLineByContext(context, ref l))
				return false;
			return l.TryIndex(context.Word, ref unit);
		}
		public bool TryLineByContext(TextContext context, ref Line unit) {
			Paragraph p = null;
			if (!TryIndex(context.Paragraph, ref p))
				return false;
			return p.TryIndex(context.Line, ref unit);
		}
		public bool TryInlineMergeAtIndex(int index) {
			if (IsEmpty)
				return false;
			Paragraph target = null;
			Paragraph next = null;
			bool foundTarget = TryIndex(index, ref target);
			bool foundNext = TryIndex(index, ref next);

			if (!foundTarget || !foundNext)
				return false;

			if (target.HasParagraphBreak)
				return false;

			Lines data = next.Lines;
			Remove(next.Index);

			foreach (Line l in data)
				target.TryAdd(l);

			return true;
		}

		//Blind Modifiers
		public Text Add(Paragraph data) {
			int count = Count;
			Paragraphs.Add(data);
			data.SetParent(this, count);
			return this;
		}
		public Text Add(Paragraphs pp) {
			int count = Count;
			int offset = 0;
			foreach (Paragraph p in pp) {
				Paragraphs.Add(p);
				p.SetParent(this, count + offset);
				offset++;
			}
			return this;
		}
		public Text Insert(Paragraphs data, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;
			int offset = index;
			Paragraphs shift = Paragraphs.Range(index);
			foreach (Paragraph p in data) {
				Paragraphs.InsertValueAt(offset, p);
				p.SetParent(this, offset);
				offset++;
			}
			count = data.Count;
			foreach (Paragraph p in shift)
				p.SetParent(this, p.Index + count);
			return this;
		}
		public Text Insert(Paragraph unit, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;
			if (IsEmpty) {
				Paragraphs.Add(unit);
				unit.SetParent(this, 0);
				return this;
			}

			Paragraphs.ShiftIndex(index, this);
			Paragraphs.InsertValueAt(index, unit);
			unit.SetParent(this, index);
			return this;
		}

		public void Clear() {
			Paragraphs = new Paragraphs();
		}
		public void AddChar(char character) {
			Character unit = Encoding.ToCharacter(character);
			Word word = LastWord;
			while(!word.TryAdd(unit))
				word = word.Next;			
		}
		public void InsertChar(TextContext context, char character) {
			Character unit = Encoding.ToCharacter(character);
			Word word = WordByContext(context);
		}

		public Paragraphs ChildrenAfterIndex(int index) {
			return Paragraphs.Range(index + 1);
		}

		public Paragraphs ChildrenBeforeIndex(int index) {
			return Paragraphs.Range(0, index);
		}

		public Paragraphs ChildrenByRange(int start, int end) {
			if (start < 0)
				start = 0;
			if (end < 0)
				end = 0;
			return Paragraphs.Range(start, end);
		}

		public Paragraphs Paragraphs = new Paragraphs();
		public Paragraph NewChild {
			get {
				Paragraph unit = new Paragraph();
				unit.SetParent(this, Count);
				Paragraphs.Add(unit);
				return unit;
			}
		}
		public Paragraph LastChild {
			get {
				if (IsEmpty)
					return NewChild;
				return Paragraphs.LastElement;
			}
		}
		public Paragraph FirstChild {
			get {
				if(IsEmpty)
					return NewChild;
				return Paragraphs.FirstElement;
			}
		}
		public Paragraph ByIndex(int index) {
			if (IsEmpty)
				return NewChild;
			int count = Count;
			if (index >= count)
				return LastChild;
			return Paragraphs.Value(index);
		}

		public Lines Lines {
			get {
				Lines result = new Lines();
				foreach (Paragraph unit in Paragraphs)
					result.AddValuesFrom(unit.Lines);
				return result;
			}
		}
		public Line LastLine {
			get {
				return LastChild.LastChild;
			}
		}
		public Line FirstLine {
			get {
				return FirstChild.FirstChild;
			}
		}
		public Line LineByIndex(int index) {
			if (IsEmpty)
				return FirstLine;
			if (index < 0)
				return FirstLine;
			if (index >= CountLines())
				return LastLine;

			int offset = 0;
			foreach (Paragraph child in Paragraphs) {
				int count = child.Count;
				if (offset + count >= index)
					return child.ByIndex(index - offset);
				offset += count;
			}
			return LastLine;
		}
		public Line LineByContext(TextContext context) {
			Paragraph paragraph = ByIndex(context.Paragraph);
			int index = context.Line;
			if(index<0)
				index = 0;
			Line result = null;
			bool found = paragraph.TryIndex(context.Line, ref result);
			if (found)
				return result;
			if (index == 0)
				return paragraph.FirstChild;
			result = paragraph.LastChild;
			if (!result.IsParagraphBreak && !result.HasLineBreak)
				return paragraph.NewChild;
			return result;
		}

		public Words Words {
			get {
				Words result = new Words();
				foreach (Paragraph unit in Paragraphs)
					result.AddValuesFrom(unit.Words);
				return result;
			}
		}
		public Word WordByContext(TextContext context) {
			Line line = LineByContext(context);
			Word result = null;
			int index = context.Word;
			if (index < 0)
				index = 0;
			if (line.TryIndex(index, ref result))
				return result;
			if (line.LastChild.IsParagraphBreak || line.HasLineBreak)
				return line.LastChild;
			return line.NewChild;
		}
		public Word WordByIndex(int index) {
			if (IsEmpty)
				return FirstWord;
			if (index < 0)
				return FirstWord;
			if (index >= CountWords())
				return LastWord;

			int offset = 0;
			foreach (Paragraph child in Paragraphs) {
				int count = child.CountWords();
				if (offset + count >= index)
					return child.WordByIndex(index - offset);
				offset += count;
			}
			return LastWord;
		}
		public Word LastWord {
			get {
				if (IsEmpty)
					return NewChild.LastWord;
				return LastChild.LastWord;
			}
		}
		public Word FirstWord {
			get {
				return FirstLine.FirstChild;
			}
		}

		public Character LastCharacter {
			get {
				return LastLine.LastCharacter;
			}
		}
		public Character FirstCharacter {
			get {
				return FirstLine.FirstCharacter;
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
			foreach (Paragraph child in Paragraphs) {
				int count = child.CountCharacters();
				if (offset + count >= index)
					return child.CharacterByIndex(index - offset);
				offset += count;
			}

			return LastCharacter;
		}
		public Character CharacterByContext(TextContext context) {
			Word word = WordByContext(context);
			Character result = null;
			int index = context.Character;
			if (index < 0)
				index = 0;
			if (word.TryIndex(index, ref result))
				return result;
			if (word.IsLineBreak || word.IsParagraphBreak)
				return word.LastChild;
			return word.NewChild;
		}

	}

	public class TextUnitType_text : TextUnitType {
		
		public override string Name {
			get {
				return "Text";
			}
		}

		public static TextUnitType_text Instance {
			get {
				return new TextUnitType_text();
			}
		}
	}

}
