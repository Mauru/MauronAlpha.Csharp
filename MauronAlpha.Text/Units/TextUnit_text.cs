using System;

using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Utility;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	//A complete text
	public class TextUnit_text : TextComponent_unit, I_textUnit<TextUnit_text> {

		//constructor
		public TextUnit_text(TextUtility_text utility, TextUtility_encoding encoding):base(TextUnitType_text.Instance){
			UTILITY_text = utility;
			UTILITY_encoding = encoding;
			CTX_context = TextContext.Start;
		}
		public TextUnit_text (TextUtility_text utility, TextUtility_encoding encoding, MauronCode_dataList<TextUnit_paragraph> children)
			: this(utility,encoding) {
			DATA_children = children.Instance;			
		}

		//Data
		private MauronCode_dataList<TextUnit_paragraph> DATA_children = new MauronCode_dataList<TextUnit_paragraph>();
		
		#region implementing I_textUnit
		
		//As String
		public string AsString {
			get {
				if(IsEmpty){
					return null;
				}
				string result="";

				foreach(TextUnit_paragraph unit in DATA_children) {
					if(!unit.IsEmpty){
						result+=unit.AsString;
					}
				}

				return result;
			}
		}
		//!Inherited(base) UnitType

		#region Boolean checks

		public bool IsEmpty {
			get {
				if(DATA_children.Count==0)
					return true;
				if(FirstChild.IsEmpty)
					return true;
				
				return false;
			}
		}
		//!Inherited(base) IsReadOnly
		public TextUnit_text SetIsReadOnly(bool b_isReadOnly) {
			B_isReadOnly=b_isReadOnly;
			return this;
		}
		public override bool IsFirstChild {
			get { return true; }
		}
		public override bool IsLastChild {
			get { return true; }
		}
		
		public bool HasMultiWord {
			get {
				foreach( TextUnit_paragraph unit in Children ) {
					if( unit.HasMultiWord ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasWhiteSpace {
			get {
				foreach( TextUnit_paragraph unit in Children ) {
					if( unit.HasWhiteSpace ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach(TextUnit_paragraph unit in Children){
					if(unit.HasLineBreak) {
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
		public TextUnit_text Instance {
			get {
				return new TextUnit_text(Utility,Encoding,DATA_children);
			}
		}
		public TextUnit_text Parent {
			get {
				return this;
			}
		}
		public TextUnit_text Source {
			get {
				return this;
			}
		}
		//!Inherited Source
		#endregion

		#region Children
		public MauronCode_dataList<TextUnit_paragraph> Children {
			get {
				return DATA_children.Instance.SetIsReadOnly(true);
			}
		}
		public TextUnit_paragraph FirstChild {
			get {
				if(ChildCount<1){
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.FirstElement;
			}
		} 
		public TextUnit_paragraph LastChild {
			get {
				if(ChildCount<1){
					Exception("No Children!",this,ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.LastElement;
			}
		}
		public TextUnit_paragraph NewChild {
			get {
				if(IsReadOnly) {
					throw Error("Is Protected!,(NewChild)", this, ErrorType_protected.Instance);
				}
				if(HasLimit && HasReachedLimit) {
					throw Error("ChildLimit reached!,{"+Limit+"},(NewChild)", this, ErrorType_limit.Instance);
				}
				TextUnit_paragraph unit = new TextUnit_paragraph(this,new TextContext(ChildCount));
				DATA_children.Add(unit);
				return unit;
			}
		}
		public TextUnit_paragraph ChildByIndex (int index) {
			if( index<0 ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			if( index>=ChildCount ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			return DATA_children.Value(index);
		}
		#endregion

		public MauronCode_dataIndex<TextUnit_text> Neighbors {
			get {
				return new MauronCode_dataIndex<TextUnit_text>().SetIsReadOnly(true);
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

		string I_textUnit<TextUnit_text>.AsString {
			get { return AsString; }
		}

		bool I_textUnit<TextUnit_text>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textUnit<TextUnit_text>.IsReadOnly {
			get { return IsReadOnly; }
		}
		bool I_textUnit<TextUnit_text>.IsLastChild {
			get { return IsLastChild; }
		}
		bool I_textUnit<TextUnit_text>.IsFirstChild {
			get {
				return IsFirstChild;
			}
		}

		bool I_textUnit<TextUnit_text>.HasMultiWord {
			get { return HasMultiWord; }
		}
		bool I_textUnit<TextUnit_text>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textUnit<TextUnit_text>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textUnit<TextUnit_text>.HasLimit {
			get { return HasLimit; }
		}
		bool I_textUnit<TextUnit_text>.HasReachedLimit {
			get {
				return HasReachedLimit;
			}
		}

		TextContext I_textUnit<TextUnit_text>.Context {
			get { return Context; }
		}

		int I_textUnit<TextUnit_text>.Index {
			get { return Index; }
		}
		int I_textUnit<TextUnit_text>.Limit {
			get { return Limit; }
		}
		int I_textUnit<TextUnit_text>.ChildCount {
			get { return ChildCount; }
		}

		I_textUnit<TextUnit_text> I_textUnit<TextUnit_text>.Instance {
			get { return Instance; }
		}
		TextComponent_unit I_textUnit<TextUnit_text>.Parent {
			get { return Parent; }
		}
		I_textUnit<TextUnit_text> I_textUnit<TextUnit_text>.Source {
			get { return Source; }
		}

		MauronCode_dataList<TextComponent_unit> I_textUnit<TextUnit_text>.Children {
			get { return Utility.INTERFACE_CovertToUnitList<TextUnit_paragraph>(Children); }
		}
		TextComponent_unit I_textUnit<TextUnit_text>.FirstChild {
			get {
				return FirstChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_text>.LastChild {
			get {
				return LastChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_text>.ChildByIndex (int index) {
			return ChildByIndex(index);
		}

		TextComponent_unit I_textUnit<TextUnit_text>.NewChild {
			get {
				return NewChild;
			}
		}

		MauronCode_dataIndex<TextComponent_unit> I_textUnit<TextUnit_text>.Neighbors {
			get { 
				return Utility.INTERFACE_CovertToUnitIndex<TextUnit_text>(Neighbors); 
			}
		}

		TextUnit_character I_textUnit<TextUnit_text>.FirstCharacter {
			get { return FirstCharacter; }
		}
		TextUnit_character I_textUnit<TextUnit_text>.LastCharacter {
			get { return LastCharacter; }
		}

		bool IEquatable<TextUnit_text>.Equals (TextUnit_text other) {
			return Equals(other);
		}

		#endregion
	}

	//Description of the codeType
	public sealed class TextUnitType_text : TextUnitType {
		#region Singleton
		private static volatile TextUnitType_text instance=new TextUnitType_text();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextUnitType_text ( ) { }
		public static TextUnitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextUnitType_text();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "text"; }
		}

		public override bool CanHaveParent {
			get {
				return false;
			}
		}
	}

}