using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Forms.Units;

using MauronAlpha.HandlingData;

namespace MauronAlpha.ConsoleApp {

	//Sets up the layout
	public class ConsoleApp_layout:Layout2d_design {

		//constructor
		public ConsoleApp_layout(Layout2d_window window):base(window) {
			LAYOUT_window=window;
		}

		private I_layoutParent LAYOUT_window;

		private static string[] KEYS_regions = new string[]{"header","content","input","footer"};
		private MauronCode_dataTree<string,Layout2d_container> TREE_regions = new MauronCode_dataTree<string,Layout2d_container>(KEYS_regions);

		//Apply the design
		public override Layout2d_design Apply ( ) {
			if(!TREE_regions.IsSet("header")) {
				Layout2d_container header=new Layout2d_container(LAYOUT_window);
				FormUnit_textField title = new FormUnit_textField(header);
			}
			return this;
		}
	}
}
