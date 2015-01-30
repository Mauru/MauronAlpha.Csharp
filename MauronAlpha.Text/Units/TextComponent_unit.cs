using MauronAlpha.Text.Context;
using MauronAlpha.Text.Utility;

namespace MauronAlpha.Text.Units {
	
	//Represents a unit of text
	public abstract class TextComponent_unit:MauronCode_textComponent {
		
		//constructor
		public TextComponent_unit(TextUnitType type):base(){
			SUB_unitType=type;
		}

		private TextUnitType SUB_unitType;
		public TextUnitType UnitType {
			get {
				if( SUB_unitType==null ) {
					NullError("TextUnitType can not be null!,(TextUnitType)",this,typeof(TextUnitType));
				}
				return SUB_unitType;
			}			
		}

		#region Utility and Comparers
		
		protected TextUtility_text UTILITY_text;
		public TextUtility_text Utility {
			get {
				return UTILITY_text;
			}
		}
		protected TextUtility_encoding UTILITY_encoding;
		public TextUtility_encoding Encoding {
			get {
				if(UTILITY_encoding == null)
					throw NullError("Encoding can not be null!,(Encoding)",this,typeof(TextUtility_encoding));
				return UTILITY_encoding;
			}
		}
		protected TextComponent_unit SETUP_Utility(TextUtility_text utility, TextUtility_encoding encoding){
			UTILITY_text = utility;
			UTILITY_encoding = encoding;
			return this;			
		}
		protected TextComponent_unit SETUP_Utility (TextComponent_unit unit) {
			UTILITY_text = unit.Utility;
			UTILITY_encoding = unit.Encoding;
			return this;
		}

		#endregion

		#region Common Shared Functions in Implemntors

		#region Booleans
		protected bool B_isReadOnly=false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}

		public bool HasLimit {
			get {
				return Limit>=0;
			}
		}
		public bool HasReachedLimit {
			get {
				return INT_limit>=0&&ChildCount>=INT_limit;
			}
		}
		public bool CanHaveChildren {
			get {
				return UnitType.CanHaveChildren;
			}
		}
		public bool CanHaveParent {
			get {
				return UnitType.CanHaveParent;
			}
		}

		#endregion

		#region Integers
		protected int INT_limit=-1;
		public int Limit {
			get { return INT_limit; }
		}
		#endregion

		
		protected TextContext CTX_context;
		public TextContext Context {
			get {
				return CTX_context;
			}
		}

		#endregion

		#region Abstracts that need to be implemented

		public abstract bool IsFirstChild { get; }
		public abstract bool IsLastChild { get;	}

		public abstract int Index { get; }

		public abstract int ChildCount { get; }

		public abstract TextUnit_character FirstCharacter { get; }
		public abstract TextUnit_character LastCharacter { get; }
	
		#endregion
	}
}