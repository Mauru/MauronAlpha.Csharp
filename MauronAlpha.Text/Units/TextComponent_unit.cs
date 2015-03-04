using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text;
using MauronAlpha.Text.Collections;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Encoding;

namespace MauronAlpha.Text.Units {

	public abstract class TextComponent_unit : MauronCode_textComponent,
	I_textUnit {

		//Constructor
		public TextComponent_unit(TextUnitType unitType):base() {
			SUB_unitType = unitType;
			DATA_children=new MauronCode_dataList<I_textUnit>();
		}

		//Unit Type
		private I_textUnitType SUB_unitType;
		public I_textUnitType UnitType {
			get { 
				if(SUB_unitType == null)
					throw NullError("TextUnitType can not be null!,(UnitType)",this, typeof(TextUnitType));
				return SUB_unitType;
			}
		}

		//Booleans
		public virtual bool Equals (I_textUnit other) {
			if(Id.Equals(other.Id))
				return true;
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
		public bool IsParent {
			get {
				if( !CanHaveChildren )
					return false;
				return (Children.Count>0);
			}
		}
		public bool IsFull {
			get { 
				return !IsEmpty && Children.LastElement.EndsParent;
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
		public virtual bool EndsParent {
			get {
				return IsChild && Encoding.UnitEndsOther(this,Parent);
			}
		}
		public virtual bool IsEmpty {
			get {
				if( DATA_children.Count<1 )
					return true;
				foreach( I_textUnit child in Children )
					if( !child.IsEmpty )
						return false;
				return true;
			}
		}

		//Methods
		public I_textUnit SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}
		public I_textUnit Clear() {
			DATA_children = new MauronCode_dataList<I_textUnit>();
			return this;
		}
		public I_textUnit SetContext(TextContext context) {
			if(IsReadOnly)
				throw Error("Is protected!,(SetContext)",this,ErrorType_protected.Instance);
			DATA_context = context;
			return this;
		}
		
		public I_textUnit UpdateContextFromParent( bool updateChildren ) {
			if(IsReadOnly)
				throw Error("Is protected!,(UpdateContextFromParent)",this,ErrorType_protected.Instance);

			if( IsChild )
				DATA_context.InheritValues( this.UnitType, Parent ); 
				
			return this;
		}
		public I_textUnit UpdateChildContext( bool chainChildren ) {
			if(IsReadOnly)
				throw Error("Is protected!,(UpdateChildContext)",this,ErrorType_protected.Instance);
			foreach(I_textUnit unit in DATA_children)
				unit.UpdateContextFromParent( chainChildren );
			return this;
		}
		
		public I_textUnit HandleEndAtIndex( int index ){
			if(IsReadOnly)
				throw Error("Is protected!,(HandleEndAtIndex)",this,ErrorType_protected.Instance);
			if(index<0 || index >= ChildCount)
				throw Error("Index out of bounds!,{"+index+"},(HandleEndAtIndex)",this,ErrorType_index.Instance);
			if(index==ChildCount-1)
				return this;

			MauronCode_dataList<I_textUnit> children = Children.Range(index);

			I_textUnit nextUnit;

			TextUnitNeighbors neighbors = Neighbors;

			//create new neighbor if necessary
			if(neighbors.Right.IsEmpty) {
				nextUnit = UnitType.New;
				Parent.InsertChildAtIndex(Index+1,nextUnit,false,true);
			} else
				nextUnit = neighbors.Right.FirstElement;
			
			//Move units
			foreach(I_textUnit child in children) {
				nextUnit.InsertChildAtIndex(nextUnit.ChildCount,child, true, true);
			}

			return this;
		}
		public I_textUnit RemoveFromParent( bool updateChildContext ) {
			if( IsReadOnly )
				throw Error("Is protected!,(RemoveFromParent)", this, ErrorType_protected.Instance);
			
			if( !IsChild )
				return this;

			Parent.RemoveChildAtIndex(Index, false, true );
			UNIT_parent = null;
			Context.SetValue(UnitType.ParentType,0);
			
			if( !updateChildContext )
				return this;

			foreach(I_textUnit unit in DATA_children)
				unit.UpdateContextFromParent(true);

			return this;

		}

		public I_textUnit InsertUnitAtIndex (int n, I_textUnit unit, bool updateOrigin, bool updateChildren) {
			if(IsReadOnly)
				throw Error("Is protected!,(InsertUnitAtIndex)",this,ErrorType_protected.Instance);
			if(n<0||n>ChildCount)
				throw Error("Index out of bounds!,{"+n+"},(InsertUnitAtIndex)",this,ErrorType_bounds.Instance);

			if( updateOrigin && unit.IsChild )
				unit.RemoveFromParent( false );
			


			

		}
		public I_textUnit InsertChildAtIndex (int n, I_textUnit unit, bool updateOrigin, bool updateChildren) {
			if(IsReadOnly)
				throw Error("Is protected!,(InsertChildAtIndex)",this, ErrorType_protected.Instance);
			if( n < 0|| n > ChildCount )
				throw Error("Index out of bounds!,{"+n+"},(InsertChildAtIndex)", this, ErrorType_bounds.Instance);

			//remove from old parent
			if( updateOrigin 
				&& unit.IsChild 
				&& !unit.Parent.Equals( this ) 
			) {
				unit.Parent.RemoveChildAtIndex( unit.Index, false, true );
				bool displace =  Encoding.UnitEndsOther( unit, unit.Parent );
				
				//This is were things might get complicated
				if(displace) {
				
				}
			}

			//update Children and their context
			if( updateChildren ) {
				int childCount = ChildCount;

				//check if the unit terminates the new parent
				bool displace =  Encoding.UnitEndsOther( unit, this );

				//We are actually inserting the unit and need to update the context
				if( childCount > 1 && n < childCount -1 ) {
					MauronCode_dataList<I_textUnit> result = DATA_children.Range( n );
					int index = 0;
					foreach( I_textUnit child in result ) {

						//Displace unit to next logical item
						if( displace ) {
							//remove
							RemoveChildAtIndex( n, false, false );
							
							//figure out neighbor
							TextUnitNeighbors neighbors = Neighbors;
							I_textUnit neighbor;

							//no adequate neighbor
							if( neighbors.Right.IsEmpty ) {
								neighbor = UnitType.New;
								neighbor.SetContext(Context.Instance.AddValue(this.UnitType,1));
							} else
								neighbor = neighbors.Right.FirstElement;
							
							//1: add each unit to new neighbor
							neighbor.InsertChildAtIndex( index, child, false, true );
							index++;
							neighbor.UpdateContextFromParent( true );

						} else {
							//Child get shifted into direction
							child.Context.AddValue( child.UnitType, 1 );
							child.UpdateChildContext( true );
						}
					}
				}

			}

			return this;
		}
		public I_textUnit RemoveChildAtIndex (int n, bool updateOrigin, bool updateChildren) { return this; }
		public I_textUnit SetParent (I_textUnit unit, bool updateOrigin, bool updateChildren) { return this; }

		public I_textUnit AppendString (string text) {
			if(IsReadOnly)
				throw Error("Is protected!,(AppendString)",this,ErrorType_protected.Instance);

			TextUnit_text newText=Encoding.StringAsTextUnit(text);
			if( newText.IsEmpty )
				return this;

			//UnitType is Text
			if( UnitType.Equals(TextUnitType_text.Instance) ) {
				foreach( I_textUnit child in newText.Children )
					InsertChildAtIndex(ChildCount, child, false, true);
				return this;
			}

			//Paragraph
			if( UnitType.Equals(TextUnitType_paragraph.Instance) ) {

				//the unit is allready full
				if( IsFull ) {

					TextUnitNeighbors neighbors = Neighbors;
					I_textUnit nextUnit;

					//create neighbor
					if( neighbors.Right.IsEmpty ) {
						nextUnit = UnitType.New;
						Parent.InsertChildAtIndex(Index+1, nextUnit, false, true);
					}
					else
						nextUnit=neighbors.Right.FirstElement;

					return nextUnit.InsertUnitAtIndex(0, newText, true, true);

				}

				MauronCode_dataList<I_textUnit> insertMe = newText.Children.SetIsReadOnly(false);
				while(  newText.ChildCount > 0 ) {
					I_textUnit fragment = newText.Shift;
				}


				return this;
			}

			//Line
			if( UnitType.Equals(TextUnitType_line.Instance) ) {

				//The unit is allready full
				if( IsFull ) {



				}

			}

		}

		//Child modifiers:return
		public I_textUnit Pop {
			get {
				if( IsReadOnly )
					throw Error("Is protected!,(Pop)",this,ErrorType_protected.Instance);
				if( !IsParent )
					throw Error("No children!,(Pop)",this,ErrorType_index.Instance);
				return DATA_children.Pop;
			}
		}
		public I_textUnit Shift {
			get {
				if( IsReadOnly )
					throw Error("Is protected!,(Shift)", this, ErrorType_protected.Instance);
				if( !IsParent )
					throw Error("No children!,(Shift)", this, ErrorType_index.Instance);
				return DATA_children.Shift;
			}
		}
		
		//Context
		protected TextContext DATA_context;
		public virtual TextContext Context {
			get { 
				if(DATA_context == null)
					DATA_context = new TextContext();
				return DATA_context.SetIsReadOnly(true);	
			 }
		}
		public abstract TextContext CountAsContext { get; }

		//As String
		public virtual string AsString { 
			get {
				string result="";
				foreach(I_textUnit unit in Children)
					result+= unit.AsString;
				return result; 
			}
		}

		//Int
		public virtual int ChildCount { 
			get {
				if(!CanHaveChildren)
					return 0;
				return Children.Count;
			}
		}
		public abstract int Index { get; }

		//Characters
		public virtual TextUnit_character FirstCharacter {
			get {
				if(IsEmpty||!IsParent)
					return TextUnit_character.Empty;
				return Children.FirstElement.FirstCharacter;
			}
		}
		public virtual TextUnit_character LastCharacter {
			get {
				if( IsEmpty||!IsParent )
					return TextUnit_character.Empty;
				return Children.LastElement.LastCharacter;
			}
		}

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
		public TextUnitNeighbors Neighbors {
			get {
				TextUnitNeighbors result= new TextUnitNeighbors(this);
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
