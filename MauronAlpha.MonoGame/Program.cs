namespace MauronAlpha.MonoGame {
	using System;
	using MauronAlpha.MonoGame.Actuals;
	using MauronAlpha.FileSystem.Units;


public static class Program {

	[STAThread]
	static void Main() {

        var domaininfo = new AppDomainSetup();
		FileStructure structure = new FileStructure(System.Environment.CurrentDirectory);
		
		GameManager manager = new GameManager();
		GameContentManager resources = new GameContentManager(manager, structure);
		GameEngine game = new GameEngine(manager);

		GameLogic logic = new SampleGameLogic();

		manager.Set(logic);


		game.Start();
		while(game != null) {
			//game.DoNothing();

			if(game.CanExit)
				game.Exit();

		}

		System.Console.WriteLine("Application exited cleanly. - Press any key to finish -");
		System.Console.ReadKey();
	}

}

}

namespace MauronAlpha.MonoGame.Actuals {

	public class SampleGameLogic :GameLogic {

		public SampleGameLogic(): base() {

		}
	}

}