using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Collections;

namespace MauronAlpha.Text.Units {

	//A Paragraph in a text
	public class TextUnit_character : TextComponent_unit,
	I_textUnit,
	I_textUnit<TextUnit_character> {

		//constructor
		public TextUnit_character (TextUnit_word parent, TextContext context)
			: base(TextUnitType_character.Instance) {
			TXT_parent=parent;
			CTX_context=context;
			SETUP_Utility(parent);
		}
		public TextUnit_character (TextUnit_word parent, TextContext context, MauronCode_dataList<char> children)
			: this(parent, context) {
			DATA_children=children.Instance;
		}

		//special constructor
		public TextUnit_character(MauronCode_dataList<char> units):base(TextUnitType_character.Instance) {
			DATA_children = units;
		}

		//DataTress
		private MauronCode_dataList<char> DATA_children=new MauronCode_dataList<char>();

		#region Implementing I_textUnit

		//Representation as string
		public string AsString {
			get {
				if( IsEmpty ) {
					return null;
				}
				string result="";
				foreach( char unit in DATA_children ) {
					result+=unit;
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
				if( DATA_children.FirstElement == Encoding.Empty)
					return true;

				return false;
			}
		}
		//!Inherited(base) IsReadOnly
		public TextUnit_character SetIsReadOnly (bool b_isReadOnly) {
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
				return false;
			}
		}
		public bool HasWhiteSpace {
			get {
				return false;
			}
		}
		public bool HasLineBreak {
			get {
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
		public TextUnit_character Instance {
			get {
				return new TextUnit_character(Parent, Context, DATA_children);
			}
		}
		private TextUnit_word TXT_parent;
		public TextUnit_word Parent {
			get { return TXT_parent; }
		}
		public TextUnit_text Source {
			get { return TXT_parent.Source; }
		}
		#endregion

		#region Children

		public MauronCode_dataList<char> Children {
			get {
				return DATA_children.Instance.SetIsReadOnly(true);
			}
		}
		public char FirstChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.FirstElement;
			}
		}
		public char LastChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.LastElement;
			}
		}
		public char NewChild {
			get {
				if( IsReadOnly ) {
					throw Error("Is Protected!,(NewChild)", this, ErrorType_protected.Instance);
				}
				if( HasLimit&&HasReachedLimit ) {
					throw Error("ChildLimit reached!,{"+Limit+"},(NewChild)", this, ErrorType_limit.Instance);
				}
				char unit=Encoding.Empty;
				DATA_children.Add(unit);
				return unit;
			}
		}
		public char ChildByIndex (int index) {
			if( index<0 ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			if( index>=ChildCount ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			return DATA_children.Value(index);
		}

		#endregion

		public MauronCode_dataIndex<TextUnit_character> Neighbors {
			get {
				MauronCode_dataIndex<TextUnit_character> result=new MauronCode_dataIndex<TextUnit_character>().SetIsReadOnly(true);
				result.SetValue(0, this);
				foreach( TextUnit_character unit in Parent.Children ) {
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
				return Instance;
			}
		}
		public override TextUnit_character LastCharacter {
			get {
				return Instance;
			}
		}

		#endregion

		#region I_textUnit, IEquatable<>, IEquatable<TextComponent_unit>

		string I_textUnit<TextUnit_character>.AsString {
			get { return AsString; }
		}

		bool I_textUnit<TextUnit_character>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textUnit<TextUnit_character>.IsReadOnly {
			get { return IsReadOnly; }
		}
		bool I_textUnit<TextUnit_character>.IsLastChild {
			get { return IsLastChild; }
		}
		bool I_textUnit<TextUnit_character>.IsFirstChild {
			get {
				return IsFirstChild;
			}
		}

		bool I_textUnit<TextUnit_character>.HasMultiWord {
			get { return HasMultiWord; }
		}
		bool I_textUnit<TextUnit_character>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textUnit<TextUnit_character>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textUnit<TextUnit_character>.HasLimit {
			get { return HasLimit; }
		}
		bool I_textUnit<TextUnit_character>.HasReachedLimit {
			get {
				return HasReachedLimit;
			}
		}

		TextContext I_textUnit<TextUnit_character>.Context {
			get { return Context; }
		}

		int I_textUnit<TextUnit_character>.Index {
			get { return Index; }
		}
		int I_textUnit<TextUnit_character>.Limit {
			get { return Limit; }
		}
		int I_textUnit<TextUnit_character>.ChildCount {
			get { return ChildCount; }
		}

		I_textUnit<TextUnit_character> I_textUnit<TextUnit_character>.Instance {
			get { return Instance; }
		}
		TextComponent_unit I_textUnit<TextUnit_character>.Parent {
			get { return Parent; }
		}
		I_textUnit<TextUnit_text> I_textUnit<TextUnit_character>.Source {
			get { return Source; }
		}

		MauronCode_dataList<TextComponent_unit> I_textUnit<TextUnit_character>.Children {
			get { return Utility.INTERFACE_CharListToUnits(this, Children); }
		}
		TextComponent_unit I_textUnit<TextUnit_character>.FirstChild {
			get {
				return Utility.INTERFACE_CharToUnit(this, FirstChild);
			}
		}
		TextComponent_unit I_textUnit<TextUnit_character>.LastChild {
			get {
				return Utility.INTERFACE_CharToUnit(this, LastChild);
			}
		}
		TextComponent_unit I_textUnit<TextUnit_character>.ChildByIndex (int index) {
			return Utility.INTERFACE_CharToUnit(this, ChildByIndex(index));
		}

		TextComponent_unit I_textUnit<TextUnit_character>.NewChild {
			get {
				return Utility.INTERFACE_CharToUnit(this, NewChild);
			}
		}

		MauronCode_dataIndex<TextComponent_unit> I_textUnit<TextUnit_character>.Neighbors {
			get { return Utility.INTERFACE_CovertToUnitIndex<TextUnit_character>(Neighbors); }
		}

		TextUnit_character I_textUnit<TextUnit_character>.FirstCharacter {
			get { return FirstCharacter; }
		}
		TextUnit_character I_textUnit<TextUnit_character>.LastCharacter {
			get { return LastCharacter; }
		}

		bool IEquatable<TextUnit_character>.Equals (TextUnit_character other) {
			return Equals(other);
		}

		#endregion

	}

	//Description of the codeType
	public sealed class TextUnitType_character : TextUnitType {
		#region Singleton
		private static volatile TextUnitType_character instance=new TextUnitType_character();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextUnitType_character ( ) { }
		public static TextUnitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextUnitType_character();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "character"; }
		}
	}

}
