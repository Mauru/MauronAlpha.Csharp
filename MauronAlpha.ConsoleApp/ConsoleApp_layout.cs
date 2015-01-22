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
			Layout2d_window window = base.Window;
			Layout2d_context context = window.Context;

			if( context == null )
				throw NullError( "LAYOUT_context can not be null!,(Apply)", this, typeof( Layout2d_context ) );

			Layout2d_size windowSize = context.Size;

			System.Console.WriteLine("Creating header");			
			#region The header
			Layout2d_container header;
			if(!TREE_regions.IsSet("header")) {
				header =new Layout2d_container(base.LAYOUT_window);
				I_layoutUnit title = new FormUnit_textField( header );
				
				base.LAYOUT_window.AddChildAtIndex(0,header);
			}
			header = base.LAYOUT_window.ChildByIndex(0).As<Layout2d_container>();
			header.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector.SetY(1)));
			header.Context.SetSize(new Layout2d_size(windowSize.AsVector.SetY(1), true));
			#endregion
			/*
			System.Console.WriteLine("Creating content");

			#region The Content
			Layout2d_container content;
			if(!TREE_regions.IsSet("content")) {
				content=new Layout2d_container(base.LAYOUT_window);
				FormUnit_textField text=new FormUnit_textField(content);
				content.AddChildAtIndex(0, text.AsReference);
				base.LAYOUT_window.AddChildAtIndex(1,content.AsReference);
			}
			content=base.LAYOUT_window.ChildByIndex(1).As<Layout2d_container>();
			content.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector.Subtract(0,3)));
			content.Context.SetSize(new Layout2d_size(content.Context.Constraint.AsVector2d,false));
			#endregion

			System.Console.WriteLine("Creating input");
			
			#region The Command-line
			Layout2d_container input;
			if(!TREE_regions.IsSet("input")){
				input = new Layout2d_container(base.LAYOUT_window);
				FormUnit_textField inputText = new FormUnit_textField(input);
				input.AddChildAtIndex(0, inputText.AsReference);
				base.LAYOUT_window.AddChildAtIndex(2,input.AsReference);
			}
			input=base.LAYOUT_window.ChildByIndex(2).As<Layout2d_container>();
			input.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector.SetY(1)));
			input.Context.SetSize(new Layout2d_size(windowSize.AsVector.SetY(1),false));
			#endregion

			System.Console.WriteLine("Creating footer");

			#region The Footer
			Layout2d_container footer;
			if( !TREE_regions.IsSet("footer") ) {
				footer=new Layout2d_container(base.LAYOUT_window);
				FormUnit_textField footerText=new FormUnit_textField(input);
				input.AddChildAtIndex(0, footerText.AsReference);
				base.LAYOUT_window.AddChildAtIndex(3, input.AsReference);
			}
			footer=base.LAYOUT_window.ChildByIndex(3).As<Layout2d_container>();
			input.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector.SetY(1)));
			input.Context.SetSize(new Layout2d_size(windowSize.AsVector.SetY(1), true));
			#endregion
			*/
			return this;
		}
	}
}