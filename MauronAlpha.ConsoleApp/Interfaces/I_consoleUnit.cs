using MauronAlpha.ConsoleApp.Collections;
using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Text.Units;

using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.Forms.DataObjects;
using MauronAlpha.Forms.Interfaces;
using MauronAlpha.Forms.Units;

namespace MauronAlpha.ConsoleApp.Interfaces {
	
	public interface I_consoleUnit:I_layoutUnit {

		I_consoleUnit SetContent(string content);
		I_consoleUnit PrependContent(string content, bool newLine);
		I_consoleUnit AppendContent(string content, bool newLine);

		I_consoleUnit DrawTo(I_consoleOutput output, I_layoutUnit parent);
		I_consoleUnit Insert(KeyPress key);

		Vector2d TextSize { get; }

		TextUnit_text Content { get; }

		CaretPosition CaretPosition { get; }

		FormUnit_textField Input { get; }

	}

}
