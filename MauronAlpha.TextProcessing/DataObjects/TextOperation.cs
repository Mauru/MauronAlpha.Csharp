using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.DataObjects {

	//Track requirements and results of operations which change a text
	public class TextOperation : TextComponent {

		public TextContext origin;

		public ContextQuery Target;

		public TextOperationType Action;

		public string AsString {
			get {
				return "{TEXTOPERATION: " + Action.Name + " @ " + Target.Context.AsString + "}";
			}
		}

		public TextRange Complete() {
			return TextOperation.CompleteOperations(this);
		}

		//constructors
		public TextOperation() : base() { }
		public TextOperation(Text unit, TextOperationType type)	: this() {
			Target = new ContextQuery(unit);
			Action = type;
		}
		public TextOperation(Paragraph unit, TextOperationType type) : this() {
			Target = new ContextQuery(unit);
			Action = type;
		}
		public TextOperation(Line unit, TextOperationType type)	: this() {
			Target = new ContextQuery(unit);
			Action = type;
		}
		public TextOperation(Word unit, TextOperationType type) :this() {
			Target = new ContextQuery(unit);
			Action = type;
		}
		public TextOperation(Character unit, TextOperationType type) : this() {
			Target = new ContextQuery(unit);
			Action = type;
		}

		public static TextOperation InsertCharacter(Character c, TextContext context) {
			ContextQuery pos = new ContextQuery(c.Text, context);
			pos.TrySolve();

			Character o = pos.Character;

			if (o.IsEmpty) {
				int index = o.Index;
				pos.Word.Remove(o.Index);
				pos.Word.Insert(c, index);
				return new TextOperation(c, TextOperation.ReIndexAheadCharacter);
			}

			if (o.IsParagraphBreak) {

				Paragraph p = null;
				if (!o.Paragraph.TryNext(ref p)) {
					p = o.Text.NewChild;
					p.LastWord.Insert(c, 0);
					return new TextOperation(c, TextOperation.Nothing);
				}

				p.FirstWord.Insert(c, 0);
				return new TextOperation(c, TextOperation.ReIndexAheadCharacter);

			}

			if (o.IsLineBreak) {

				Line l = null;
				if (!o.Line.TryAhead(ref l)) {
					l = o.Paragraph.NewChild;
					l.LastChild.Insert(c, 0);
					return new TextOperation(c, TextOperation.Nothing);
				}
				l.FirstChild.Insert(c, 0);
				return new TextOperation(c, TextOperation.ReIndexAheadCharacter);

			}

			if (o.IsUtility) {

				Word w = null;
				if (!o.Parent.TryNext(ref w)) {
					w = o.Line.NewChild;
					w.Insert(c,0);
					return new TextOperation(c, TextOperation.ReIndexAheadCharacter);
				}

				w.Insert(c, 0);
				return new TextOperation(c, TextOperation.ReIndexAheadCharacter);

			}

			if (c.IsUtility) {
				Word newW = new Word(c);
				c.Line.Insert(newW, o.Parent.Index + 1);
				return new TextOperation(c, TextOperation.ReIndexAheadCharacter);
			}

			o.Parent.Insert(c, o.Index + 1);
			return new TextOperation(c, TextOperation.ReIndexAheadCharacter);
		}
		public static TextOperation ReIndex(Character c) {
			Character cc = null;
			if (!c.TryBehind(ref cc))
				return TextOperation.ReIndexAhead(c);
			return TextOperation.ReIndexAhead(cc);
		}
		public static TextOperation ReIndex(Word w) {
			Word ww = null;
			if (!w.TryBehind(ref ww)) {
				if (w.IsEmpty)
					return ReIndexer.MergeNext(w);
				return TextOperation.ReIndexAhead(w.FirstChild);
			}
			return ReIndexer.MergeNext(ww);
		}
		public static TextOperation ReIndex(Line l) {
			Line ll = null;
			if (!l.TryBehind(ref ll)) {
				if (l.IsEmpty)
					return ReIndexer.MergeNext(l);
				return TextOperation.ReIndexAhead(l.FirstCharacter);
			}
			return ReIndexer.MergeNext(ll);
		}
		public static TextOperation ReIndex(Paragraph p) {
			Paragraph pp = null;
			if (!p.TryPrevious(ref pp)) {
				if (p.IsEmpty)
					return ReIndexer.MergeNext(p);
				return TextOperation.ReIndexAhead(p.FirstCharacter);
			}
			return ReIndexer.MergeNext(pp);
		}
		public static TextOperation ReIndex(Text t) {
			if (t.IsEmpty)
				return new TextOperation(t, TextOperation.Nothing);
			return TextOperation.ReIndexAhead(t.FirstCharacter);
		}
		public static TextOperation ReIndexAhead(Character o) {

			if (o.IsParagraphBreak) {

				TextSelector result = TextOperation.SplitAtCharacterUntilParagraphEnd(o);

				if (result.IsEmpty)
					return new TextOperation(o, TextOperation.Nothing);

				Paragraph p = null;
				Character n = null;
				if (!o.Paragraph.TryNext(ref p)) {
					o.Text.Insert(ReIndexer.AssembleIntoParagraphs(result).Paragraphs, o.Paragraph.Index + 1);
					if (!o.TryAhead(ref n))
						return new TextOperation(o, TextOperation.Nothing);
					return new TextOperation(n, TextOperation.ReIndexAheadCharacter);
				}

				TextSelector step = ReIndexer.AssembleIntoParagraphs(result);

				o.Text.Insert(ReIndexer.AssembleIntoParagraphs(step).Paragraphs, o.Paragraph.Index + 1);

				if (!o.TryAhead(ref n))
					return new TextOperation(o, TextOperation.Nothing);
				return new TextOperation(n, TextOperation.ReIndexAheadCharacter);

			}

			if (o.IsLineBreak) {

				TextSelector result = TextOperation.SplitAtCharacterUntilLineEnd(o);


				if (result.IsEmpty)
					return new TextOperation(o.Paragraph, TextOperation.MergeAheadParagraph);

				Paragraph p = null;
				Character n = null;
				o.Paragraph.Insert(ReIndexer.AssembleIntoLines(result).Lines, o.Line.Index + 1);
				if (!o.TryAhead(ref n))
					return new TextOperation(o, TextOperation.Nothing);
				return new TextOperation(n, TextOperation.ReIndexAheadCharacter);
			}

			if (o.IsUtility) {

				TextSelector result = TextOperation.SplitAtCharacterUntilWordEnd(o);

				if (result.IsEmpty)
					return new TextOperation(o.Line, TextOperation.MergeAheadLine);

				TextSelector step = ReIndexer.AssembleIntoWords(result.Characters);
				o.Line.Insert(step.Words, o.Parent.Index + 1);

				Character n = null;
				if (!o.TryAhead(ref n))
					return new TextOperation(o, TextOperation.Nothing);
				return new TextOperation(n, TextOperation.ReIndexAheadCharacter);
			}

			Character nn = null;
			if (o.TryNext(ref nn)) {
				if (nn.IsUtility) {
					Characters cc = TextOperation.SplitAtCharacterUntilWordEnd(o).Characters;
					Words w = ReIndexer.AssembleIntoWords(cc).Words;
					o.Line.Insert(w, o.Parent.Index + 1);
					return new TextOperation(nn, TextOperation.ReIndexAheadCharacter);
				}
				return new TextOperation(nn, TextOperation.ReIndexAheadCharacter);
			}

			return new TextOperation(o.Parent, TextOperation.MergeAheadWord);
		}

		public static TextRange CompleteOperations(TextOperation op) {

			if (op.Action.Equals(TextOperation.Nothing))
				return op.Target.ToTextRange;

			if (op.Action.Equals(TextOperation.MergeAheadLine)) {
				TextOperation result = ReIndexer.MergeNext(op.Target.Line);
				return CompleteOperations(result);
			}
			if (op.Action.Equals(TextOperation.MergeAheadWord)) {
				TextOperation result = ReIndexer.MergeNext(op.Target.Word);
				return CompleteOperations(result);
			}
			if (op.Action.Equals(TextOperation.MergeAheadLine)) {
				TextOperation result = ReIndexer.MergeNext(op.Target.Line);
				return CompleteOperations(result);
			}
			if (op.Action.Equals(TextOperation.MergeAheadParagraph)) {
				TextOperation result = ReIndexer.MergeNext(op.Target.Paragraph);
				return CompleteOperations(result);
			}
			if (op.Action.Equals(TextOperation.ReIndexAheadCharacter)) {
				TextOperation result = TextOperation.ReIndexAhead(op.Target.Character);
				return CompleteOperations(result);
			}
			else
				throw new System.Exception("Unsolved Operation " + op.AsString);
		}

		public static TextSelector SplitAtCharacterUntilParagraphEnd(Character o) {

			TextSelector result = new TextSelector();
			result.AddCharacters(o.Parent.SplitAt(o.Index + 1));
			result.AddWords(o.Line.SplitAt(o.Parent.Index + 1));
			result.AddLines(o.Paragraph.SplitAt(o.Line.Index + 1));

			return result;

		}
		public static TextSelector SplitAtCharacterUntilLineEnd(Character o) {

			TextSelector result = new TextSelector();
			result.AddCharacters(o.Parent.SplitAt(o.Index + 1));
			result.AddWords(o.Line.SplitAt(o.Parent.Index + 1));
			return result;

		}
		public static TextSelector SplitAtCharacterUntilWordEnd(Character o) {
			TextSelector result = new TextSelector();
			result.AddCharacters(o.Parent.SplitAt(o.Index + 1));
			return result;
		}


		//static accessors
		public static TextOperationType Nothing {
			get {
				return new TextOperation_nothing();
			}
		}
		public static TextOperationType MergeAheadLine {
			get {
				return new TextOperation_mergeAheadLine();
			}
		}
		public static TextOperationType MergeAheadWord {
			get {
				return new TextOperation_mergeAheadWord();
			}
		}
		public static TextOperationType MergeAheadParagraph {
			get {
				return new TextOperation_mergeAheadParagraph();
			}
		}

		public static TextOperationType SplitAheadParagraph {
			get {
				return new TextOperation_splitAheadParagraph();
			}
		}
		public static TextOperationType SplitAheadLine {
			get {
				return new TextOperation_splitAheadLine();
			}
		}
		public static TextOperationType SplitAheadWord {
			get {
				return new TextOperation_splitAheadWord();
			}
		}

		public static TextOperationType ReIndexAheadCharacter {
			get {
				return new TextOperation_reIndexAheadCharacter();
			}
		}
	}		

	public abstract class TextOperationType : TextComponent {

		public abstract string Name { get; }

		public abstract TextUnitType SourceUnit { get; }

		public bool Equals(TextOperationType other) {
			return Name.Equals(other.Name);
		}

	}

	public class TextOperation_nothing : TextOperationType {
		public override string Name {
			get {
				return "Nothing";
			}
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Text; }
		}
	}
	public class TextOperation_mergeAheadParagraph : TextOperationType {
		public override string Name {
			get {
				return "MergeAheadParagraph";
			}
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Paragraph; }
		}
	}
	public class TextOperation_mergeAheadLine : TextOperationType {
		public override string Name {
			get {
				return "MergeLineAhead";
			}
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Line; }

		}
	}
	public class TextOperation_mergeAheadWord : TextOperationType {
		public override string Name {
			get {
				return "MergeAheadWord";
			}
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Word; }

		}
	}

	public class TextOperation_splitAheadParagraph : TextOperationType {
		public override string Name {
			get { return "SplitAheadParagraph"; }
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Line; }
		}
	}
	public class TextOperation_splitAheadLine : TextOperationType {
		public override string Name {
			get { return "SplitAheadLine"; }
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Word; }
		}
	}
	public class TextOperation_splitAheadWord : TextOperationType {
		public override string Name {
			get { return "SplitAheadWord"; }
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Character; }
		}
	}

	public class TextOperation_reIndexAheadCharacter : TextOperationType {
		public override string Name {
			get { return "ReIndexAheadCharacter"; }
		}

		public override TextUnitType SourceUnit {
			get { return TextUnitTypes.Character; }
		}
	}
}