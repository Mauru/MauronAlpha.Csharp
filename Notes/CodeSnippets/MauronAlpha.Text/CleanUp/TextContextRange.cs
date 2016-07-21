using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {	

	//A range of text
	public class TextContextRange:MauronCode_textComponent {

		//constructor
		public TextContextRange() {}
		
		#region the Parent Text
		private TextUnit_text TXT_parent;
		public TextUnit_text Parent {
			get {
				if(TXT_parent==null){
					NullError("Parent can not be null!,(Parent)",this,typeof(TextUnit_text));
				}
				return TXT_parent;
			}
		}
		private TextContextRange SetParent(TextUnit_text parent){
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(SetParent)", this, ErrorType_protected.Instance);
			}
			#endregion
			TXT_parent=parent;
			return this;
		}
		#endregion

		#region ReadOnly
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		public TextContextRange SetReadOnly (bool status) {
			B_isReadOnly=status;
			return this;
		}
		#endregion

	}
}
