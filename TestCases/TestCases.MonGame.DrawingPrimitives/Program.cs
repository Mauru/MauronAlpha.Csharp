namespace MauronAlpha.MonoGame {
using System;
	using MauronAlpha.MonoGame.Actuals;

#if WINDOWS || LINUX
public static class Program {

	[STAThread]
	static void Main() {
		GameLogic logic = new SampleGameLogic();
		GameManager manager = new GameManager();
		manager.Set(logic);
		GameEngine game = new GameEngine(manager);
		
		game.Run();
		while(game.CanExit) {
			game.DoNothing();
		}
	}
}
#endif
}

