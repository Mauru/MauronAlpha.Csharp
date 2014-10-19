using System;

using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Context {
	
	
	public class TextContextValidator:MauronCode_utility {

		private TextContext TXT_textContext;
		private TextContext Context {
			get {
				if(TXT_textContext==null) {
					NullError("Context can not be null!,(Context)",this,typeof(TextContext));
				}
				return TXT_textContext;
			}
		}

		//constructor
		public TextContextValidator(TextContext context) {
			TXT_textContext = context;
		}

		//LineIndex
		public bool LineBounds(TextUnit_text text){
			
			if( Context.Index.Line < 0 ) {
				return false;
			}

			if( Context.Index.Line > text.LineCount ) {
				return false;
			}

			return true;
		}


	}


}
