using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Position {


	public class Layout2d_design:Layout2d_component {
		
		//constructor
		public Layout2d_design (Layout2d_window window) { 
			LAYOUT_window = window;
		}

		public virtual Layout2d_design Apply(){
			return this;
		}

		//The source anchor we are using
		protected Layout2d_window LAYOUT_window;
		public Layout2d_window Window {
			get {
				if( LAYOUT_window == null )
					throw NullError( "LAYOUT_window can not be null!,(Window)", this, typeof( Layout2d_window ) );
				return LAYOUT_window;
			}
		}

	}
}
