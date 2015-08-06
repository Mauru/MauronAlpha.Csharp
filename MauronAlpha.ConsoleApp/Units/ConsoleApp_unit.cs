using MauronAlpha.HandlingData;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Forms.Interfaces;
using MauronAlpha.Forms.Units;
using MauronAlpha.Forms.DataObjects;

using MauronAlpha.ConsoleApp.Interfaces;
using MauronAlpha.ConsoleApp.Collections;

using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.Text.Units;

namespace MauronAlpha.ConsoleApp.Units {
	
	public class ConsoleApp_unit : Layout2d_unit,
	I_consoleUnit,
	I_caretSource {
		
		//constructor
		public ConsoleApp_unit ( ) : base(UnitType_container.Instance) {
			AddChildAtIndex(0, FORM_content, true);
		}
		public ConsoleApp_unit ( Vector2d position, Vector2d size ):this() {
			Layout2d_context context=new Layout2d_context(position, size);
			SetContext(context);
			FORM_content.SetContext(context.Instance);			
		}

		//Methods
		public virtual I_consoleUnit PrependContent(string content,bool newLine) {
			FORM_content.PrependText(content, newLine);
			RequestRender();
			return this;
		}
		public virtual I_consoleUnit AppendContent(string content, bool newLine) {
			FORM_content.AppendText(content, newLine);
			RequestRender();
			return this;
		}
		public virtual I_consoleUnit SetContent (string content) {
			FORM_content.SetText(content);
			RequestRender();
			return this;
		}
		
		public virtual I_consoleUnit DrawTo(I_consoleOutput output, I_layoutUnit parent) {
			Layout2d_size maxSize = Context.MaxSize;

			int height = Input.CountLines;
			int maxHeight = (int) maxSize.Height;
			if (maxHeight < 0)
				maxHeight = height;

			Vector2d position = new Vector2d(Context.Position.X+parent.Position.X,Context.Position.Y+parent.Position.Y);
			

			
			MauronCode_dataList<TextUnit_line> lines = Input.Lines;
			if (maxHeight > lines.Count)
				maxHeight = lines.Count;

			for (int n = 0; n < maxHeight; n++) {
				output.SetCaretPosition(position);
				output.Write(lines.Value(n));
				position.Add(0,1);
			}

			return this;

		}
		
		public virtual I_consoleUnit Insert(KeyPress key) {
			if (key.IsFunction)
				System.Console.WriteLine("Is function!,(ConsoleApp_unit)");
			else
				System.Console.WriteLine("Is not a function!,(ConsoleApp_unit)");

			TextUnit_text text = Content;
			text.SetIsReadOnly(false);
			text.InsertUnitAtContext(CaretPosition.Context, new TextUnit_character(key.Char), true);

			RequestRender();
			return this;
		}
		public virtual I_consoleUnit InsertContent(string str) {
			MauronCode_dataList<TextUnit_character> characters = Input.Encoding.StringToCharacters(str);
			TextUnit_word word = ActiveWord;
			int count = characters.Count;
			int offset = CaretPosition.Character;
			for (int index = 0; index < count; index++)
				word.InsertChildAtIndex(offset + index, characters.Value(index), false);
			word.Parent.ReIndex(word.Index, true, true);
			RequestRender();
			return this;
		}

		public virtual I_consoleUnit SetMaxSize(int x, int y) {
			Context.SetMaxSize(new Layout2d_size(x, y));
			return this;
		}

		//Properties

		public virtual TextUnit_text Content {
			get {
				return FORM_content.Text;
			}
		}
		
		public virtual TextUnit_line ActiveLine {
			get {
				return Input.ActiveLine;
			}
		}
		public virtual TextUnit_word ActiveWord {
			get {
				return Input.ActiveWord;
			}
		}
		public virtual TextUnit_character ActiveCharacter {
			get {
				return Input.ActiveCharacter;
			}
		}

		public virtual CaretPosition CaretPosition {
			get {
				return Input.CaretPosition;
			}
		}

		public Vector2d TextSize { 
			get {
				int x = 0;
				int y = 0;
				foreach (TextUnit_line line in Input.Lines) {
					int charCount = line.CountRealCharacters;
					if (charCount > x)
						x = charCount;
					y++;
				}
				return new Vector2d(x, y);
			} }

		public virtual Layout2d_size MaxSize { 
			get {
				if (Context.MaxSize.Equals(-1,-1))
					return new Layout2d_size(TextSize);
				return Context.MaxSize;
			}
		}

		protected FormUnit_textField FORM_content = new FormUnit_textField();
		public FormUnit_textField Input {
			get {
				return FORM_content;
			}
		}

	}

}
