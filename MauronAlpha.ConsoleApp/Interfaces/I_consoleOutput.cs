using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Text.Units;

namespace MauronAlpha.ConsoleApp.Interfaces {
	
	public interface I_consoleOutput:I_layoutRenderer {
		
		I_consoleOutput WriteLine(TextUnit_line unit);
	}

}
