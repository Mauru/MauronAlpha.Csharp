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
		public readonly Paragraph Paragraph {
			get {
				if (ResultP != null)
					return ResultP;

				return Text.LastChild;
			}
		}

		private Line ResultL;
		public readonly Line Line {
			get {
				if (ResultL != null)
					return ResultL;

				return Text.LastLine;
			}
		}

		private Word ResultW;
		public readonly Word Word {
			get {
				if (ResultW != null)
					return ResultW;

				return Text.LastWord;
			}
		}

		private Character ResultC;
		public readonly Character Character {
			get {
				if (ResultC != null)
					return ResultC;

				return Text.LastCharacter;
			}
		}

		//Solves strictly
		public ContextQuery(Text text, TextContext context) {
			Text = text;
			D_context = context;

			TrySolve(false);
		}
		public ContextQuery(Text text, TextContext context, bool resultMode) {
			Text = text;
			D_context = context;

			//we are strict
			if (resultMode == true)
				TryCheck();
			else
				TrySolve();
		}

		public ContextQuery(Text text, TextContext context, TextContext result) {
			Text = text;
			D_context = context;
			D_result = result;
		}

		public bool IsSolved {
			get {
				return (D_result == null);
			}
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

		//return a new Character
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
	
		//Insertion
		public ContextQuery InsertCharacter(Character c) {
			
			//we trim down to where we are
			ContextQuery check = new ContextQuery(Text, D_result, true);	

			//we want to find out what the current position is

			//a: we found a text that is empty
			if (LastKnownTextUnitType.Equals(TextUnitTypes.Text)) {
				//create new word and add c
				Word w = Text.FirstWord;
				w.TryAdd(c);

				if(c.IsParagraphBreak)
					c.Paragraph.FixParagraphEnd();

				return new ContextQuery(Text, c.Context, c.Paragraph, c.Line, c.Parent, c, c.Context);
			}

			//b: we found a Paragraph
			if (LastKnownTextUnitType.Equals(TextUnitTypes.Paragraph)) {
				Paragraph p = check.Paragraph;

				//is empty
				if(p.IsEmpty) {
					p.FirstChild.TryAdd(new Word(c));
					if(p.HasParent && p.Index > 0) {
						TextRange result = ReIndexer.TryMergeWith(p.Parent.ByIndex(p.Index-1),p);


					}


				}

				if(p.HasParagraphBreak) {

				}

				p.TryAdd(new Line(c));

				if(c.IsParagraphBreak)
					c.Paragraph.FixParagraphEnd();

				return new ContextQuery(Text, c.Context);
			}

			//c: we found a Line that is Empty
			if(LastKnownTextUnitType.Equals(TextUnitTypes.Line)) {
				Line l = check.Line;

				l.TryAdd(new Word(c));
				if(c.IsParagraphBreak)
					c.Paragraph.FixParagraphEnd();

				return new ContextQuery(Text, c.Context);
			}

			//d: we found a Word that is empty
			if (LastKnownTextUnitType.Equals(TextUnitTypes.Word)) {
				Word w = check.Word;

				w.TryAdd(c);
				if (c.IsParagraphBreak)
					c.Paragraph.FixParagraphEnd();

				return new ContextQuery(Text, c.Context);
			}

			//e: we found an actual Character
			if (LastKnownTextUnitType.Equals(TextUnitTypes.Character)) {

				Character root = check.Character;

				//The actual work begins here
				if (c.IsParagraphBreak) { 
					TextSelector result = TextSelector.Next(root);
					
					//we just cut this stuff off
					Characters cutC = root.Parent.SplitAt(check.Character.Index+1);

					Word newW = null;
					if (!cutC.IsEmpty) {
						newW = new Word(cutC);
						//have to merge cutC into next word...						
					}

					Words cutW = root.Line.SplitAt(check.Word.Index + 1);

					Line newL = null;
					if (!cutW.IsEmpty) {
						newL = new Line(cutW);
						//have to merge cutL into next line...
					}

					Paragraph newP = null;
					Lines cutL = root.Paragraph.SplitAt(check.Paragraph.Index + 1);
					if (!cutL.IsEmpty)
						newP = new Paragraph(cutL);
						//have to insert cutP into Text...
					}

					//Now we merge all cut Items together

					TextSelector following = TextSelector.Next(root);
					if(following.Lines.IsEmpty) {
						check.Line.TryAdd(new Word(c));
						c.Paragraph.FixParagraphEnd();
					}

					//TODO: do merge and append action

					ContextQuery final = new ContextQuery(Text,c.Context);
					return final;				
				}

				if(c.IsLineBreak) {

					Character root = check.Character;
					TextSelector result = TextSelector.Next(root);
					
					//we just cut this stuff off
					Characters cutC = root.Parent.SplitAt(check.Character.Index+1);

					Word newW = null;
					if (!cutC.IsEmpty) {
						newW = new Word(cutC);
						//have to merge cutC into next word...						
					}

					Words cutW = root.Line.SplitAt(check.Word.Index + 1);

					//Now we merge all cut Items together
					
					TextSelector following = TextSelector.Next(root);
					if(following.Words.IsEmpty) {
						check.Line.TryAdd(new Word(c));
					}

					//TODO: do merge and append action

					ContextQuery final = new ContextQuery(Text,c.Context);
					return final;

				}

				if (c.IsUtility) {

					Character root = check.Character;
					TextSelector result = TextSelector.Next(root);
					
					//we just cut this stuff off
					Characters cutC = root.Parent.SplitAt(check.Character.Index+1);

					Word newW = null;
					if(!cutC.IsEmpty) {
						newW = new Word(cutC);
					}

					TextSelector following = TextSelector.Next(root);
					if(following.Characters.IsEmpty) {
						check.Line.Insert(new Word(c),root.Parent.Index+1); //might want to use tryInlineMerge!
					}

					//TODO: do merge and append action

					ContextQuery final = new ContextQuery(Text,c.Context);
					return final;


				}

				//is regular character
				else {
					Character root = check.Character;
					TextSelector result = TextSelector.Next(root);

					//Here we actually need to know what root is
					if(result.Characters.IsEmpty) {

						if(root.IsEmpty)
						

					}

				}


			}


			

		}
	
	}

}
