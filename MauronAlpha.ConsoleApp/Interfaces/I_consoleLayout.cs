using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.ConsoleApp.Interfaces {

	public interface I_consoleLayout:I_layoutModel {

		string Title { get; }

		I_consoleLayout Draw();

		new I_consoleUnit Member (string name);


	}

}
