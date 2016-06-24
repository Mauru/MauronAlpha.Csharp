namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	
	public class GameState:GameComponent {

		GameRules DATA_rules;
		public GameState(GameRules masterRules)	: base() {
			DATA_rules = masterRules;
		}
		private GameClock Clock = new GameClock();
	
	}

}
