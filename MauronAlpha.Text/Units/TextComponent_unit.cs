using MauronAlpha.Text.Context;

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

		protected bool B_isReadOnly=false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}

		protected int INT_limit=-1;
		public int Limit {
			get { return INT_limit; }
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

		public abstract bool IsFirstChild { get; }
		public abstract bool IsLastChild { get;	}

		protected TextContext CTX_context;
		public TextContext Context {
			get  {
				return CTX_context;
			}
		}

		public abstract int Index { get; }

		public abstract int ChildCount { get; }

		public abstract TextUnit_character FirstCharacter { get; }
		public abstract TextUnit_character LastCharacter { get; }
	}
}