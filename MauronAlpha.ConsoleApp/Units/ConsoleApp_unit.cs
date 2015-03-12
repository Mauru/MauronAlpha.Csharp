using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Forms.Interfaces;
using MauronAlpha.Forms.Units;

using MauronAlpha.ConsoleApp.Interfaces;

using MauronAlpha.Text.Units;


namespace MauronAlpha.ConsoleApp.Units {
	
	public class ConsoleApp_unit : Layout2d_unit,
	I_consoleUnit {
		
		//constructor
		public ConsoleApp_unit ( ) : base(UnitType_container.Instance) {
			AddChildAtIndex(0, FORM_content, true);
		}
		public ConsoleApp_unit ( Vector2d position, Vector2d size ):this() {
			Layout2d_context context=new Layout2d_context(position, size);
			SetContext(context);
			FORM_content.SetContext(context.Instance);			
		}

		protected FormUnit_textField FORM_content = new FormUnit_textField();

		//Methods
		public virtual I_consoleUnit SetContent (string content) {
			FORM_content.SetText(content);
			return this;
		}

		public virtual TextUnit_text Content {
			get {
				return FORM_content.Text;
			}
		}

		public virtual TextUnit_line LineAsOutput (int n) {
			return FORM_content.LineByIndex(n);
		}

		private CaretPosition DATA_position = new CaretPosition();
		public virtual CaretPosition CaretPosition {
			get {
				return DATA_position;
			}
		}

		public I_formComponent Input {
			get {
				return FORM_content;
			}
		}

	}

	public class ConsoleLayout_header : ConsoleApp_unit {

		//constructor
		public ConsoleLayout_header ( ) : base() { }
		public ConsoleLayout_header (Vector2d position, Vector2d size) : base(position, size) {
			FORM_content.SetHeight(1);
		}

	}

	public class ConsoleLayout_content : ConsoleApp_unit {

		//constructor
		public ConsoleLayout_content ( ) : base() { }
		public ConsoleLayout_content (Vector2d position, Vector2d size)	: base(position, size) {}
		
	}

	public class ConsoleLayout_footer : ConsoleApp_unit {

		//constructor
		public ConsoleLayout_footer ( ) : base() { }
		public ConsoleLayout_footer (Vector2d position, Vector2d size)	: base(position, size) {
			FORM_content.SetHeight(1);
		}

	}

}
