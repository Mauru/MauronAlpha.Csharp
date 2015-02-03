using System;

using MauronAlpha.HandlingData;

using MauronAlpha.Text;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Encoding;

namespace MauronAlpha.Text.Units {

	public abstract class TextComponent_unit : MauronCode_textComponent,
	I_textUnit {

		//Constructor
		public TextComponent_unit(TextUnitType unitType):base() {
			SUB_unitType = unitType;
		}

		//Unit Type
		private TextUnitType SUB_unitType;
		public TextUnitType UnitType {
			get { 
				if(SUB_unitType == null)
					throw NullError("TextUnitType can not be null!,(UnitType)",this, typeof(TextUnitType));
				return SUB_unitType;
			}
		}

		//Booleans
		public bool Equals (I_textUnit other) {
			if(!SUB_unitType.Equals(other.UnitType))
				return false;
			if(IsParent!=other.IsParent)
				return false;
			if(!Context.Equals(other.Context))
				return false;
			if(IsChild!=other.IsChild)
				return false;
			return DATA_children.Equals(other.Children);
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public abstract bool IsEmpty { get; }
		public bool IsParent {
			get {
				if( !CanHaveChildren )
					return false;
				return (Children.Count>0);
			}
		}
		public bool IsChild {
			get {
				if( !CanHaveParent )
					return false;
				return (UNIT_parent==null);
			}
		}
		public bool CanHaveChildren {
			get { return SUB_unitType.CanHaveChildren; }
		}
		public bool CanHaveParent {
			get { return SUB_unitType.CanHaveParent; }
		}

		//Methods
		public I_textUnit SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}

		//Context
		private TextContext DATA_context;
		public TextContext Context {
			get { 
				if(DATA_context == null)
					DATA_context = new TextContext();
				return DATA_context;	
			 }
		}

		//As String
		public abstract string AsString { get; }

		//Children
		protected MauronCode_dataList<I_textUnit> DATA_children;
		public MauronCode_dataList<I_textUnit> Children {
			get { 
				if(DATA_children == null)
					DATA_children = new MauronCode_dataList<I_textUnit>();
				return DATA_children;
			}
		}

		//Neighbors
		public MauronCode_dataIndex<I_textUnit> Neighbors {
			get {
				MauronCode_dataIndex<I_textUnit> result = new MauronCode_dataIndex<I_textUnit>().SetValue(0,this);
				if(!IsChild||!CanHaveParent)
					return result.SetIsReadOnly(true);

				MauronCode_dataList<I_textUnit> children = Parent.Children;
				int s = children.IndexOf(this);
				MauronCode_dataList<I_textUnit> left;
				if(s > 0) {
					left = children.Range(0, s-1);
					result.Prepend(left);
				}
				if(s < children.Count-1){
					left = children.Range(s+1, children.Count-1);
					result.Append(left);
				}
				return result;
			}
		}

		//Parent
		protected I_textUnit UNIT_parent;
		public I_textUnit Parent {
			get { 
				if(UNIT_parent == null)
					throw NullError("Parent is null!,(Parent)",this,typeof(TextComponent_unit));
				return UNIT_parent;
			}
		}

		protected I_textEncoding UTILITY_encoding;
		public I_textEncoding Encoding {
			get { 
				if(UTILITY_encoding==null)
					UTILITY_encoding = new TextEncoding_UTF8();
				return UTILITY_encoding;
			}
		}
	
	}
}
