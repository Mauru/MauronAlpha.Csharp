using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Collections {
	
	public class Layout2d_queryResult {
		
		public I_layoutUnit Source;

		public string QueryAsString;

		public Layout2d_unitCollection ResultSet;

		public I_layoutUnit BestResult {
			get {
				if( ResultSet == null )
					return new Layout2d_delayedUnit( this );
				
			}
		}
	}

}
