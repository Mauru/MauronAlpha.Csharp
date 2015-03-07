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
			
			for(int index=0; index<ChildCount; index++) {
				I_textUnit unit = DATA_children.Value(index);
				unit.Context.SetIsReadOnly(false).SetValue(unit.UnitType,index);
				unit.UpdateContextFromParent( chainChildren );
			}
			return this;
		}

		public I_textUnit HandleLooseEnds() {
			if( IsReadOnly )
				throw Error("Is protected!,(HandleLooseEnds)", this, ErrorType_protected.Instance);
			
			int index = EndIndex;

			if(index > -1)
				return this;
			
			MauronCode_dataList<I_textUnit> neighbors = Neighbors.Right;
			if(neighbors.IsEmpty)
				return this;
			
			I_textUnit nextUnit = neighbors.FirstElement;

			MauronCode_dataList<I_textUnit> moveMe=new MauronCode_dataList<I_textUnit>();
			int otherEnd = nextUnit.EndIndex;
			if(otherEnd > -1)
				moveMe = nextUnit.Children.Range(0,otherEnd);
			else
				moveMe = nextUnit.Children;

			foreach(I_textUnit unit in moveMe) {
				nextUnit.RemoveChildAtIndex(unit.Index, false);
				InsertChildAtIndex(ChildCount, unit, false);
			}

			nextUnit.UpdateChildContext(true);
			nextUnit.HandleLooseEnds();

			if(EndIndex<0)
				return HandleLooseEnds();

			return this;
		}
		
		public I_textUnit HandleEnds(){
			if(IsReadOnly)
				throw Error("Is protected!,(HandleEndAtIndex)",this,ErrorType_protected.Instance);
			
			int index = EndIndex;
			if(index<0)
				return this;
			if(index==ChildCount-1)
				return this;

			//We actually need to move stuff
			I_textUnit nextUnit;
			TextUnitNeighbors neighbors = Neighbors;

			//create new neighbor if necessary
			if(neighbors.Right.IsEmpty) {
				nextUnit = UnitType.New;
				Parent.InsertChildAtIndex(Index+1, nextUnit, false);
			} else
				nextUnit = neighbors.Right.FirstElement;

			MauronCode_dataList<I_textUnit> moveMe = DATA_children.Range(index+1);
			DATA_children.RemoveByRange(index+1,ChildCount-1);
			foreach(I_textUnit unit in moveMe){
				nextUnit.InsertChildAtIndex(0, unit, true);
			}

			if(EndIndex > -1)
				return HandleEnds();

			return this;
		}

		public I_textUnit InsertChildAtIndex (int n, I_textUnit unit, bool reIndex)  {
			if(IsReadOnly)
				throw Error("Is protected!,(InsertChildAtIndex)",this, ErrorType_protected.Instance);
			if( n < 0|| n > ChildCount )
				throw Error("Index out of bounds!,{"+n+"},(InsertChildAtIndex)", this, ErrorType_bounds.Instance);
			if (!CanHaveChildren || !unit.UnitType.Equals (UnitType.ChildType))
				throw Error ("Invalid child!,{"+unit.UnitType.Name+"},(InsertChildAtIndex)", this, ErrorType_scope.Instance);

			DATA_children.InsertValueAt(n, unit);
			unit.SetParent(this);
			unit.UpdateContextFromParent(true);
			unit.Context.SetIsReadOnly(false).SetValue(unit.UnitType,n);

			if( reIndex ) {

				if(n<ChildCount-1) {
					MauronCode_dataList<I_textUnit> needUpdate = DATA_children.Range(n+1);
					foreach(I_textUnit updateMe in needUpdate)
						updateMe.Context.AddValue(updateMe.UnitType, 1);
				}
						
				if(unit.EndsParent)
					HandleEnds();

			}

			return this;
		}
		public I_textUnit RemoveChildAtIndex (int n, bool reIndex) { 
			if(IsReadOnly)
				throw Error("Is protected!,(RemoveChildAtIndex)",this,ErrorType_protected.Instance);
			if(n<0||n>=ChildCount)
				throw Error("Invalid index!,{"+n+"},(RemoveChildAtIndex)",this,ErrorType_index.Instance);

			DATA_children.RemoveByKey(n);

			if(reIndex) {
				
				if(n<ChildCount-1){
					MauronCode_dataList<I_textUnit> needUpdate = DATA_children.Range(n);
					foreach( I_textUnit updateMe in needUpdate )
						updateMe.Context.AddValue(updateMe.UnitType, -1);
				}

				if(!DATA_children.LastElement.EndsParent)
					Parent.HandleLooseEnds();

			}

			return this; 
		}
		public I_textUnit SetParent (I_textUnit unit) { 
			if (IsReadOnly)
				throw Error ("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			if (!unit.UnitType.Equals (UnitType.ParentType))
				throw Error ("Invalid parent!,{"+unit.UnitType.Name+"},(SetParent)", this, ErrorType_scope.Instance);
			if(!CanHaveParent)
				throw Error("Unit can not have a parent!,(SetParent)",this,ErrorType_scope.Instance);

			UNIT_parent = unit;

			return this; 
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
				string result = "";
				foreach(I_textUnit unit in Children)
					result += unit.AsString;
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
		public int EndIndex {
			get {
				if(IsEmpty)
					return -1;
				for(int index=0; index<ChildCount; index++){
					I_textUnit unit = DATA_children.Value(index);
					if(unit.EndsParent)
						return index;
				}
				return -1;
			}
		}

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
