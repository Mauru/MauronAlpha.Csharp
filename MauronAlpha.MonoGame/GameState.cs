
namespace MauronAlpha.MonoGame {
	using MauronAlpha.FileSystem.Units;
	
	public abstract class GameState:MonoGameComponent {

		bool B_isBusy = false;
		public bool IsBusy {
			get { return B_isBusy; }
		}

		public GameState(GameManager game): base() {
			DATA_game = game;
		}

		GameManager DATA_game;
		public GameManager Game { get { return DATA_game; } }

		public Directory GetDataDirectory(GameStateSubSet subSet) {
			return DATA_game.Content.SaveDirectory;
		}
		public string SaveGameExtension(GameStateSubSet s) {
			return "msv";
		}
	}

	public class GameStateSubSet:MonoGameComponent {

		string STR_name;
		public string Name { get { return STR_name; } }

		public GameStateSubSet(GameState parent, string name): base() {
			STR_name = name;
			DATA_gameState = parent;
		}

		GameState DATA_gameState;
		public GameManager Game { get { return DATA_gameState.Game; } }

		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }

		public bool Equals(GameStateSubSet other) {
			return Name.Equals(other.Name);
		}

		File DATA_file;
		public File File {
			get {
				if(DATA_file == null)
					DATA_file = new File(DATA_gameState.GetDataDirectory(this), STR_name, DATA_gameState.SaveGameExtension(this));
				return DATA_file;
			}
		}
	}
	public class DataRequest :MonoGameComponent {

		string resultAsString;
		long resultAsLong = 0;		

	}

}
