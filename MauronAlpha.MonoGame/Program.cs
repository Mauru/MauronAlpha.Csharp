namespace MauronAlpha.MonoGame {
	using System;
	using MauronAlpha.MonoGame.Actuals;
	using MauronAlpha.FileSystem.Units;


public static class Program {

	[STAThread]
	static void Main() {

		//Get the current directory and establish the FileStructure
        var domaininfo = new AppDomainSetup();
		string dirOfCode = System.Environment.CurrentDirectory;
		string dirOfExe = System.Reflection.Assembly.GetExecutingAssembly().Location;
		FileStructure structure = new FileStructure(dirOfExe);

		//0: Create the game-manager
		GameManager manager = new GameManager();
		//1: Create the asset-manager
		AssetManager assets = new AssetManager(manager, structure);
		//2: Create the Game-Engine (MonoGame Wrapper)
		GameEngine engine = new GameEngine(ref manager);
		//3: Create the Game-Logic
		GameLogic logic = new SampleGameLogic(manager);
		//4: Create the Game-State
		GameStateManager gameState = new GameStateManager(manager);
		//5: Create the Game-Renderer
		GameRenderer renderer = new GameRenderer(manager);

		engine.Start();
		while(engine != null) {
			if(engine.CanExit)
				engine.Exit();
		}

		System.Console.WriteLine("Application exited cleanly. - Press any key to finish -");
		System.Console.ReadKey();
	}
}
}

namespace MauronAlpha.MonoGame.Actuals {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	public class SampleGameLogic :GameLogic {

		public SampleGameLogic(GameManager manager): base(manager) {}

		public override void SetStartUpScene() {
			if(!Game.Assets.HasDefaultFont)
				throw new GameError("No default font loaded!", this);

			I_GameScene scene = new Scene_BasicShape(Game);
			scene.Initialize();
		}

		public override void Cycle(long time) {
			I_GameScene scene = Game.Renderer.CurrentScene;
			scene.RunLogicCycle(time);
		}

	}
}