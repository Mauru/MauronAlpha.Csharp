using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	//Represents a line of text in a pragraph
	public class TextUnit_line:TextComponent_unit {

		//Constructors
		public TextUnit_line():base(TextUnitType_line.Instance) {}
		public TextUnit_line(TextUnit_paragraph parent):this() {
			parent.InsertChildAtIndex(parent.ChildCount, this, false);
		}
		public TextUnit_line(string text) : this() {
			SetText(text);
		}

		//return a copy of the line - without parent assocations
		public TextUnit_line Copy { get {
			TextUnit_line copy = new TextUnit_line();
			foreach (TextUnit_word word in Children)
				copy.InsertChildAtIndex(copy.ChildCount,word.Copy,false);
			copy.ReIndex(0,true,false);
			return copy;
		} }

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
		public int CountRealCharacters {
			get {
				int result = 0;
				foreach (TextUnit_character ch in Characters)
					if (ch.IsRealCharacter)
						result++;
				return result;
			}
		}
		//Int : Index
		public override int Index {
			get { 
				return Context.Line; 
			}
		}


		//Methods
		public TextUnit_line SetText(string text) {
			if (IsReadOnly)
				throw Error("Is protected!,(SetText)", this, ErrorType_protected.Instance);
			base.Clear();
			TextUnit_text txt = new TextUnit_text(text);
			MauronCode_dataList<TextUnit_word> words = txt.Words;
			foreach (TextUnit_word word in words)
				InsertChildAtIndex(ChildCount, word, false);
			Parent.ReIndex(Index, true, false);
			return this;
		}
		public TextUnit_line PrependText(string text, bool updateParent) {
			if (IsReadOnly)
				throw Error("Is protected!,(PrependText)", this, ErrorType_protected.Instance);
			TextUnit_text txt = new TextUnit_text(text);
			MauronCode_dataList<TextUnit_word> words = txt.Words;
			for (int n = 0; n < words.Count; n++)
				InsertChildAtIndex(n, words.Value(n), false);
			if (updateParent)
				Parent.ReIndex(Index, true, true);
			return this;
		}
		public TextUnit_line AppendText(string text, bool updateParent) {
			if (IsReadOnly)
				throw Error("Is protected!,(PrependText)", this, ErrorType_protected.Instance);
			TextUnit_text txt = new TextUnit_text(text);
			MauronCode_dataList<TextUnit_word> words = txt.Words;
			foreach (TextUnit_word word in words)
				InsertChildAtIndex(ChildCount, word, false);
			if (updateParent)
				Parent.ReIndex(Index, true, true);
			return this;
		}

		//Querying
		public new TextUnit_word ChildByIndex( int n ) {
			if( n<0||n>ChildCount )
				throw Error( "Index out of bounds!,{"+n+"},(ChildByIndex)", this, ErrorType_index.Instance );
			return (TextUnit_word) DATA_children.Value( n );
		}
		public TextUnit_word FirstChild {
			get {
				if (ChildCount > 0)
					return ChildByIndex(0);
				else if (IsReadOnly)
					throw Error("Is protected!,(FirstChild)", this, ErrorType_protected.Instance);
				TextUnit_word result = new TextUnit_word();
				InsertChildAtIndex(0, result, true);
				return ChildByIndex(0);
			}
		}
		public TextUnit_word LastChild {
			get {
				if (ChildCount == 0)
					return FirstChild;
				return ChildByIndex(ChildCount - 1);
			}
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

	//Description of the UnitType
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

		public override I_textUnit New {
			get {
				return new TextUnit_line();
			}
		}

		public override I_textUnitType ParentType {
			get {
				return TextUnitType_paragraph.Instance;
			}
		}
		public override I_textUnitType ChildType {
			get {
				return TextUnitType_word.Instance;
			}
		}

	}

}
