
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Interfaces;

namespace MauronAlpha.Text.Collections {
	
	//A list of Neighbors
	public class TextUnitNeighbors:MauronCode_textComponent,
	I_textUnitCollection {

		//Constructor
		public TextUnitNeighbors():base() {}
		
		public MauronCode_dataList<I_textUnit> Left { 
			get {
				return new MauronCode_dataList<I_textUnit>();
			}
		}
		public MauronCode_dataList<I_textUnit> Right { 
			get {
				return new MauronCode_dataList<I_textUnit>();
			}
		}

		public MauronCode_dataList<I_textUnit> All {
			get {
				
			}
		}
	}

}
