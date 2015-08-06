using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	//Describes a Paragraph
	public class TextUnit_paragraph:TextComponent_unit {

		//constructors
		public TextUnit_paragraph():base(TextUnitType_paragraph.Instance){}
		public TextUnit_paragraph ( TextUnit_text parent ) : base(TextUnitType_paragraph.Instance) { 
			parent.InsertChildAtIndex(parent.ChildCount, this, false);			
		}

		//Index
		public override int Index { 
			get { 
				return Context.Paragraph; 
			}
		}

		public TextUnit_paragraph Copy {
			get {
				TextUnit_paragraph copy = new TextUnit_paragraph();
				foreach (TextUnit_line unit in Children)
					copy.InsertChildAtIndex(copy.ChildCount, unit, false);
				copy.ReIndex(0,true,false);
				return copy;
			}
		}

		//Querying
		public new TextUnit_line ChildByIndex(int n) {
			if(n >= ChildCount)
				throw Error("Invalid Index!,{"+n+"},(ChildByIndex)",this,ErrorType_index.Instance);
			return (TextUnit_line) DATA_children.Value(n);
		}
		public TextUnit_line FirstChild {
			get {
				if (ChildCount > 0)
					return ChildByIndex(0);
				else if (IsReadOnly)
					throw Error("Is protected!,(FirstChild)", this, ErrorType_protected.Instance);
				TextUnit_line result = new TextUnit_line();
				InsertChildAtIndex(0, result, true);
				return ChildByIndex(0);
			}
		}
		public TextUnit_line LastChild {
			get {
				if (ChildCount == 0)
					return FirstChild;
				return ChildByIndex(ChildCount - 1);
			}
		}
		public TextUnit_line LineByIndex(int n) {
			return ChildByIndex(n);
		}
		public TextUnit_line NewLine { get {
			if (IsReadOnly)
				throw Error("Is protected!,(NewLine)", this, ErrorType_protected.Instance);
			InsertChildAtIndex(ChildCount, new TextUnit_line(), true);
			return LastChild;
		} }
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

		public MauronCode_dataList<TextUnit_line> Lines {
			get {
				MauronCode_dataList<TextUnit_line> result = new MauronCode_dataList<TextUnit_line>();
				for (int index = 0; index < ChildCount; index++)
					result.Add(ChildByIndex(index));
				return result;
			}
		}
		public MauronCode_dataList<TextUnit_word> Words {
			get {
				MauronCode_dataList<TextUnit_word> result = new MauronCode_dataList<TextUnit_word>();
				foreach (TextUnit_line line in Children)
					foreach (TextUnit_word word in line.Children)
						result.Add(word);
				return result;
			}
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
