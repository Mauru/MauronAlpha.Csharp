using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Text.Units;

using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.ConsoleApp.Interfaces {
	
	public interface I_consoleUnit:I_layoutUnit {

		I_consoleUnit SetContent(string content);

		TextUnit_line LineAsOutput(int n);

		TextUnit_text Content { get; }

		CaretPosition CaretPosition { get; }

		I_formComponent Input { get; }
	}

}
