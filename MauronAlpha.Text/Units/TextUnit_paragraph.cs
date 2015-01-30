using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Collections;

namespace MauronAlpha.Text.Units {

	//A Paragraph in a text
	public class TextUnit_paragraph : TextComponent_unit,
	I_textUnit,
	I_textUnit<TextUnit_paragraph> {

		//constructor
		public TextUnit_paragraph (TextUnit_text parent):base(TextUnitType_paragraph.Instance) {
			TXT_parent = parent;
		}
		public TextUnit_paragraph (TextUnit_text parent, TextContext context)
			: base(TextUnitType_paragraph.Instance) {
			TXT_parent=parent;
			CTX_context=context;
			SETUP_Utility(parent);
		}
		public TextUnit_paragraph (TextUnit_text parent, TextContext context, MauronCode_dataList<TextUnit_line> children)
			: this(parent, context) {
			DATA_children = children.Instance;			
		}

		//Children
		


		//Representation as string
		public string AsString {
			get { 
				if(IsEmpty){
					return null;
				}
				string result="";
				foreach(TextUnit_line line in DATA_children) {
					result+=line.AsString;
				}
				return result;
			 }
		}
		//!Inherited(base) UnitType

		//Booleans
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
		public TextUnit_paragraph SetIsReadOnly (bool b_isReadOnly) {
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
				foreach( TextUnit_line unit in Children ) {
					if( unit.HasMultiWord ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasWhiteSpace {
			get {
				foreach( TextUnit_line unit in Children ) {
					if( unit.HasWhiteSpace ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach( TextUnit_line unit in Children ) {
					if( unit.HasLineBreak ) {
						return true;
					}
				}
				return false;
			}
		}
		//!Inherited(base) HasLimit
		//!Inherited(base) HasReachedLimit

		//!Inherited(base) Context

		//Counters
		public override int Index {
			get { return 0; }
		}
		//!Inherited(base) Limit
		public override int ChildCount {
			get {
				return DATA_children.Count;
			}
		}

		//Parent (TextUnit_text)
		private TextUnit_text TXT_parent;
		public TextUnit_text Parent {
			get { return TXT_parent; }
		}
		public TextUnit_text Source {
			get { return TXT_parent; }
		}

		//Children (TextUnit_line)
		private MauronCode_dataList<TextUnit_line> DATA_children = new MauronCode_dataList<TextUnit_line>();
		public MauronCode_dataList<TextUnit_line> Children {
			get {
				return DATA_children.Instance.SetIsReadOnly(true);
			}
		}
		
		public TextUnit_line FirstChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.FirstElement;
			}
		}
		public TextUnit_line LastChild {
			get {
				if( ChildCount<1 ) {
					Exception("No Children!", this, ErrorResolution.Create);
					return NewChild;
				}
				return DATA_children.LastElement;
			}
		}
		public TextUnit_line NewChild {
			get {
				if( IsReadOnly ) {
					throw Error("Is Protected!,(NewChild)", this, ErrorType_protected.Instance);
				}
				if( HasLimit&&HasReachedLimit ) {
					throw Error("ChildLimit reached!,{"+Limit+"},(NewChild)", this, ErrorType_limit.Instance);
				}
				TextUnit_line unit=new TextUnit_line(this, new TextContext(Index, ChildCount));
				DATA_children.Add(unit);
				return unit;
			}
		}
		public TextUnit_line ChildByIndex (int index) {
			if( index<0 ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			if( index>=ChildCount ) {
				throw Error("Invalid Index!,{"+index+"},ChildByIndex", this, ErrorType_bounds.Instance);
			}
			return DATA_children.Value(index);
		}		
		
		//Neightbors
		public MauronCode_dataIndex<TextUnit_paragraph> Neighbors {
			get {
				MauronCode_dataIndex<TextUnit_paragraph> result = new MauronCode_dataIndex<TextUnit_paragraph>().SetIsReadOnly(true);
				result.SetValue(0,this);
				foreach(TextUnit_paragraph unit in Parent.Children) {
					if(unit.Index != Index) {
						int newIndex = unit.Index - Index;
						result.SetValue(newIndex,this);
					}
				}
				return result.SetIsReadOnly(true);
			}
		}

		//Methods
		public TextUnit_paragraph Instance {
			get {
				return new TextUnit_paragraph(Parent, Context, DATA_children);
			}
		}

	
		//Character Indexers
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

		#region I_textUnit, IEquatable<>, IEquatable<TextComponent_unit>

		string I_textUnit<TextUnit_paragraph>.AsString {
			get { return AsString; }
		}

		bool I_textUnit<TextUnit_paragraph>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textUnit<TextUnit_paragraph>.IsReadOnly {
			get { return IsReadOnly; }
		}
		bool I_textUnit<TextUnit_paragraph>.IsLastChild {
			get { return IsLastChild; }
		}
		bool I_textUnit<TextUnit_paragraph>.IsFirstChild {
			get {
				return IsFirstChild;
			}
		}

		bool I_textUnit<TextUnit_paragraph>.HasMultiWord {
			get { return HasMultiWord; }
		}
		bool I_textUnit<TextUnit_paragraph>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textUnit<TextUnit_paragraph>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textUnit<TextUnit_paragraph>.HasLimit {
			get { return HasLimit; }
		}
		bool I_textUnit<TextUnit_paragraph>.HasReachedLimit {
			get {
				return HasReachedLimit;
			}
		}

		TextContext I_textUnit<TextUnit_paragraph>.Context {
			get { return Context; }
		}

		int I_textUnit<TextUnit_paragraph>.Index {
			get { return Index; }
		}
		int I_textUnit<TextUnit_paragraph>.Limit {
			get { return Limit; }
		}
		int I_textUnit<TextUnit_paragraph>.ChildCount {
			get { return ChildCount; }
		}

		I_textUnit<TextUnit_paragraph> I_textUnit<TextUnit_paragraph>.Instance {
			get { return Instance; }
		}
		TextComponent_unit I_textUnit<TextUnit_paragraph>.Parent {
			get { return Parent; }
		}
		I_textUnit<TextUnit_text> I_textUnit<TextUnit_paragraph>.Source {
			get { return Source; }
		}

		MauronCode_dataList<TextComponent_unit> I_textUnit<TextUnit_paragraph>.Children {
			get { 
				return Utility.INTERFACE_CovertToUnitList<TextUnit_line>(Children); 
			}
		}
		TextComponent_unit I_textUnit<TextUnit_paragraph>.FirstChild {
			get {
				return FirstChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_paragraph>.LastChild {
			get {
				return LastChild;
			}
		}
		TextComponent_unit I_textUnit<TextUnit_paragraph>.ChildByIndex (int index) {
			return ChildByIndex(index);
		}

		TextComponent_unit I_textUnit<TextUnit_paragraph>.NewChild {
			get {
				return NewChild;
			}
		}

		MauronCode_dataIndex<TextComponent_unit> I_textUnit<TextUnit_paragraph>.Neighbors {
			get { 
				return Utility.INTERFACE_CovertToUnitIndex<TextUnit_paragraph>(Neighbors);
			}
		}

		TextUnit_character I_textUnit<TextUnit_paragraph>.FirstCharacter {
			get { return FirstCharacter; }
		}
		TextUnit_character I_textUnit<TextUnit_paragraph>.LastCharacter {
			get { return LastCharacter; }
		}

		bool IEquatable<TextUnit_paragraph>.Equals (TextUnit_paragraph other) {
			return Equals(other);
		}

		#endregion
	
	}

	//Description of the codeType
	public sealed class TextUnitType_paragraph : TextUnitType {
		#region Singleton
		private static volatile TextUnitType_paragraph instance=new TextUnitType_paragraph();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextUnitType_paragraph ( ) { }
		public static TextUnitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextUnitType_paragraph();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "paragraph"; }
		}
	}

}
