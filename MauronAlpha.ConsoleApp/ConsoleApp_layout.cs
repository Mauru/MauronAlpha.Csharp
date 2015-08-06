using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Collections;

using MauronAlpha.Forms.Units;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.ConsoleApp.Interfaces;
using MauronAlpha.ConsoleApp.Units;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.ConsoleApp {

	//Sets up the layout
	public class ConsoleApp_layout : Layout2d_design,
	I_consoleLayout,I_layoutRenderer {

		//constructor
		public ConsoleApp_layout( ) : base() {
		}
		public ConsoleApp_layout( I_layoutController console ) : base() {
			UNIT_console = console;
		}

		//Booleans
		private bool B_isRendering = false;
		public bool IsRendering { get { return B_isRendering; } }
		private bool B_needsRenderUpdate = true;
		public bool NeedsRenderUpdate { 
			get { 
				return B_needsRenderUpdate;
			} 
		}
		public bool HasMember(string member) {
			return DATA_regions.ContainsKey(member);
		}

		//The regions
		private static string[] KEYS_regions = new string[ 4 ] { "header", "content", "footer", "debug" };
		private MauronCode_dataTree<string, I_consoleUnit> DATA_regions=new MauronCode_dataTree<string, I_consoleUnit>( KEYS_regions );

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

			ConsoleLayout_header header = new ConsoleLayout_header( new Vector2d(), new Vector2d( size.Width, 1 ) );
			header.SetMaxSize((int)size.Width, 1);
			header.SetRenderManager(this);

			ConsoleLayout_content content = new ConsoleLayout_content( new Vector2d( 0, 1 ), new Vector2d( size.Width, size.Height-3 ) );
			content.Context.SetResizeMode("Y");
			content.SetRenderManager(this);

			ConsoleLayout_debug debug = new ConsoleLayout_debug(new Vector2d(0, size.Height - 2), new Vector2d(size.Width, 1));
			debug.SetMaxSize((int)size.Width, 1);
			debug.SetRenderManager(this);

			ConsoleLayout_footer footer = new ConsoleLayout_footer( new Vector2d( 0, size.Height-1 ), new Vector2d( size.Width, 1 ) );
			footer.SetMaxSize((int) size.Width, 1);
			footer.SetRenderManager(this);

			unit.AddChildAtIndex( unit.NextChildIndex, header, true );
			unit.AddChildAtIndex( unit.NextChildIndex, content, true );
			unit.AddChildAtIndex(unit.NextChildIndex, debug, true);
			unit.AddChildAtIndex( unit.NextChildIndex, footer, true );

			DATA_regions.SetValue( "header", header );
			DATA_regions.SetValue("content", content);
			DATA_regions.SetValue("debug", debug);
			DATA_regions.SetValue( "footer", footer );

			return this;
		}

		//The Console itself
		private I_layoutController UNIT_console;
		public I_layoutController Controller {
			get {
				if( UNIT_console==null )
					throw NullError( "I_layoutControler can not be null!,(Controler)", this, typeof( I_layoutController ) );
				return UNIT_console;
			}
		}

		//Get a Member by name
		public I_consoleUnit Member( string key ) {
			if( !DATA_regions.ContainsKey( key ) )
				throw Error( "Invalid Member!,{"+key+"},(Member)", this, ErrorType_index.Instance );
			return DATA_regions.Value( key );
		}
		I_layoutUnit I_layoutModel.Member( string key ) {
			if( !DATA_regions.ContainsKey( key ) )
				throw Error( "Invalid Member!,{"+key+"},(Member)", this, ErrorType_index.Instance );
			return ( I_layoutUnit ) DATA_regions.Value( key );
		}
		public I_layoutUnit MainScreen { get { return Controller.MainScreen; } }
	
		//Methods
		public I_consoleLayout Draw( ) {
			//No console defined
			if( UNIT_console == null ) {
				Exception( "No Console defined!,(Draw)", this, ErrorResolution.DoNothing );
				return this;
			}

			//Render-state
			if (IsRendering || !NeedsRenderUpdate)
				return this;
			B_isRendering = true;

			I_consoleOutput output = ( I_consoleOutput ) UNIT_console.Output;

			//Determine maxWidth
			Layout2d_size size = Controller.Context.Size;

			//Determine height
			MauronCode_dataMap<Vector2d> map = BuildSizeMap();
			
			Header.DrawTo(output, MainScreen);
			Content.DrawTo(output, MainScreen);
			Debug.DrawTo(output, MainScreen);
			Footer.DrawTo(output, MainScreen);

			//Reset cursor position to be on top of screen
			output.SetCaretPosition(Header, Header.CaretPosition);
			output.SetCaretPosition(Content, Content.CaretPosition);

			B_isRendering = false;
			B_needsRenderUpdate = false;
			return this;

		}
		public I_consoleUnit Header {
			get {
				return Member("header");
			}
		}
		public I_consoleUnit Footer {
			get {
				return Member("footer");
			}
		}
		public I_consoleUnit Content {
			get {
				return Member("content");
			}
		}
		public I_consoleUnit Debug {
			get {
				return Member("debug");
			}
		}

		public MauronCode_dataMap<Vector2d> BuildSizeMap() {
			MauronCode_dataMap<Vector2d> result = new MauronCode_dataMap<Vector2d>();
			result.SetValue("header", Header.TextSize);
			result.SetValue("content", Content.TextSize);
			result.SetValue("debug", Debug.TextSize);
			result.SetValue("footer", Footer.TextSize);
			return result;
		}

		public I_layoutRenderer HandleRenderRequest(Layout2d_renderChain chain) {
			B_needsRenderUpdate = true;
			if (!IsRendering) {
				Clear();
				Draw();
			}
			
			return this;
		}
		public I_layoutRenderer Clear() {
			Controller.Output.Clear();
			return this;
		}

		public FormUnit_textField ActiveInput { 
			get { 
				return Content.Input; 
			}
		}
	}


}