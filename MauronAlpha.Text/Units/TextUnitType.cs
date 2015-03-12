using MauronAlpha.Text.Interfaces;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//A Class defining the subtype of a textunit
	public abstract class TextUnitType : MauronCode_subtype,
	I_textUnitType {

		//constructor
		public TextUnitType ( ) : base() { }

		//Name
		public abstract string Name { get; }

		public virtual bool CanHaveParent {
			get { return true; }
		}
		public virtual bool CanHaveChildren {
			get { return true; }
		}

		public bool Equals(I_textUnitType other) {
			return Name == other.Name;
		}

		public abstract I_textUnit New { get; }

		public abstract I_textUnitType ParentType { get; }
		public abstract I_textUnitType ChildType { get; }

		public static TextUnitType_character Character {
			get {
				return TextUnitType_character.Instance;
			}
		}
		public static TextUnitType_word Word {
			get {
				return TextUnitType_word.Instance;
			}
		}
		public static TextUnitType_line Line {
			get {
				return TextUnitType_line.Instance;
			}
		}
		public static TextUnitType_paragraph Paragraph {
			get {
				return TextUnitType_paragraph.Instance;
			}
		}
		public static TextUnitType_text Text {
			get {
				return TextUnitType_text.Instance;
			}
		}

		public static MauronCode_dataList<I_textUnitType> Order = new MauronCode_dataList<I_textUnitType>() { 
			TextUnitType_character.Instance,
			TextUnitType_word.Instance,
			TextUnitType_line.Instance,
			TextUnitType_paragraph.Instance,
			TextUnitType_text.Instance
		};

		public int CompareTo(I_textUnitType other) {
			int pos = Order.IndexOf(this);
			int otherPos = Order.IndexOf(other);

			return pos.CompareTo(otherPos);
		}
}
}