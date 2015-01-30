using System;

using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Utility;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextUnit_line : TextComponent_unit,
	I_textUnit,
	I_textUnit<TextUnit_line> {

		//constructor
		public TextUnit_line (TextUnit_paragraph parent, TextContext context):base(TextUnitType_line.Instance) {
			TXT_parent=parent;
			CTX_context=context;
			SETUP_Utility(parent);
		}
		public TextUnit_line (TextUnit_paragraph parent, TextContext context, MauronCode_dataList<TextUnit_word> children)
			: this(parent, context) {
			DATA_children = children.Instance;			
		}

		//DataTress
		private MauronCode_dataList<TextUnit_word> DATA_children=new MauronCode_dataList<TextUnit_word>();
		private MauronCode_dataList<TextUnit_word> Words {
			get {
				return DATA_children.Instance.SetIsReadOnly(true);
			}
		}

		#region Implementing I_textUnit

		//Representation as string
		public string AsString {
			get {
				if( IsEmpty ) {
					return null;
				}
				string result="";
				foreach( TextUnit_word unit in DATA_children ) {
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
		public TextUnit_line SetIsReadOnly (bool b_isReadOnly) {
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
				foreach( TextUnit_word unit in Children ) {
					if( unit.HasMultiWord ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasWhiteSpace {
			get {
				foreach( TextUnit_word unit in Children ) {
					if( unit.HasWhiteSpace ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach( TextUnit_word unit in Children ) {
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
		public TextUnit_line Instance {
			get {
				return new TextUnit_line(Parent, Context, DATA_children);
			}
		}
		private TextUnit_paragraph TXT_parent;
		public TextUnit_paragraph Parent {
			get { return TXT_parent; }
		}
		public TextUnit_text Source {
			get { return TXT_parent.Source; }
		}
		#endregion

		#region Children

		public MauronCode_dataList<TextUnit_word> Children {
			get {
				return DATA_children.Instance.SetIsReadOnly(true);
			}
		}
		public TextUnit_word FirstChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.FirstElement;
			}
		}
		public TextUnit_word LastChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.LastElement;
			}
		}
		public TextUnit_word NewChild {
			get {
				if( IsReadOnly ) {
					throw Error("Is Protected!,(NewChild)", this, ErrorType_protected.Instance);
				}
				if( HasLimit&&HasReachedLimit ) {
					throw Error("ChildLimit reached!,{"+Limit+"},(NewChild)", this, ErrorType_limit.Instance);
				}
				TextUnit_word unit=new TextUnit_word(this, new TextContext(Context.Paragraph, Index, ChildCount));
				DATA_children.Add(unit);
				return unit;
			}
		}
		public TextUnit_word ChildByIndex (int index) {
			if( index<0 ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			if( index>=ChildCount ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			return DATA_children.Value(index);
		}

		#endregion

		public MauronCode_dataIndex<TextUnit_line> Neighbors {
			get {
				MauronCode_dataIndex<TextUnit_line> result=new MauronCode_dataIndex<TextUnit_line>().SetIsReadOnly(true);
				result.SetValue(0, this);
				foreach( TextUnit_line unit in Parent.Children ) {
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
				return FirstChild.FirstCharacter;
			}
		}
		public override TextUnit_character LastCharacter {
			get {
				return LastChild.LastCharacter;
			}
		}

		#endregion

		#region I_textUnit, IEquatable<>, IEquatable<TextComponent_unit>

		string I_textUnit<TextUnit_line>.AsString {
			get { return AsString; }
		}

		bool I_textUnit<TextUnit_line>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textUnit<TextUnit_line>.IsReadOnly {
			get { return IsReadOnly; }
		}
		bool I_textUnit<TextUnit_line>.IsLastChild {
			get { return IsLastChild; }
		}
		bool I_textUnit<TextUnit_line>.IsFirstChild {
			get {
				return IsFirstChild;
			}
		}

		bool I_textUnit<TextUnit_line>.HasMultiWord {
			get { return HasMultiWord; }
		}
		bool I_textUnit<TextUnit_line>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textUnit<TextUnit_line>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textUnit<TextUnit_line>.HasLimit {
			get { return HasLimit; }
		}
		bool I_textUnit<TextUnit_line>.HasReachedLimit {
			get {
				return HasReachedLimit;
			}
		}

		TextContext I_textUnit<TextUnit_line>.Context {
			get { return Context; }
		}

		int I_textUnit<TextUnit_line>.Index {
			get { return Index; }
		}
		int I_textUnit<TextUnit_line>.Limit {
			get { return Limit; }
		}
		int I_textUnit<TextUnit_line>.ChildCount {
			get { return ChildCount; }
		}

		I_textUnit<TextUnit_line> I_textUnit<TextUnit_line>.Instance {
			get { return Instance; }
		}
		TextComponent_unit I_textUnit<TextUnit_line>.Parent {
			get { return Parent; }
		}
		I_textUnit<TextUnit_text> I_textUnit<TextUnit_line>.Source {
			get { return Source; }
		}

		MauronCode_dataList<TextComponent_unit> I_textUnit<TextUnit_line>.Children {
			get { 
				return Utility.INTERFACE_CovertToUnitList<TextUnit_word>(Children);
			}
		}
		TextComponent_unit I_textUnit<TextUnit_line>.FirstChild {
			get {
				return FirstChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_line>.LastChild {
			get {
				return LastChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_line>.ChildByIndex (int index) {
			return ChildByIndex(index);
		}

		TextComponent_unit I_textUnit<TextUnit_line>.NewChild {
			get {
				return NewChild;
			}
		}

		MauronCode_dataIndex<TextComponent_unit> I_textUnit<TextUnit_line>.Neighbors {
			get { return Utility.INTERFACE_CovertToUnitIndex<TextUnit_line>(Neighbors); }
		}

		TextUnit_character I_textUnit<TextUnit_line>.FirstCharacter {
			get { return FirstCharacter; }
		}
		TextUnit_character I_textUnit<TextUnit_line>.LastCharacter {
			get { return LastCharacter; }
		}

		bool IEquatable<TextUnit_line>.Equals (TextUnit_line other) {
			return Equals(other);
		}

		#endregion

	}

	//Description of the codeType
	public sealed class TextUnitType_line : TextUnitType {
		#region Singleton
		private static volatile TextUnitType_line instance=new TextUnitType_line();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextUnitType_line ( ) { }
		public static TextUnitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextUnitType_line();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "line"; }
		}

	}


}