using MauronAlpha.MonoGame.Scripts;
using MauronAlpha.MonoGame.Actors;
using MauronAlpha.MonoGame.Resources;

using Microsoft.Xna.Framework.Content;

namespace MauronAlpha.MonoGame.Utility {
	
	
	public class GameEngine:MonoGameComponent {

		GameManager Manager = new GameManager();
		MonoGameWrapper Process;
		ContentManager Content { get { return Process.Content; } }

		GameObjectManager Objects { get { return Manager.Objects; } }
		GameLogic Logic { get { return Manager.Logic; } }

		ResourceManager Resources { get { return Manager.Resources; } }


		GameStage Stage;

		private bool B_initialized = false;
		public bool Initialized { get { return B_initialized; }}

		public GameEngine(MonoGameWrapper process, GameLogic logic) : base() {
			Manager.Process = process;
			Manager.Logic = logic;
			Manager.Engine = this;
		}

		public void InitializeContent() {
			Manager.Renderer = Process.Renderer;
			Manager.Objects = new GameObjectManager(Manager);
			Manager.Resources = new ResourceManager(Manager);

			//Generating a font
			GameFontStyle fontStyle = new GameFontStyle("Arial",14);
			GameFont defaultFont = new GameFont("Arial","Arial14.spritefont",fontStyle);
			Resources.RegisterFont(defaultFont);
			Resources.SetDefaultFont("Arial14.spritefont");

			Logic.Initialize(Manager);
		}

		//Load content
		public void PrepareStartUpAssetts() {
			//Stage
			Stage = new GameStage(Manager);
			Stage.SetSize(Process.WindowSize);
			GameText text = new GameText(Stage.NewLevel, Manager, "HEY THERE", Resources.Font("Arial14.spritefont"));
		}

		public ContentManager ContentLoader { get { return Process.Content; } }



	}


}
