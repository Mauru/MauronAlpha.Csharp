
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Interfaces;

namespace MauronAlpha.Text.Collections {
	
	//A list of Neighbors
	public class TextUnitNeighbors:MauronCode_textComponent,
	I_textUnitCollection {

		//Constructor
		public TextUnitNeighbors():base() {}

		private I_textUnit UNIT_self;
		public I_textUnit Self { 
			get {
				return UNIT_self;
			}
		}
		
		private MauronCode_dataList<I_textUnit> UNITS_left;
		public MauronCode_dataList<I_textUnit> Left { 
			get {
				if(UNITS_left==null) {}

			}
		}
		private MauronCode_dataList<I_textUnit> UNITS_right;
		public MauronCode_dataList<I_textUnit> Right { 
			get {
				if(UNITS_right==null) {}
			}
		}

		public MauronCode_dataList<I_textUnit> All {
			get {
				return Left.Instance.Join(Self).Join(Right);
			}
		}
	}

}
