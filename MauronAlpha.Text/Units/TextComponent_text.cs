using System.Collections.Generic;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Utility;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {

	//A complete text
	public class TextComponent_text:TextComponent {

		private MauronCode_dataList<TextComponent_line> Lines=new MauronCode_dataList<TextComponent_line>();

		public TextComponent_text AddLine(TextComponent_line line){
			Lines.AddValue (line);
			return this;
		}

		public TextComponent_text AddLineAtContext(TextContext context){
			TextComponent_line line = new TextComponent_line (this, context);
			Lines.InsertValueAt (context.LineOffset, line);
			return this;
		}

		public TextComponent_line LineByContext(TextContext context){
			if (!Lines.ContainsKey (context.LineOffset)) {
				Error ("Invalid Line {" + context.LineOffset + "}", this);
			}
			return Lines.Value (context.LineOffset);
		}

		public int LineCount {
			get { 
				return Lines.Count;
			}
		}

		public TextComponent_line LastLine {
			get { 
				if (Lines.Count < 1) {
					Error ("Lines is empty #f{LastLine}", this, ErrorType_index.Instance);
				}
				return Lines.LastElement;
			}

		}
	}

}