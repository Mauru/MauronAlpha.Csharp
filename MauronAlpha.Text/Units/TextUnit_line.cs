using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	//Represents a line of text in a pragraph
	public class TextUnit_line:TextComponent_unit {

		//Constructors
		public TextUnit_line():base(TextUnitType_line.Instance) {}
		public TextUnit_line(TextUnit_paragraph parent, bool updateParent ):this() {
			UNIT_parent = parent;
		}
		public TextUnit_line( string text ) : this() {
			SetText( text );
		}

		//Count
		public override TextContext CountAsContext {
			get {
				if( IsEmpty )
					return new TextContext();
				TextContext result = new TextContext(0, 0, ChildCount, 0);
				foreach(I_textUnit unit in DATA_children)
					result.Add(unit.CountAsContext);
				return result.SetIsReadOnly(true);
			}
		}

		//Int : Index
		public override int Index {
			get { return Context.Line; }
		}

		//Querying
		public TextUnit_word ChildByIndex( int n ) {
			if( n<0||n>ChildCount )
				throw Error( "Index out of bounds!,{"+n+"},(ChildByIndex)", this, ErrorType_index.Instance );
			return (TextUnit_word) DATA_children.Value( n );
		}
		public TextUnit_word WordByIndex( int n ) {
			return ChildByIndex(n);
		}
		public TextUnit_character CharacterByIndex( int index ) {
			TextContext count = CountAsContext;
			if( index < 0 || index > count.Character )
				throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );

			int offset = 0;

			foreach( TextUnit_word unit in DATA_children ) {
				TextContext unit_count = unit.CountAsContext;

				if( index < offset + unit_count.Character )
					return unit.ChildByIndex( index - offset );

				offset += unit_count.Character;
			}

			throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );
		}

	}

	public class TextUnitType_line:TextUnitType {
		
		public override string Name {
			get {
				return "line";
			}
		}
	
		public static TextUnitType_line Instance {
			get {
				return new TextUnitType_line();
			}
		}
	
	}

}
