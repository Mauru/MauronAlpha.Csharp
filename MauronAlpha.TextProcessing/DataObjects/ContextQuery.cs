using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.DataObjects {
	
	//Use for interacting with text-units and indexing
	public class ContextQuery:TextComponent {

		public readonly Text Text;

		private TextContext D_context;
		//currently returns the result if possible
		public TextContext Context {
			get {
				if (D_context == null)
					D_context = new TextContext();
				return D_context;
			}
		}

		private TextContext D_result;
		public TextContext Result {
			get {
				if(D_result==null) {
					D_result = Context.Copy;
					TrySolve();
				}
				return D_result;
			}
		}

		private Paragraph ResultP;
		public Paragraph Paragraph {
			get {
				if (ResultP != null)
					return ResultP;

				return Text.LastChild;
			}
		}

		private Line ResultL;
		public Line Line {
			get {
				if (ResultL != null)
					return ResultL;

				return Text.LastLine;
			}
		}

		private Word ResultW;
		public Word Word {
			get {
				if (ResultW != null)
					return ResultW;

				return Text.LastWord;
			}
		}

		private Character ResultC;
		public Character Character {
			get {
				if (ResultC != null)
					return ResultC;

				return Text.LastCharacter;
			}
		}

		public TextRange ToTextRange {
			get {
				TextUnitType result = LastKnownTextUnitType;
				if (result.Equals(TextUnitTypes.Text))
					return new TextRange(Text, Text.Start, Text.End);
				if(result.Equals(TextUnitTypes.Paragraph))
					return new TextRange(Paragraph.Parent,Paragraph.Start,Paragraph.End);
				if(result.Equals(TextUnitTypes.Line))
					return new TextRange(Line.Text, Line.Start, Line.End);
				if (result.Equals(TextUnitTypes.Word))
					return new TextRange(Word.Text, Word.Start, Word.End);
				if (result.Equals(TextUnitTypes.Character))
					return new TextRange(Character.Text, Character.Context,Character.Context);
				return TextRange.None;
			}
		}

		//Constructors
		public ContextQuery() :base() {}
		public ContextQuery(Text text, TextContext context) :this() {
			Text = text;
			D_context = context;
		}
		public ContextQuery(Text text, TextContext context, bool resultMode) :this() {
			Text = text;
			D_context = context;

			//we are strict
			if (resultMode == true)
				TryCheck();
			else
				TrySolve();
		}
		public ContextQuery(Text text, TextContext context, TextContext result) :this() {
			Text = text;
			D_context = context;
			D_result = result;
		}

		public ContextQuery(Text t) :this() {
			D_context = t.End;
			Text = t;
		}
		public ContextQuery(Paragraph p) :this() {
			D_context = p.End;
			Text = p.Parent;
			ResultP = p;
		}
		public ContextQuery(Line l) :this() {
			ResultL = l;
			D_context = l.End;
			Text = l.Text;
		}
		public ContextQuery(Word w) :this() {
			ResultW = w;
			D_context = w.End;
			Text = w.Text;
		}
		public ContextQuery(Character c) :this() {
			ResultC = c;
			D_context = c.Context;
			Text = c.Text;
		}

		//Used for instancing
		private ContextQuery(Text text, TextContext context, Paragraph p, Line l, Word w, Character c) {
			Text = text;
			D_context = context;

			ResultP = p;
			ResultL = l;
			ResultW = w;
			ResultC = c;
		}
		private ContextQuery(Text text, TextContext context, Paragraph p, Line l, Word w, Character c, TextContext result) {
			Text = text;
			D_context = context;
			D_result = result;
			ResultP = p;
			ResultL = l;
			ResultW = w;
			ResultC = c;
		}
		public ContextQuery Copy {
			get {
				return new ContextQuery(Text, D_context, ResultP, ResultL, ResultW, ResultC, D_result);
			}
		}

		//Reset all parameters excet context
		public void ResetResult() {
			ResultP = null;
			ResultL = null;
			ResultW = null;
			ResultC = null;
			D_result = null;
		}

		//Check the Current context strictly
		public bool TryCheck() {

			TextContext context = Context.Copy;

			Paragraph p = null;
			if (!Text.TryIndex(Context.Paragraph, ref p)) {
				ResultP = null;
				ResultL = null;
				ResultW = null;
				ResultC = null;
				D_result = null;
				return false;
			}
			ResultP = p;

			Line l = null;
			if (!p.TryIndex(Result.Line, ref l)) {
				ResultL = null;
				ResultW = null;
				ResultC = null;
				D_result = null;
				return false;
			}
			ResultL = l;

			Word w = null;
			if (!l.TryIndex(Result.Word, ref w)) {
				ResultW = null;
				ResultC = null;
				D_result = null;
				return false;
			}
			ResultW = w;

			Character c = null;
			if (!w.TryIndex(Result.Character, ref c)) {
				ResultC = null;
				D_result = null;
				return false;
			}
			ResultC = c;
			return true;		
		}
		//Solve the current context
		public bool TrySolve() {
			//ResetResult();
	
			TextContext context = Context.Copy;
	
			Paragraph p = null;
			if(!Text.TryIndex(Context.Paragraph, ref p)) {
				p = Text.LastChild;
				Result.Set(p.End);
				return false;
			}
			ResultP = p;

			Line l = null;
			if (!p.TryIndex(Result.Line, ref l)) {
				l = p.LastChild;
				Result.Set(l.End);
				return false;
			}
			ResultL = l;

			Word w = null;
			if (!l.TryIndex(Result.Word, ref w)) {
				w = l.LastChild;
				Result.Set(w.End);
				return false;
			}
			ResultW = w;

			//here is gets complicated
			Character c = null;
			if (!w.TryIndex(Result.Character, ref c)) {
				c = w.LastChild;
				Result.Set(w.End);
				return false;
			}
			ResultC = c;
			return true;		
		}

		//Check solved state by boolean
		public bool SolvedIs(bool p, bool l, bool w, bool c) {
			if (!SolvedP == p)
				return false;
			if (!SolvedL == l)
				return false;
			if (!SolvedL == w)
				return false;
			if (!SolvedL == c)
				return false;
			return true;
		}
		public bool IsSolved {
			get {
				return (D_result == null);
			}
		}

		//return a new Character BEFORE the current position
		public ContextQuery SolveForInsert() {
			TextContext context = Result.Copy;
			ContextQuery result = ForceResults();

			Character c = result.Character;
			if (c.IsEmpty)
				return result;

			Paragraph p = c.Paragraph;
			Line l = c.Line;
			Word w = c.Parent;

			if (c.IsParagraphBreak) {
				Line prevL = null;

				if (!l.TryPrevious(ref prevL)) {
					int index = l.Index;
					Line newL = Lines.LineBreak;
					p.Insert(newL, index);
					return new ContextQuery(Text, newL.Context).SolveForInsert();
				}

				return new ContextQuery(Text, prevL.End).SolveForAdd();
			}

			if (c.IsUtility) {

				Word prevW = null;
				if (!w.TryPrevious(ref prevW)) {
					Word newW = new Word();
					l.Insert(newW, 0);
					return new ContextQuery(Text,newW.Context).ForceResults();
				}
				if(prevW.IsUtility) {
					Word newW = new Word();
					l.Insert(newW,w.Index);
					return new ContextQuery(Text, newW.Context).ForceResults();
				}

			}
			Character newC = w.CreateEmptyCharacterAtIndex(c.Index);
			return new ContextQuery(Text, newC.Context).ForceResults();
		}

		//return a new Character AFTER the current position
		public ContextQuery SolveForAdd() {
			TextContext context = Result.Copy;
			ContextQuery result = ForceResults();
			Character c = result.Character;

			if (c.IsEmpty)
				return result;

			if (c.IsParagraphBreak) {
				Paragraph pNext = result.Paragraph.Next;
				Character cNext = result.Paragraph.FirstCharacter;
				if (cNext.IsEmpty)
					return new ContextQuery(Text, cNext.Context, cNext.Paragraph, cNext.Line, cNext.Parent, cNext);

				ContextQuery step = new ContextQuery(Text, cNext.Context).ForceResults();

				return step.SolveForInsert();
			}

			if (c.IsLineBreak) {
				Line l = null;
				if (!c.Line.TryNext(ref l)) {
					l = c.Paragraph.NewChild;

					ContextQuery step = new ContextQuery(Text, l.FirstCharacter.Context).ForceResults();

					return step.SolveForInsert();
				}
			}

			if (c.IsUtility){
				Word w = null;
				if (!c.Parent.TryNext(ref w)) {
					w = c.Line.NewChild;
					return new ContextQuery(Text, w.Context).ForceResults();
				}

				if(w.IsUtility) {
					int index = w.Index;
					bool inserted = w.Parent.Insert(new Word(), index);
					w = w.Parent.ByIndex(index);

					return new ContextQuery(Text, w.Context).ForceResults();
				}

				Character final = w.CreateEmptyCharacterAtIndex(0);
				return new ContextQuery(Text,final.Context).ForceResults();
			}

			Word finalW = c.Parent;
			Character finalC = finalW.CreateEmptyCharacterAtIndex(c.Index + 1);

			return new ContextQuery(Text, finalW.Context).ForceResults();
		}

		public ContextQuery ForceResults() {
			if(D_result == null)
				TrySolve();
			Paragraph p = ResultP;
			if (p == null)
				p = Text.LastChild;
			Line l = ResultL;
			if(ResultL == null)
				l = p.LastChild;
			Word w = ResultW;	
			if(ResultW==null)
				w = l.LastChild;
			Character c = ResultC;	
			if(ResultC==null)
				c = w.LastChild;

			return new ContextQuery(Text,c.Context,p,l,w,c);
		}

		//Check solved states
		public bool SolvedP {
			get {
				return (ResultP == null);
			}
		}
		public bool SolvedL {
			get {
				return (ResultL == null);
			}
		}
		public bool SolvedW {
			get {
				return (ResultW == null);
			}
		}
		public bool SolvedC {
			get {
				return (ResultC == null);
			}
		}

		public TextUnitType LastKnownTextUnitType {
			get {
				if (SolvedC)
					return TextUnitTypes.Character;
				if (SolvedW)
					return TextUnitTypes.Word;
				if (SolvedL)
					return TextUnitTypes.Line;
				if (SolvedP)
					return TextUnitTypes.Paragraph;
				return TextUnitTypes.Text;
			}
		}
	
	}

}
