using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	
	public class GameTurn:GameComponent {

		public GameTime Time;
		public TurnOrder TurnOrder;
		public TurnPhases Phases;

	}

	public class GameClock : GameComponent {

		public GameTime Time;

	}

	public class GameTime : GameValue<T_gameTime> { }

	public class T_gameTime : ValueType {

		public override GameName Name {
			get { return new GameName("Time"); }
		}
	}

	public class Duration : GameValue<T_duration> {

		public GameClock Clock;
		public GameTime Start;
		public GameTime End;

	}

	public class T_duration : ValueType {

		public override GameName Name {
			get { return new GameName("Duration"); }
		}
	}

	public class GameParty : GameComponent {
		public bool IsAi;
	}
	public class TurnOrder : GameList<GameParty> { }
	public class TurnPhase : GameComponent { }
	public class TurnPhases : GameList<TurnPhase> { }

}
