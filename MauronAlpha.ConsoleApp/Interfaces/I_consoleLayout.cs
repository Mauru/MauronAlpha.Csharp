using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.ConsoleApp.Interfaces {

	public interface I_consoleLayout:I_layoutModel {

		string Title { get; }

		I_consoleLayout Draw();

		new I_consoleUnit Member (string name);

		I_formComponent ActiveInput { get; }

	}

}
