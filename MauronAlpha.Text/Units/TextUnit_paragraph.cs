using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	//Describes a Paragraph
	public class TextUnit_paragraph:TextComponent_unit {

		//constructors
		public TextUnit_paragraph():base(TextUnitType_paragraph.Instance){}
		public TextUnit_paragraph(TextUnit_text parent, bool updateParent):this() {
			UNIT_parent = parent;

			if(updateParent)
				parent.InsertChildAtIndex(parent.ChildCount, this, false, false);
		}

		//Index
		public override int Index { 
			get { 
				return Context.Paragraph; 
			}
		}

		//Querying
		public TextUnit_line ChildByIndex(int n) {
			if(n >= ChildCount)
				throw Error("Invalid Index!,{"+n+"},(ChildByIndex)",this,ErrorType_index.Instance);
			return (TextUnit_line) DATA_children.Value(n);
		}
		public TextUnit_line LineByIndex(int n) {
			return ChildByIndex(n);
		}
		public TextUnit_word WordByIndex( int index ) {
			int offset = 0;
			TextContext count = CountAsContext;
			if( index < 0 || index >= count.Word )
				throw Error( "Index out of Bounds!,{"+index+"},(WordByIndex)", this, ErrorType_index.Instance );

			foreach( TextUnit_line unit in DATA_children ) {
				TextContext unit_count = unit.CountAsContext;
				if( offset + unit_count.Word > index )
					return unit.ChildByIndex( index - offset );

				offset += unit_count.Word;
			}

			throw Error( "Index out of Bounds!,{"+index+"},(WordByIndex)", this, ErrorType_index.Instance );

		}
		public TextUnit_character CharacterByIndex( int index ) {
			TextContext count = CountAsContext;
			if( index < 0 || index > count.Character )
				throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );

			int offset = 0;

			foreach( TextUnit_line unit in DATA_children ) {
				TextContext unit_count = unit.CountAsContext;

				if( index < offset + unit_count.Character )
					return unit.CharacterByIndex( index - offset );

				offset += unit_count.Character;
			}

			throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );
		}

		//Count
		public override TextContext CountAsContext {
			get {
				if( IsEmpty )
					return new TextContext();
				TextContext result=new TextContext(0, ChildCount, 0, 0);
				foreach( I_textUnit unit in DATA_children )
					result.Add(unit.CountAsContext);
				return result.SetIsReadOnly(true);
			}
		}

	}

	//Description of type Capabilities
	public class TextUnitType_paragraph:TextUnitType {
		
		private TextUnitType_paragraph():base(){}

		public override string Name { 
			get {
				return "paragraph";
			}
		}

		public static TextUnitType_paragraph Instance { 
			get {
				return new TextUnitType_paragraph();
			}
		}

		public override I_textUnit New {
			get {
				return new TextUnit_paragraph();
			}
		}

		public override I_textUnitType ParentType {
			get {
				return TextUnitType_text.Instance;
			}
		}
		public override I_textUnitType ChildType {
			get {
				return TextUnitType_line.Instance;
			}
		}

	}

}
