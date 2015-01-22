using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Collections {
	
	//A basic collection of I_layoutUnit
	public class Layout2d_unitCollection : Layout2d_component, I_protectable {
		
		//constructor
		public Layout2d_unitCollection():base() {}

		#region I_protectable (implemented)
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		public Layout2d_unitCollection SetIsReadOnly(bool status){
			B_isReadOnly = status;
			return this;
		}
		#endregion
	}
}
