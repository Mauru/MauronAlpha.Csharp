using MauronAlpha.HandlingErrors;
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

		//Querying
		public TextUnit_character ChildByIndex( int n ) {
			if( n<0 || n>ChildCount )
				throw Error( "Index out of bounds!,{"+n+"},(ChildByIndex)", this, ErrorType_index.Instance );
			return ( TextUnit_character ) DATA_children.Value( n );
		}
		public TextUnit_character CharacterByIndex( int n ) {
			return ChildByIndex( n );
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
