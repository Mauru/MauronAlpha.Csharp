using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Text.Units;

namespace MauronAlpha.ConsoleApp.Interfaces {
	
	public interface I_consoleUnit:I_layoutUnit {

		I_consoleUnit SetContent(string content);

		TextUnit_line LineAsOutput(int n);

		TextUnit_text Content { get; }

	}

}
