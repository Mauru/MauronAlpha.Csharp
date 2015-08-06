using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Forms.Interfaces;
using MauronAlpha.Forms.Units;

namespace MauronAlpha.ConsoleApp.Interfaces {

	public interface I_consoleLayout:I_layoutModel {

		string Title { get; }

		I_consoleLayout Draw();

		bool HasMember(string member);
		new I_consoleUnit Member (string name);

		FormUnit_textField ActiveInput { get; }

	}

}
