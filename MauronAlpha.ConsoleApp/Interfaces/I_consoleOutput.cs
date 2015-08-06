using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Forms.DataObjects;

namespace MauronAlpha.ConsoleApp.Interfaces {
	
	public interface I_consoleOutput:I_layoutRenderer {
		I_consoleOutput Write(I_textUnit unit);
		I_consoleOutput Write(string text);
		I_consoleOutput WriteLine(I_textUnit unit);
		I_consoleOutput WriteLine(string text);
		I_consoleOutput SetCaretPosition(I_consoleUnit focus, CaretPosition position);
		I_consoleOutput SetCaretPosition(Vector2d pos);
		I_consoleOutput SetScreenBufferSize(Vector2d size);
	}

}
