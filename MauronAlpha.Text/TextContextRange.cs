using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {	

	//A range of text
	public class TextContextRange:MauronCode_textComponent {

		//constructor
		public TextContextRange() {}
		
		#region the Parent Text
		private TextComponent_text TXT_parent;
		public TextComponent_text Parent {
			get {
				if(TXT_parent==null){
					NullError("Parent can not be null!,(Parent)",this,typeof(TextComponent_text));
				}
				return TXT_parent;
			}
		}
		private TextContextRange SetParent(TextComponent_text parent){
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
