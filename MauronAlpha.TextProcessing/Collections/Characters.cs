using MauronAlpha.HandlingData;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.Collections {
	
	public class Characters:MauronCode_dataList<Character> {

		public Characters() : base() { }
		public Characters(MauronCode_dataList<Character> data): this() {
			base.AddValuesFrom(data);
		}
		public Characters(string text) : this() {
				foreach (char c in text)
					Add(new Character(c));
		}

		public new Characters Reverse() {
			base.Reverse();
			return this;
		}
		public new Characters Range(int index) {
			return new Characters(base.Range(index));
		}
		public new Characters Range(int start,int end) {
			return new Characters(base.Range(start,end));
		}

		public bool HasUtility {
			get {
				foreach (Character c in this)
					if (c.IsUtility)
						return true;
				return false;
			}
		}
		public bool HasParagraphBreak {
			get {
				foreach (Character c in this)
					if (c.IsParagraphBreak)
						return true;
				return false;
			}
		}

		public static Character WhiteSpace {
			get {
				Character result = new Character();
				result.IsWhiteSpace = true;
				return result;
			}
		}
		public static Character Tab {
			get {
				Character result = new Character();
				result.IsTab = true;
				return result;
			}
		}
		public static Character LineBreak{
			get {
				Character result = new Character();
				result.IsLineBreak = true;
				return result;
			}
		}
		public static Character ParagraphBreak {
			get {
				Character result = new Character();
				result.IsParagraphBreak = true;
				return result;
			}
		}
		public static Character Empty {
			get {
				Character unit = new Character();
				unit.IsEmpty = true;
				return unit;
			}
		}

		public void ShiftIndex(int index, Word parent) {
			MauronCode_dataList<Character> candidates = Range(index);
			foreach (Character unit in candidates)
				unit.SetParent(parent, unit.Index + 1);
		}
		public void UnShiftIndex(int index, Word parent) {
			MauronCode_dataList<Character> candidates = Range(index);
			foreach (Character unit in candidates)
				unit.SetParent(parent, unit.Index -1);
		}

	}

}
