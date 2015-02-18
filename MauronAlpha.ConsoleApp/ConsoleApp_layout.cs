using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;

using MauronAlpha.Forms.Units;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.ConsoleApp.Interfaces;
using MauronAlpha.ConsoleApp.Units;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.ConsoleApp {

	//Sets up the layout
	public class ConsoleApp_layout : Layout2d_design,
	I_consoleLayout {

		//constructor
		public ConsoleApp_layout( ) : base() {
		}
		public ConsoleApp_layout( I_layoutController console )
			: base() {
			UNIT_console = console;
		}

		//The regions
		private static string[] KEYS_regions = new string[ 3 ] { "header", "content", "footer" };
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

			ConsoleLayout_content content = new ConsoleLayout_content( new Vector2d( 0, 1 ), new Vector2d( size.Width, size.Height-2 ) );

			ConsoleLayout_footer footer = new ConsoleLayout_footer( new Vector2d( 0, size.Height-1 ), new Vector2d( size.Width, 1 ) );


			unit.AddChildAtIndex( unit.NextChildIndex, header, true );
			unit.AddChildAtIndex( unit.NextChildIndex, content, true );
			unit.AddChildAtIndex( unit.NextChildIndex, footer, true );

			DATA_regions.SetValue( "header", header );
			DATA_regions.SetValue( "footer", footer );
			DATA_regions.SetValue( "content", content );

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

		//Methods
		public I_consoleLayout Draw( ) {
			if( UNIT_console == null ) {
				Exception( "No Console defined!,(Draw)", this, ErrorResolution.DoNothing );
				return this;
			}
			I_consoleOutput output = ( I_consoleOutput ) UNIT_console.Output;

			Vector2d lineIndex=new Vector2d();
			int maxWidth = ( int ) Controller.Context.Size.Width;

			I_consoleUnit header = Member( "header" );
			TextContext headerContext = header.Content.CountAsContext;

			for( int n=0; n < header.Context.Size.Height; n++ ) {
				if( n >= headerContext.Line ) {
					output.WriteLine( new TextUnit_line() );
					break;
				}
				TextUnit_line line = header.LineAsOutput( n );
				output.WriteLine( line, maxWidth );
			}

			I_consoleUnit content = Member( "content" );
			TextContext mainContext = content.Content.CountAsContext;

			System.Console.WriteLine( content.Context.AsString );

			for( int n=0; n < content.Context.Size.Height; n++ ) {
				if( n >= mainContext.Line ) {
					output.WriteLine( new TextUnit_line( n+"#EMPTY" ) );
				} else {
					TextUnit_line line = content.LineAsOutput( n );
					TextUnit_word preface = new TextUnit_word();
					preface.SetText( n+"#" );
					line.InsertChildAtIndex( 0, preface, false, true );
					output.WriteLine( line, maxWidth );
				}
			}

			I_consoleUnit footer=Member( "footer" );
			TextContext footerContext = footer.Content.CountAsContext;

			return this;

		}

	}


}