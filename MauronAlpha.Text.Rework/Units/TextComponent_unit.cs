using System;

using MauronAlpha.Text;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;

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
			throw new NotImplementedException();
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		//Methods
		public I_textUnit SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}

		private TextContext DATA_context;
		public TextContext Context {
			get { 
				if(DATA_context == null)
					DATA_context = new TextContext();
				return DATA_context;	
			 }
		}

		public abstract string AsString { get; }

		public abstract bool IsEmpty { get; }

		public bool IsParent {
			get { 
				if(!CanHaveChildren)
					return false;
				return (Children.Count>0);					
			}
		}
		public bool IsChild {
			get { 
				if(!CanHaveParent)
					return false;
				return (UNIT_parent == null);
			}
		}
		public bool CanHaveChildren {
			get { return SUB_unitType.CanHaveChildren; }
		}
		public bool CanHaveParent {
			get { return SUB_unitType.CanHaveParent; }
		}

		public System.Collections.Generic.ICollection<I_textUnit> Children {
			get { throw new NotImplementedException(); }
		}

		public System.Collections.Generic.ICollection<I_textUnit> Neighbors {
			get { throw new NotImplementedException(); }
		}

		public I_textUnit Parent {
			get { throw new NotImplementedException(); }
		}

		public I_textEncoding Encoding {
			get { throw new NotImplementedException(); }
		}
	}
}
