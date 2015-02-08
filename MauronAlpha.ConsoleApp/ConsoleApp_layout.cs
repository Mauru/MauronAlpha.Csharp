using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

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
		public ConsoleApp_layout():base(  ) {}
		public ConsoleApp_layout ( I_layoutController console ) : base() { 
			UNIT_console = console;
		}

		//The regions
		private static string[] KEYS_regions = new string[3]{ "header", "content", "footer"	};
		private MauronCode_dataTree<string,I_layoutUnit> DATA_regions = new MauronCode_dataTree<string,I_layoutUnit>(KEYS_regions);

		//The title
		public string Title {
			get {
				if( UNIT_console == null )
					return "Unnamed Console Application";
				return UNIT_console.Name;
			}
		}

		//Apply the design
		public ConsoleApp_layout ApplyTo( I_layoutUnit unit ) {
			Layout2d_context context = unit.Context;

			if( context == null )
				throw NullError( "Context can not be null!,(ApplyTo)", this, typeof( Layout2d_context ) );

			Layout2d_size size = context.Size;

			ConsoleLayout_header header = new ConsoleLayout_header( new Vector2d(), new Vector2d(size.Width, 1) );
			ConsoleLayout_content content = new ConsoleLayout_content( new Vector2d(0,1), new Vector2d(size.Width, size.Height-2));
			ConsoleLayout_footer footer = new ConsoleLayout_footer(new Vector2d(0,size.Height-1), new Vector2d(size.Width, 1));

			unit.AddChildAtIndex( unit.NextChildIndex, header, true );
			unit.AddChildAtIndex( unit.NextChildIndex, content, true );
			unit.AddChildAtIndex( unit.NextChildIndex, footer, true );

			DATA_regions.SetValue("header", header.ChildByIndex(0) );
			DATA_regions.SetValue("footer", footer.ChildByIndex(0));
			DATA_regions.SetValue("content", content.ChildByIndex(0));

			return this;
		}
		
		//The Console itself
		private I_layoutController UNIT_console;
		public I_layoutController Controller {
			get {
				if( UNIT_console==null )
					throw NullError( "I_layoutControler can not be null!,(Controler)",this, typeof(I_layoutController) );
				return UNIT_console;
			}
		}

		//Get a Member by name
		public I_layoutUnit Member(string key) {
			if(!DATA_regions.ContainsKey(key))
				throw Error("Invalid Member!,{"+key+"},(Member)",this,ErrorType_index.Instance);
			return DATA_regions.Value(key);
		}
	
		
	}


	public class ConsoleLayout_header : Layout2d_unit,
	I_layoutUnit {
		
		//constructor
		public ConsoleLayout_header():base( UnitType_container.Instance ) {}
		public ConsoleLayout_header( Vector2d position, Vector2d size ) : base(UnitType_container.Instance) { 
			Layout2d_context context = new Layout2d_context(position,size);
			base.SetContext( context );

			FormUnit_textField text = new FormUnit_textField();
			text.SetContext(new Layout2d_context(0,0, (int) context.Size.Width,1));
			base.AddChildAtIndex(0,text,true);
		}

	}

	public class ConsoleLayout_content : Layout2d_unit,
	I_layoutUnit {

		//constructor
		public ConsoleLayout_content():base( UnitType_container.Instance ) {}
		public ConsoleLayout_content ( Vector2d position, Vector2d size ) : base(UnitType_container.Instance) { 
			Layout2d_context context = new Layout2d_context(position,size);
			base.SetContext( context );

			FormUnit_textField text = new FormUnit_textField();
			text.SetContext(new Layout2d_context(0,0, (int) context.Size.Width, (int) context.Size.Height ) );
			base.AddChildAtIndex(0,text,true);
		}
	}

	public class ConsoleLayout_footer : Layout2d_unit,
	I_layoutUnit {

		//constructor
		public ConsoleLayout_footer ( ) : base(UnitType_container.Instance) { }
		public ConsoleLayout_footer (Vector2d position, Vector2d size)
			: base(UnitType_container.Instance) {
			Layout2d_context context=new Layout2d_context(position, size);
			base.SetContext(context);

			FormUnit_textField text=new FormUnit_textField();
			text.SetContext(new Layout2d_context(0, 0, (int) context.Size.Width, 1));
			base.AddChildAtIndex(0, text, true);
		}
	}
}