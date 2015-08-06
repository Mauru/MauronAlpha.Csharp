using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.ConsoleApp.Collections;
using MauronAlpha.ConsoleApp.Interfaces;

namespace MauronAlpha.ConsoleApp.Units
{
    public class ConsoleLayout_content : ConsoleApp_unit
    {

        //constructor
        public ConsoleLayout_content() : base() { }
        public ConsoleLayout_content(Vector2d position, Vector2d size) : base(position, size)
        {
            FORM_content.SetHeight(1);
        }

		//Command delegates - Movement
		public virtual bool CMD_cursorLeft() {
			CaretPosition.Add(0, 0, 0, 1);
			return true;
		}
		public virtual bool CMD_cursorRight() {
			CaretPosition.Add(0, 0, 0, -1);
			return true;
		}
		public virtual bool CMD_cursorUp() {
			CaretPosition.Add(0, -1, 0, 0);
			return true;
		}
		public virtual bool CMD_cursorDown() {
			CaretPosition.Add(0, 1, 0, 0);
			return true;
		}
		public virtual bool CMD_backSpace() {
			Content.RemoveCharacterAtContext(CaretPosition.Context);
			CMD_cursorLeft();
			return true;
		}

		//Register commands
		public I_consoleUnit Register_commands(ConsoleApp_keyCommands map) {
			map.SetCommand("LeftArrow", CMD_cursorLeft);
			map.SetCommand("RightArrow", CMD_cursorRight);
			map.SetCommand("UpArrow", CMD_cursorRight);
			map.SetCommand("DownArrow", CMD_cursorDown);
			map.SetCommand("Backspace", CMD_backSpace);
			return this;
		}
    }
}
