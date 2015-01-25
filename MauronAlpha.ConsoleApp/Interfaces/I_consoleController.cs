using MauronAlpha.ConsoleApp.Interfaces;


namespace MauronAlpha.ConsoleApp.Interfaces {
	
	public interface I_consoleController {

		I_consoleLayout LayoutModel { get; }

		ConsoleApp_commandModel CommandModel { get; }

		I_consoleData ContentModel { get; }

		bool AllowsInput { get; }

	}
}
