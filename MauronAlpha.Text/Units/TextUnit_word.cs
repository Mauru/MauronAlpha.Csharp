using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	//Describes a word in a Text
	public class TextUnit_word:TextComponent_unit {

		//Constructors
		public TextUnit_word():base(TextUnitType_word.Instance) {}
		public TextUnit_word(TextUnit_line parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies){
				parent.AddChild(this,false);
				SetContext(parent.Context.Instance.Add(0, 0, parent.ChildCount, 0));
			}
		}
		
		//Methods
		public TextUnit_word SetParent (TextUnit_line parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent=parent;
			if( updateDependencies ){
				parent.AddChild(this, false);
				SetContext(parent.Context.Instance.Add(0, 0, parent.ChildCount, 0));
			}
			return this;
		}
		public TextUnit_word AddChild (TextUnit_character unit, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(AddChild)", this, ErrorType_protected.Instance);

			DATA_children.AddValue(unit);
			if( updateDependencies ){
				unit.SetParent(this, false);
				unit.SetContext(Context.Instance.Add(0, 0, 0, ChildCount));
			}
			return this;
		}
		public TextUnit_word SetText( string str ) {
			if( IsReadOnly )
				throw Error( "Is protected!,(SetText)", this, ErrorType_protected.Instance );

			Clear();

			TextUnit_text text = new TextUnit_text(str);

			TextContext count = text.CountAsContext;

			if( count.Word > 1 || count.Word <= 0 )
				throw Error( "String is not a word!,(SetText)", this, ErrorType_scope.Instance );

			TextUnit_word word = text.WordByIndex( 0 );
			
			foreach(TextUnit_character ch in word.Children)
				AddChild( ch, true );

			return this;
		}

		//Count
		public override TextContext CountAsContext {
			get {
				return new TextContext(0,0,0,ChildCount).SetIsReadOnly(true);
			}
		}

		//Indexing
		public TextUnit_character ChildByIndex( int n ) {
			if( n<0 || n>ChildCount )
				throw Error( "Index out of bounds!,{"+n+"},(ChildByIndex)", this, ErrorType_index.Instance );
			return ( TextUnit_character ) DATA_children.Value( n );
		}
	}

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
	
	}

}
