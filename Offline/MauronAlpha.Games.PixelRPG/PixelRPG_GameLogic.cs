using MauronAlpha.GameEngine;

namespace MauronAlpha.Games.PixelRPG {
	
	//Logic for the PixelRPG
    public class PixelRPG_GameLogic : GameLogic {

		//constructor
		public PixelRPG_GameLogic ( ) { }

		//The GameEngine
		public Game GameEngine;
        public override Game Game {
			get{
				return this.GameEngine;
			}
		}

		// Draw Layers
		public Drawable Stage;
		public Drawable GUI;

		//Initialize
		public override void Initialize (Game game) {
			MauronCode.Debug("Game Initializing", this);
			GameEngine=game;
			InitializeLayers();
		}

		private void InitializeLayers(){
			Stage = new Drawable_layer(GameEngine, "Stage");
			GUI = new Drawable_layer(GameEngine, "GUI");
		} 

		//Gather a List of all Assets required for this Game
		public override void Prepare ( ) {
			GameEngine.AssetManager.Load(PixelRPG_Assets.Assets);
		}

		//Start the logic
		public override void Start ( ) {
			MauronCode.Debug("Game started",this);
			Started=true;
			//SetupGrid();
			SetupConsole();
			Console.Write("Test");			
		}

		//callbacks
		public override void Callback (string message, object source) {
			//assets loaded, start game
			if( message=="LoadQueueComplete"&&!Started ) {
				Start();
			}
		}

		//set up the console
		GameConsole Console;
		public void SetupConsole() {
			Console = new GameConsole(Stage,this);
			GUI.AddChild(Console);

		}

		//Hex Grid
		public HexGrid Grid;
		public HexFieldData HexStyle=new HexFieldData();

		//setup the game grid
		public void SetupGrid(){
			HexStyle.Style=new HexFieldStyle(20);
			Grid=new HexGrid(Stage, 3, 1, HexStyle.Instance);
			MauronCode.Debug(HexStyle.Style.Size.ToString(),this);
			for( int i=0; i<Grid.Fields.Length; i++ ) {
				HexField f=Grid.Fields[i];
				if( f==null ) {
					MauronCode.Debug("WTF! Null Reference",this);
					return;
				}
			}
			Debug("Generated grid",this);
		}
    
	}

}