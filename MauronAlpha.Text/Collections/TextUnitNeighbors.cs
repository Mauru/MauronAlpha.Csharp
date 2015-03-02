using System;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Interfaces;

namespace MauronAlpha.Text.Collections {
	
	//A list of Neighbors
	public class TextUnitNeighbors:MauronCode_textComponent,
	I_textUnitCollection,
	I_protectable<TextUnitNeighbors>,
	I_instantiable<TextUnitNeighbors>,
	IEquatable<TextUnitNeighbors> {

		//Constructor
		public TextUnitNeighbors():base() {}
		public TextUnitNeighbors( I_textUnit unit_self ) : this() { 
			UNIT_self = unit_self;
		}
		public TextUnitNeighbors( TextUnitNeighbors other ) : this() {
			if(other.IsInitialized) {
				UNIT_self = other.Self;
				UNITS_left = other.Left;
				UNITS_right = other.Right;
			}				
		}
		
		//Reference anchor
		private I_textUnit UNIT_self;
		public I_textUnit Self { 
			get {
				if(UNIT_self == null)
					throw NullError("UNIT_self can not be null!,(Self)",this,typeof(I_textUnit));
				return UNIT_self;
			}
		}

		//Neighbors
		private MauronCode_dataList<I_textUnit> UNITS_left;
		public MauronCode_dataList<I_textUnit> Left {
			get {
				if( UNITS_left==null ) {
					if( UNIT_self==null||!UNIT_self.IsChild ) {
						UNITS_left=new MauronCode_dataList<I_textUnit>();
						return UNITS_left;
					}

					int index=UNIT_self.Index;
					I_textUnit parent=UNIT_self.Parent;
					MauronCode_dataList<I_textUnit> children=parent.Children;

					if( index>children.Count )
						index=children.Count-1;

					if( index <= 0 ) {
						UNITS_left=new MauronCode_dataList<I_textUnit>();
						return UNITS_left;
					}

					UNITS_left=children.Range(0, index-1);
				}
				return UNITS_left;
			}
		}
		private MauronCode_dataList<I_textUnit> UNITS_right;
		public MauronCode_dataList<I_textUnit> Right {
			get {
				if( UNITS_right==null ) {
					if( UNIT_self==null||!UNIT_self.IsChild ) {
						UNITS_right=new MauronCode_dataList<I_textUnit>();
						return UNITS_right;
					}

					int index=UNIT_self.Index;
					I_textUnit parent=UNIT_self.Parent;
					MauronCode_dataList<I_textUnit> children=parent.Children;

					if( index >= children.Count ) {
						UNITS_right=new MauronCode_dataList<I_textUnit>();
						return UNITS_right;
					}

					if(index < 0)
						index = 0;
				
					UNITS_right=children.Range(index);
				}
				return UNITS_right;
			}
		}
		public MauronCode_dataList<I_textUnit> All {
			get {
				return Left.Instance.Add(Self).Join(Right);
			}
		}
	
		//Booleans
		public bool Equals(TextUnitNeighbors other) {
			if(!IsInitialized || !other.IsInitialized)
				throw Error("Compared units are not initialized!",this, ErrorType_nullError.Instance);

			return UNIT_self.Equals(other.Self);
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly { 
			get {
				return B_isReadOnly;
			}
		}
		public bool IsInitialized {
			get {
				return UNIT_self == null;
			}
		}

		//Methods
		public TextUnitNeighbors SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}
		public TextUnitNeighbors Instance { 
			get {
				return new TextUnitNeighbors(this);
			}
		}
	}

}
