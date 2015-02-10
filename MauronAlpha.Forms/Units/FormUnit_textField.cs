using MauronAlpha.Text.Units;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Forms.Units {
	
	//A Entity waiting for user input
	public class FormUnit_textField : FormComponent_unit, I_layoutUnit {
		
		//constructor
		public FormUnit_textField():base( FormType_textField.Instance ) {}
       
		private TextUnit_text UNIT_text = new TextUnit_text();
		public FormUnit_textField SetText (string text) {
			UNIT_text = new TextUnit_text();
			
			UNIT_text.SetText(text);
			return this;
		}
		public TextUnit_text Text {
			get {
				UNIT_text.SetIsReadOnly(true);
				return UNIT_text;
			}
		}

		public TextUnit_line LineByIndex (int n) {
			return UNIT_text.LineByIndex(n);
		}

		


	}

	//Form Description
	public sealed class FormType_textField : Layout2d_unitType {
		#region singleton
		private static volatile FormType_textField instance = new FormType_textField();
		private static object syncRoot = new System.Object();

		//constructor singleton multithread safe
		static FormType_textField ( ) { }
		public static Layout2d_unitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance = new FormType_textField();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "textField"; } }

		public override bool CanHaveChildren {
			get { return true; }
		}
		public override bool CanHaveParent {
			get { return true; }
		}
		public override bool CanHide {
			get { return true; }
		}
		public override bool IsDynamic {
			get { return true; }
		}
	}

}