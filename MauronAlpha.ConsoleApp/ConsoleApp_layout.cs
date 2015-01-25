using MauronAlpha.HandlingData;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;

using MauronAlpha.Forms.Units;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.ConsoleApp.Interfaces;

namespace MauronAlpha.ConsoleApp {

	//Sets up the layout
	public class ConsoleApp_layout:Layout2d_design,
	I_consoleLayout {

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

			ConsoleLayout_header header = new ConsoleLayout_header( new Vector2d(), new Vector2d(size.Width, 1) );
			
			unit.AddChildAtIndex( unit.NextChildIndex,header, true );
			return this;
		}
	}

	public class ConsoleLayout_header : Layout2d_unit,
	I_layoutUnit {
		
		//constructor
		public ConsoleLayout_header():base( UnitType_container.Instance ) {}
		public ConsoleLayout_header( Vector2d position, Vector2d size ) : base(UnitType_container.Instance) { 
			Layout2d_context context = new Layout2d_context(position,size);
			base.SetContext( context );
		}

	}
}