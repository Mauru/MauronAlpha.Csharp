using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Context;
using MauronAlpha.Text.Collections;

namespace MauronAlpha.Text.Units {

	//A Paragraph in a text
	public class TextUnit_word : TextComponent_unit, I_textUnit<TextUnit_word> {

		//constructor
		public TextUnit_word (TextUnit_line parent, TextContext context)
			: base(TextUnitType_word.Instance) {
			TXT_parent=parent;
			CTX_context=context;
			SETUP_Utility(parent);
		}
		public TextUnit_word (TextUnit_line parent, TextContext context, MauronCode_dataList<TextUnit_character> children)
			: this(parent, context) {
			DATA_children=children.Instance;
		}

		//DataTress
		private MauronCode_dataList<TextUnit_character> DATA_children=new MauronCode_dataList<TextUnit_character>();

		#region Specific to this TextUnit
			public bool HasWordBreak {
				get {
					if( Utility.RangeCompare<TextUnit_character>(DATA_children, 0, 1, Encoding.WordBreaks) ) {
						return true;	
					}
					return false;
				}
			}
		#endregion

		#region Implementing I_textUnit

		//Representation as string
		public string AsString {
			get {
				if( IsEmpty ) {
					return null;
				}
				string result="";
				foreach( TextUnit_character unit in DATA_children ) {
					result+=unit.AsString;
				}
				return result;
			}
		}
		//!Inherited(base) UnitType

		#region Boolean Checks

		public bool IsEmpty {
			get {
				if( DATA_children.Count==0 )
					return true;
				if( FirstChild.IsEmpty )
					return true;

				return false;
			}
		}
		//!Inherited(base) IsReadOnly
		public TextUnit_word SetIsReadOnly (bool b_isReadOnly) {
			B_isReadOnly=b_isReadOnly;
			return this;
		}
		public override bool IsFirstChild {
			get { return Parent.FirstChild.Context.Equals(Context); }
		}
		public override bool IsLastChild {
			get { return Parent.LastChild.Context.Equals(Context); }
		}

		public bool HasMultiWord {
			get {
				return HasWordBreak;
			}
		}
		public bool HasWhiteSpace {
			get {
				foreach( TextUnit_character unit in Children ) {
					if( unit.HasWhiteSpace ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach( TextUnit_character unit in Children ) {
					if( unit.HasLineBreak ) {
						return true;
					}
				}
				return false;
			}
		}
		//!Inherited(base) HasLimit
		//!Inherited(base) HasReachedLimit

		#endregion

		//!Inherited(base) Context

		#region INT queries
		public override int Index {
			get { return 0; }
		}
		//!Inherited(base) Limit
		public override int ChildCount {
			get {
				return DATA_children.Count;
			}
		}
		#endregion

		#region Instancing, Parents
		public TextUnit_word Instance {
			get {
				return new TextUnit_word(Parent, Context, DATA_children);
			}
		}
		private TextUnit_line TXT_parent;
		public TextUnit_line Parent {
			get { return TXT_parent; }
		}
		public TextUnit_text Source {
			get { return TXT_parent.Source; }
		}
		#endregion

		#region Children

		public MauronCode_dataList<TextUnit_character> Children {
			get {
				return DATA_children.Instance.SetIsReadOnly(true);
			}
		}
		public TextUnit_character FirstChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.FirstElement;
			}
		}
		public TextUnit_character LastChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.LastElement;
			}
		}
		public TextUnit_character NewChild {
			get {
				if( IsReadOnly ) {
					throw Error("Is Protected!,(NewChild)", this, ErrorType_protected.Instance);
				}
				if( HasLimit&&HasReachedLimit ) {
					throw Error("ChildLimit reached!,{"+Limit+"},(NewChild)", this, ErrorType_limit.Instance);
				}
				TextUnit_character unit=new TextUnit_character(this, new TextContext(Context.Paragraph, Context.Line, Index, ChildCount));
				DATA_children.Add(unit);
				return unit;
			}
		}
		public TextUnit_character ChildByIndex (int index) {
			if( index<0 ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			if( index>=ChildCount ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			return DATA_children.Value(index);
		}

		#endregion

		public MauronCode_dataIndex<TextUnit_word> Neighbors {
			get {
				MauronCode_dataIndex<TextUnit_word> result=new MauronCode_dataIndex<TextUnit_word>().SetIsReadOnly(true);
				result.SetValue(0, this);
				foreach( TextUnit_word unit in Parent.Children ) {
					if( unit.Index!=Index ) {
						int newIndex=unit.Index-Index;
						result.SetValue(newIndex, this);
					}
				}
				return result.SetIsReadOnly(true);
			}
		}

		public override TextUnit_character FirstCharacter {
			get {
				return FirstChild;
			}
		}
		public override TextUnit_character LastCharacter {
			get {
				return LastChild;
			}
		}

		#endregion

		#region I_textUnit, IEquatable<>, IEquatable<TextComponent_unit>

		string I_textUnit<TextUnit_word>.AsString {
			get { return AsString; }
		}

		bool I_textUnit<TextUnit_word>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textUnit<TextUnit_word>.IsReadOnly {
			get { return IsReadOnly; }
		}
		bool I_textUnit<TextUnit_word>.IsLastChild {
			get { return IsLastChild; }
		}
		bool I_textUnit<TextUnit_word>.IsFirstChild {
			get {
				return IsFirstChild;
			}
		}

		bool I_textUnit<TextUnit_word>.HasMultiWord {
			get { return HasMultiWord; }
		}
		bool I_textUnit<TextUnit_word>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textUnit<TextUnit_word>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textUnit<TextUnit_word>.HasLimit {
			get { return HasLimit; }
		}
		bool I_textUnit<TextUnit_word>.HasReachedLimit {
			get {
				return HasReachedLimit;
			}
		}

		TextContext I_textUnit<TextUnit_word>.Context {
			get { return Context; }
		}

		int I_textUnit<TextUnit_word>.Index {
			get { return Index; }
		}
		int I_textUnit<TextUnit_word>.Limit {
			get { return Limit; }
		}
		int I_textUnit<TextUnit_word>.ChildCount {
			get { return ChildCount; }
		}

		I_textUnit<TextUnit_word> I_textUnit<TextUnit_word>.Instance {
			get { return Instance; }
		}
		TextComponent_unit I_textUnit<TextUnit_word>.Parent {
			get { return Parent; }
		}
		I_textUnit<TextUnit_text> I_textUnit<TextUnit_word>.Source {
			get { return Source; }
		}

		MauronCode_dataList<TextComponent_unit> I_textUnit<TextUnit_word>.Children {
			get { return Utility.INTERFACE_CovertToUnitList<TextUnit_character>(Children); }
		}
		TextComponent_unit I_textUnit<TextUnit_word>.FirstChild {
			get {
				return FirstChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_word>.LastChild {
			get {
				return LastChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_word>.ChildByIndex (int index) {
			return ChildByIndex(index);
		}

		TextComponent_unit I_textUnit<TextUnit_word>.NewChild {
			get {
				return NewChild;
			}
		}

		MauronCode_dataIndex<TextComponent_unit> I_textUnit<TextUnit_word>.Neighbors {
			get { return Utility.INTERFACE_CovertToUnitIndex<TextUnit_word>(Neighbors); }
		}

		TextUnit_character I_textUnit<TextUnit_word>.FirstCharacter {
			get { return FirstCharacter; }
		}
		TextUnit_character I_textUnit<TextUnit_word>.LastCharacter {
			get { return LastCharacter; }
		}

		bool IEquatable<TextUnit_word>.Equals (TextUnit_word other) {
			return Equals(other);
		}

		#endregion

	}

	//Description of the codeType
	public sealed class TextUnitType_word : TextUnitType {
		#region Singleton
		private static volatile TextUnitType_word instance=new TextUnitType_word();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextUnitType_word ( ) { }
		public static TextUnitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextUnitType_word();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "word"; }
		}
	}

}
