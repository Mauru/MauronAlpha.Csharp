namespace MauronAlpha.GameEngine {
	
	//Base class for a game's logical functions
    public abstract class GameLogic : GameComponent {
		public abstract string Name { get; }

		public GameLogic() {}
		
		public abstract MauronAlphaGame Game { get;}

		public bool Started = false;

		//Pre-Game Actions
		public abstract void Prepare();

		//Start the Logic
		public abstract void Start();

		//Callback Function
		public abstract void Callback(string message, object source);

		public abstract void Initialize (MauronAlphaGame game);
    }

}
