namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class MapAction:GameComponent {

		public BuildingComponent Cost;
		public BuildingComponent Yield;

	}

	public class GameTurn : GameComponent {

		public GameList<TurnPhase> Phases;

	}

	public class TurnPhase : GameComponent { }


}
