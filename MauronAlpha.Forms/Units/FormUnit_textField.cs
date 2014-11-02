using MauronAlpha.Text.Units;
using MauronAlpha.Text.Utility;
using MauronAlpha.Layout.Position;

namespace MauronAlpha.Forms.Units {
	
	//A entity waiting for user input
	public class FormUnit_textField:FormComponent_unit {
		
		//constructor
		public FormUnit_textField():base() {}

		private TextUnit_text TXT_text;

		private TextUtility_encoding Encoding;

		private bool B_isReadOnly = false;

		private LayoutPosition XY_position;

	}
}
