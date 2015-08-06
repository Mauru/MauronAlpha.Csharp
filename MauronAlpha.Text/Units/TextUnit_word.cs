using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	//Describes a word in a Text
	public class TextUnit_word:TextComponent_unit {

		//Constructors
		public TextUnit_word():base(TextUnitType_word.Instance) {}
		public TextUnit_word(TextUnit_line parent):this() {
			parent.InsertChildAtIndex(parent.ChildCount, this, false);
		}
		
		//Booleans
		public bool IsAtEndOfLine {
			get {
				if (!IsChild)
					return true;
				if (Parent.ChildCount > Index+1)
					return false;
				return true;
			}
		}

		//Count
		public override TextContext CountAsContext {
			get {
				return new TextContext(0,0,0,ChildCount).SetIsReadOnly(true);
			}
		}

		//Int : Index
		public override int Index {
			get { return Context.Word; }
		}

		public TextUnit_word Copy {
			get {
				TextUnit_word copy = new TextUnit_word();
				foreach (TextUnit_character unit in Children)
					copy.InsertChildAtIndex(copy.ChildCount, unit.Copy, false);
				copy.ReIndex(0, true, false);
				return copy;
			}
		}

		//Querying
		public new TextUnit_character ChildByIndex( int n ) {
			if( n<0 || n>ChildCount )
				throw Error( "Index out of bounds!,{"+n+"},(ChildByIndex)", this, ErrorType_index.Instance );
			return ( TextUnit_character ) DATA_children.Value( n );
		}
		public TextUnit_character FirstChild {
			get {
				if (ChildCount > 0)
					return ChildByIndex(0);
				else if (IsReadOnly)
					throw Error("Is protected!,(FirstChild)", this, ErrorType_protected.Instance);
				TextUnit_character result = new TextUnit_character();
				InsertChildAtIndex(0, result, true);
				return ChildByIndex(0);
			}
		}
		public TextUnit_character LastChild {
			get {
				if (ChildCount == 0)
					return FirstChild;
				return ChildByIndex(ChildCount - 1);
			}
		}
		public TextUnit_character CharacterByIndex( int n ) {
			return ChildByIndex( n );
		}

		//Create a new empty character
		public TextUnit_character NewCharacter {
			get {
				TextUnit_character ch = new TextUnit_character();
				InsertChildAtIndex(ChildCount, this, true);
				return ch;
			}
		}

		//Tries to return a character - if not found returns next applicable one
		public TextUnit_character ForceCharacter(int index) {
			if (HasChildAtIndex(index))
				return ChildByIndex(index);
			return NewCharacter;
		}

		//Methods

		//split a word and return the result
		public TextUnit_word SplitAtIndex(int index, bool reIndex) {
			if (index <= 0)
				return this;
			if (index >= ChildCount) {
				if (Parent.HasChildAtIndex(Index + 1))
					return (TextUnit_word) Parent.ChildByIndex(Index + 1);
				else
					return (TextUnit_word) Parent.NewChildAtIndex(Parent.ChildCount, true);
			}
			MauronCode_dataList<I_textUnit> candidates = Children.SetIsReadOnly(false).Split(index);

			TextUnit_word newWord = new TextUnit_word();
			foreach (I_textUnit unit in candidates)
				newWord.InsertChildAtIndex(newWord.ChildCount, unit, false);
			if (reIndex)
				Parent.ReIndex(Index,true,true);
			return newWord;				
		}
	}

	//Decsription of the UnitType
	public class TextUnitType_word : TextUnitType {
		
		public override string Name {
			get {
				return "word";
			}
		}

		public static TextUnitType_word Instance {
			get {
				return new TextUnitType_word();
			}
		}

		public override I_textUnit New {
			get {
				return new TextUnit_word();
			}
		}

		public override I_textUnitType ParentType {
			get {
				return TextUnitType_line.Instance;
			}
		}
		public override I_textUnitType ChildType {
			get {
				return TextUnitType_character.Instance;
			}
		}

	}

}
