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

        private I_layoutUnit LAYOUT_window;

		private static string[] KEYS_regions = new string[]{"header","content","input","footer"};
		private MauronCode_dataTree<string,Layout2d_container> TREE_regions = new MauronCode_dataTree<string,Layout2d_container>(KEYS_regions);

		//Apply the design
		public override Layout2d_design Apply ( ) {

			Layout2d_size windowSize = LAYOUT_window.Context.Size;
			
			#region The header
			Layout2d_container header;
			if(!TREE_regions.IsSet("header")) {
				header =new Layout2d_container(LAYOUT_window);
				FormUnit_textField title = new FormUnit_textField(header);
				header.AddChildAtIndex(0, title.AsReference);
				LAYOUT_window.AddChildAtIndex(0,header.AsReference);
			}
			header = LAYOUT_window.ChildByIndex(0).As<Layout2d_container>();
			header.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector2d.SetY(1)));
			header.Context.SetSize(new Layout2d_size(windowSize.AsVector2d.SetY(1), true));
			#endregion

			#region The Content
			Layout2d_container content;
			if(!TREE_regions.IsSet("content")) {
				content=new Layout2d_container(LAYOUT_window);
				FormUnit_textField text=new FormUnit_textField(content);
				content.AddChildAtIndex(0, text.AsReference);
				LAYOUT_window.AddChildAtIndex(1,content.AsReference);
			}
			content=LAYOUT_window.ChildByIndex(1).As<Layout2d_container>();
			content.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector2d.Subtract(0,3)));
			content.Context.SetSize(new Layout2d_size(content.Context.Constraint.AsVector2d,false));
			#endregion
			
			#region The Command-line
			Layout2d_container input;
			if(!TREE_regions.IsSet("input")){
				input = new Layout2d_container(LAYOUT_window);
				FormUnit_textField inputText = new FormUnit_textField(input);
				input.AddChildAtIndex(0, inputText.AsReference);
				LAYOUT_window.AddChildAtIndex(2,input.AsReference);
			}
			input=LAYOUT_window.ChildByIndex(2).As<Layout2d_container>();
			input.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector2d.SetY(1)));
			input.Context.SetSize(new Layout2d_size(windowSize.AsVector2d.SetY(1),false));
			#endregion

			#region The Footer
			Layout2d_container footer;
			if( !TREE_regions.IsSet("footer") ) {
				footer=new Layout2d_container(LAYOUT_window);
				FormUnit_textField footerText=new FormUnit_textField(input);
				input.AddChildAtIndex(0, footerText.AsReference);
				LAYOUT_window.AddChildAtIndex(3, input.AsReference);
			}
			footer=LAYOUT_window.ChildByIndex(3).As<Layout2d_container>();
			input.Context.SetConstraint(new Layout2d_constraint(windowSize.AsVector2d.SetY(1)));
			input.Context.SetSize(new Layout2d_size(windowSize.AsVector2d.SetY(1), true));
			#endregion

			return this;
		}
	}
}