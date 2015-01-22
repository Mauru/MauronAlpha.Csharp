using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Forms.Units;

using MauronAlpha.HandlingData;

namespace MauronAlpha.ConsoleApp {

	//Sets up the layout
	public class ConsoleApp_layout:Layout2d_design {

		//constructor

		public ConsoleApp_layout( Layout2d_window window ) : base( window ) {}

		private static string[] KEYS_regions = new string[4]{"header","content","input","footer"};
		private MauronCode_dataTree<string,Layout2d_container> TREE_regions = new MauronCode_dataTree<string,Layout2d_container>(KEYS_regions);

		//Apply the design
		public override Layout2d_design Apply ( ) {
			Layout2d_window window = base.LAYOUT_window;
			Layout2d_context context = window.Context;

			Layout2d_size windowSize = context.Size;

			Layout2d_container header = new Layout2d_container();

			
			return this;
		}
	}
}