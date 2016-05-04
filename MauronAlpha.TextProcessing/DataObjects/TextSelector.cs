using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

using MauronAlpha.HandlingData;

namespace MauronAlpha.TextProcessing.DataObjects {
	
	public class TextSelector:TextComponent {


		public static int DefaulResolution = 5;

		//constructors
		public TextSelector() : base() { }
		public TextSelector(Paragraphs p, Lines l, Words w, Characters c ):this() {
			D_c = c;
			D_w = w;
			D_l = l;
			D_p = p;
		}
		public TextSelector(Characters c):this() {
			D_c = c;
		}
		public TextSelector(Words w) : this() {
			D_w = w;
		}
		public TextSelector(Lines l) : this() {
			D_l = l;
		}
		public TextSelector(Paragraphs p) : this() {
			D_p = p;
		}

		//Instancing
		public TextSelector Copy() {
			TextSelector result = new TextSelector();
			if (D_p != null)
				result.SetParagraphs(D_p);
			if (D_l != null)
				result.SetLines(D_l);
			if (D_w != null)
				result.SetWords(D_w);
			if (D_c != null)
				result.SetCharacters(D_c);

			return result;
		}

		//Buffer Data and Accessors
		private Paragraphs D_p;
		public Paragraphs Paragraphs {
			get {
				if (D_p == null)
					return new Paragraphs();
				return D_p;
			}
		}
		private Lines D_l;
		public Lines Lines {
			get {
				if (D_l == null)
					return new Lines();
				return D_l;
			}
		}
		public Words Words {
			get {
				if (D_w == null)
					return new Words();
				return D_w;
			}
		}
		private Words D_w;
		public Characters Characters {
			get {
				if (D_c == null)
					return new Characters();
				return D_c;
			}
		}
		private Characters D_c;

		//Static Queries
		public static TextSelector Empty {
			get {
				return new TextSelector(new Paragraphs(), new Lines(), new Words(), new Characters());
			}
		}
		public static TextSelector Previous(Character c) {


			Characters ac = c.LookLeft;
			Words aw = c.Parent.LookLeft;
			Lines al = c.Line.LookLeft;
			Paragraphs ap = c.Paragraph.LookLeft;

			return new TextSelector(ap, al, aw, ac);

		}
		public static TextSelector Next(Character c) {

			Characters ac = c.LookRight;
			Words aw = c.Parent.LookRight;
			Lines al = c.Line.LookRight;
			Paragraphs ap = c.Paragraph.LookRight;

			return new TextSelector(ap, al, aw, ac);

		}
		public static TextSelector PreviousCharactersByOffset(Character c, int amount) {
			if (!c.HasParent)
				return TextSelector.Empty;

			int end = c.Index;
			if (end <= 0)
				return TextSelector.Empty;

			int start = c.Index - amount;

			if (start <= 0)
				start = 0;

			return new TextSelector(c.Parent.ChildrenByRange(start, end));
		}

		//Indexing Queries - Analyze Content 
		public TextUnitType HighestUnitType {
			get {
				if(!D_p.IsEmpty)
					return TextUnitTypes.Paragraph;
				if(!D_l.IsEmpty)
					return TextUnitTypes.Line;
				if(!D_w.IsEmpty)
					return TextUnitTypes.Word;
				if (!D_c.IsEmpty)
					return TextUnitTypes.Character;
				return TextUnitTypes.None;
			}
		}
		public TextUnitType HighestMultipleUnitType {
			get {
				if (!D_p.IsEmpty && D_p.Count > 1)
					return TextUnitTypes.Paragraph;
				if (!D_l.IsEmpty && D_l.Count > 1)
					return TextUnitTypes.Line;
				if (!D_w.IsEmpty && D_w.Count > 1)
					return TextUnitTypes.Word;
				if (!D_c.IsEmpty && D_c.Count > 1)
					return TextUnitTypes.Character;
				return TextUnitTypes.None;
			}
		}

		//Buffer Modifiers
		public TextSelector AddCharacters(Characters aa) {
			if (D_c != null)
				D_c.AddValuesFrom(aa);
			else
				D_c = aa;

			return this;
		}
		public TextSelector SetCharacters(Characters aa) {
			D_c = aa;
			return this;
		}
		public TextSelector UnsetCharacters() {
			D_c = null;
			return this;
		}
		public TextSelector AddWords(Words aa) {
			if (D_w != null)
				D_w.AddValuesFrom(aa);
			else
				D_w = aa;

			return this;
		}
		public TextSelector SetWords(Words aa) {
			D_w = aa;
			return this;
		}
		public TextSelector UnsetWords() {
			D_w = null;
			return this;
		}
		public TextSelector AddLines(Lines aa) {
			if (D_l != null)
				D_l.AddValuesFrom(aa);
			else
				D_l = aa;

			return this;
		}
		public TextSelector SetLines(Lines aa) {
			D_l = aa;
			return this;
		}
		public TextSelector UnsetLines() {
			D_l = null;
			return this;
		}
		public TextSelector AddParagraphs(Paragraphs aa) {
			if (D_p != null)
				D_p.AddValuesFrom(aa);
			else
				D_p = aa;

			return this;
		}
		public TextSelector SetParagraphs(Paragraphs aa) {
			D_p = aa;
			return this;
		}
		public TextSelector UnsetParagraphs() {
			D_p = null;
			return this;
		}

		//Conditionals
		public bool HasCharacters {
			get {
				if (D_c == null)
					return false;
				if (D_c.IsEmpty)
					return false;
				return true;
			}
		}
		public bool HasWords {
			get {
				if (D_w == null)
					return false;
				if (D_w.IsEmpty)
					return false;
				return true;
			}
		}
		public bool HasLines {
			get {
				if (D_l == null)
					return false;
				if (D_l.IsEmpty)
					return false;
				return true;
			}
		}
		public bool HasParagraphs {
			get {
				if (D_p == null)
					return false;
				if (D_p.IsEmpty)
					return false;
				return true;
			}
		}
		
		
		/* #TODO
		public static TextSelector PrevUntilParagraphBreak(Character c) {

			if (!c.HasParent)
				return TextSelector.Empty;

			Characters ac = c.LookLeft;
			ac = ac.Reverse();
			int final = 0;

			foreach (Character cc in ac) {
				if (cc.IsParagraphBreak) {
					final = cc.Index;
					break;
				}
			}
			int start = final;
			int end = c.Index-1;
			Characters finalC = c.Parent.ChildrenByRange(start, end);

			if (!finalC.IsEmpty)
				return new TextSelector(finalC);

			TextSelector aw = TextSelector.PrevUntilParagraphBreak(c.Parent);


		
		}

		public static TextSelector PrevUntilParagraphBreak(Word w) {
			if (!w.HasParent)
				return TextSelector.Empty;

			Words aw = w.LookLeft;
			aw.Reverse();
			int final = 0;
			foreach (Word ww in aw) {
				if (ww.IsParagraphBreak) { 
					final = ww.Index;
					break;
				}
			}
			int start = final;
			int end = w.Index - 1;
			Words finalC = w.Parent.ChildrenByRange(start, end);



		}*/



		public bool IsEmpty {
			get {
				return ( Paragraphs.IsEmpty && Lines.IsEmpty && Words.IsEmpty && Characters.IsEmpty );
			}
		}
	}
}
