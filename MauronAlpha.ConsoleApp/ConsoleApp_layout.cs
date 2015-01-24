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

		public ConsoleApp_layout():base() {}

		private static string[] KEYS_regions = new string[4]{"header","content","input","footer"};
		private MauronCode_dataTree<string,Layout2d_container> TREE_regions = new MauronCode_dataTree<string,Layout2d_container>(KEYS_regions);

		//Apply the design
		public ConsoleApp_layout ApplyTo(I_layoutUnit unit) {
			Layout2d_context context = unit.Context;

			if( context == null )
				throw NullError( "Context can not be null!,(ApplyTo)", this, typeof( Layout2d_context ) );

			Layout2d_size size = context.Size;
			return this;
		}
	}
}