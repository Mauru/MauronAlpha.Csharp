using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Interfaces;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.Forms.DataObjects;
using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.Units {
	
	//A Entity waiting for user input
	public class FormUnit_textField : FormComponent_unit,
	I_layoutUnit {
		
		//constructor
		public FormUnit_textField():base( FormType_textField.Instance ) {}
       
		public FormUnit_textField SetText (string text) {
			UNIT_text = new TextUnit_text(text);
			return this;
		}
		public FormUnit_textField PrependText(string text, bool newLine) { 
			UNIT_text.PrependText(text, newLine);
			return this;
		}
		public FormUnit_textField AppendText(string text, bool newLine) { 
			UNIT_text.AppendText(text, newLine);
			return this;
		}

		private TextUnit_text UNIT_text = new TextUnit_text();
		public TextUnit_text Text {
			get {
				UNIT_text.SetIsReadOnly(true);
				return UNIT_text;
			}
		}

		private CaretPosition DATA_caret;
		public CaretPosition CaretPosition {
			get {
				if (DATA_caret == null)
					DATA_caret = new CaretPosition(UNIT_text);
				return DATA_caret;
			}
		}

		public I_textEncoding Encoding {
			get {
				return UNIT_text.Encoding;
			}
		}

		public int CountLines {
			get {
				int result = 0;
				foreach (TextUnit_paragraph p in UNIT_text.Children) {
					result += p.ChildCount;
				}
				return result;
			}
		}

		//Querying
		public TextUnit_paragraph ActiveParagraph {
			get {
				return UNIT_text.ParagraphByIndex(CaretPosition.Paragraph);
			}
		}

		public TextUnit_line LineByIndex (int index){
			TextContext count = UNIT_text.CountAsContext;
			if (index < 0 || index > count.Line)
				throw Error("Index out of bounds!,{"+index+"},(LineByIndex)", this, ErrorType_bounds.Instance);
			else if (index == count.Line)
				return UNIT_text.NewLine;
			return UNIT_text.LineByIndex(index);
		}
		public TextUnit_line ActiveLine {
			get {
				return ActiveParagraph.LineByIndex(CaretPosition.Line);
			}
		}
		
		public TextUnit_word ActiveWord {
			get {
				return ActiveLine.WordByIndex(CaretPosition.Word);
			}
		}

		public TextUnit_character ActiveCharacter {
			get {
				return ActiveWord.ForceCharacter(CaretPosition.Character);
			}
		}

		public MauronCode_dataList<TextUnit_line> Lines {
			get {
				return UNIT_text.Lines;
			}
		}

		//Booleans
		public bool HasLine(int n) {
			return UNIT_text.HasLineAtIndex(n);
		}
		public override bool EVENT_keyUp (KeyPress key) {
			if (IsReadOnly)
				return true;
			UNIT_text.SetIsReadOnly(false);
			UNIT_text.InsertUnitAtContext(CaretPosition.Context, new TextUnit_character(key.Char), true);
			this.RequestRender();
			return true;
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