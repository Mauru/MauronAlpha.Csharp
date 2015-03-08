using System;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	//Base class of a text
	public class TextUnit_text:TextComponent_unit,
	I_textUnit {
		
		//constructor
		public TextUnit_text():base(TextUnitType_text.Instance) {
			
		}
		public TextUnit_text(string text):this() {
			Encoding.StringToUnit(text, this);
		}

		//Context
		public override TextContext Context {
			get {
				if(DATA_context==null)
					DATA_context = new TextContext(Id,0,0,0,0);
				return DATA_context.SetIsReadOnly(true);
			}
		}
		public override TextContext CountAsContext {
			get {
				if(IsEmpty)
					return new TextContext();
				TextContext result = new TextContext(ChildCount,0,0,0);
				foreach(I_textUnit unit in DATA_children)
					result.Add(unit.CountAsContext);
				return result.SetIsReadOnly(true);
			}
		}

		//int Index
		public override int Index { 
			get { 
				return 0; 
			} 
		}

		//Indexing
		public TextUnit_paragraph ChildByIndex( int index ) {
			if( index < 0 || index >= ChildCount )
				throw Error( "Index out of bounds!,{"+index+"},(ChildByIndex)", this, ErrorType_index.Instance );

			return ( TextUnit_paragraph ) DATA_children.Value( index );
		}
		public TextUnit_paragraph ParagraphByIndex (int n) {
			return ChildByIndex(n);
		}
		public TextUnit_line LineByIndex(int index) {
			int offset = 0;
			TextContext count = CountAsContext;

			if( index < 0 || index >= count.Line )
				throw Error( "Index out of Bounds!,{"+index+"},(LineByIndex)", this, ErrorType_index.Instance );

			foreach(TextUnit_paragraph unit in DATA_children) {
				TextContext unit_count = unit.CountAsContext;
				if( offset + unit_count.Line > index )
					return unit.ChildByIndex( index - offset );

				offset += unit_count.Line;
			}

			throw Error( "No unit found!,{"+index+"},(LineByIndex)", this, ErrorType_index.Instance );	

		}
		public TextUnit_word WordByIndex( int index ) {
			int offset = 0;
			TextContext count = CountAsContext;
			if(index < 0 || index >= count.Word)
				throw Error( "Index out of Bounds!,{"+index+"},(WordByIndex)", this, ErrorType_index.Instance );

			foreach(TextUnit_paragraph unit in DATA_children) {
				TextContext unit_count = unit.CountAsContext;
				if( offset + unit_count.Word > index )
					return unit.WordByIndex( index - offset );

				offset += unit_count.Word;
			}

			throw Error( "Index out of Bounds!,{"+index+"},(WordByIndex)", this, ErrorType_index.Instance );	

		}
		public TextUnit_character CharacterByIndex( int index ) {
			TextContext count = CountAsContext;
			if( index < 0 || index > count.Character )
				throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );

			int offset = 0;

			foreach( TextUnit_paragraph unit in DATA_children ) {
				TextContext unit_count = unit.CountAsContext;

				if( index < offset + unit_count.Character )
					return unit.CharacterByIndex( index - offset );

				offset += unit_count.Character;
			}

			throw Error( "Invalid index!{"+index+"},(CharacterByIndex)", this, ErrorType_index.Instance );
		}
	
	}

	//Description of a text unit 
	public class TextUnitType_text:TextUnitType,
	I_textUnitType {
		
		public override string Name { 
			get { 
				return "text"; 
			} 
		}

		public override bool CanHaveParent {
			get { 
				return false; 
			}
		}
	
		public static TextUnitType_text Instance { 
			get {
				return new TextUnitType_text();
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
				return TextUnitType_paragraph.Instance;
			}
		}

	}
}
