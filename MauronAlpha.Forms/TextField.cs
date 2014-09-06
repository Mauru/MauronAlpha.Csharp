using System;
using MauronAlpha.Text.Units;
using MauronAlpha.Text;
using MauronAlpha.Text.Utility;
using MauronAlpha.HandlingErrors;


namespace MauronAlpha.Forms {

	public class TextField:FormComponent {

		private TextContext CaretPosition=new TextContext(0,0,0);

		private TextComponent_text TXT_text;
		private TextComponent_text Text {
			get {
				if (TXT_text == null) {
					TXT_text = new TextComponent_text ();
				}
				return TXT_text;
			}
		}

		public TextField RemoveCharacterByOffset(int n){
			#region ExceptionCheck: bounds : !R;
			if( !Text.ContainsCharacterOffset(n) ) {
				Exception("CharacterOffset out of bounds!,{"+n+"},(RemoveCharacterByOffset)",this,ErrorResolution.DoNothing);
				return this;
			}
			#endregion
			Text.RemoveCharacterByOffset(n);
			return this;
		}

		public TextField AddString(string txt){
			Text.AddString (txt);
			return this;
		}

		public TextField AddStringAtOffset(string txt, int offset){
			return this;
		}
	}

}
