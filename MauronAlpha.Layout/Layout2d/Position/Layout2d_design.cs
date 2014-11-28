using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Position {


	public class Layout2d_design:Layout2d_component {
		
		//constructor
		public Layout2d_design (Layout2d_window window) { 
			LAYOUT_window = window;
			Apply();
		}

		public virtual Layout2d_design Apply(){
			return this;
		}

		//The source anchor we are using
		private Layout2d_window LAYOUT_window;

	}
}
