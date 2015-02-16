﻿using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	public class TextUnit_paragraph:TextComponent_unit,
	I_textUnit {

		//constructors
		public TextUnit_paragraph():base(TextUnitType_paragraph.Instance){}
		public TextUnit_paragraph(TextUnit_text parent, bool updateDependencies):this() {
			UNIT_parent = parent;
			if(updateDependencies){
				parent.AddChild(this,false);
				SetContext(parent.Context.Instance.Add(parent.ChildCount, 0, 0, 0));
			}
		}

		//Members
		public TextUnit_paragraph SetParent(TextUnit_text parent, bool updateDependencies) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			UNIT_parent = parent;
			if(updateDependencies){
				parent.AddChild(this,false);
				SetContext(parent.Context.Instance.Add(parent.ChildCount, 0, 0, 0));
			}
			return this;
		}
		public TextUnit_paragraph AddChild(TextUnit_line unit, bool updateDependencies) {
			if(IsReadOnly)
				throw Error("Is protected!,(AddChild)",this,ErrorType_protected.Instance);
			DATA_children.AddValue(unit);
			if(updateDependencies){
				unit.SetParent(this,false);
				unit.SetContext(Context.Instance.Add(0, ChildCount, 0, 0));
			}
			return this;
		}

		//Indexing
		public TextUnit_line ChildByIndex(int n) {
			if(n >= ChildCount)
				throw Error("Invalid Index!,{"+n+"},(ChildByIndex)",this,ErrorType_index.Instance);
			return (TextUnit_line) DATA_children.Value(n);
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

	}

}
