namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	public class Duration : GameValue<T_Duration> {

		public GameClock Clock;
		public GameTime Start;
		public GameTime End;

	}
	public class GameTime : GameValue<T_GameTime> { }
	public class T_GameTime : ValueType {
		public override GameName Name {
			get { return new GameName("GameTime"); }
		}
	}
	public class T_Duration : ValueType {

		public override GameName Name {
			get { return new GameName("Duration"); }
		}
	}

	public class T_GameTimeHour : ValueType {

		public override GameName Name {
			get {
				return new GameName("GameTimeHour");
			}
		}
	}
	public class T_GameTimeDay : ValueType {
		public override GameName Name {
			get {
				return new GameName("GameTimeDay");
			}
		}
	}
	public class T_GameTimeMonth : ValueType {
		public override GameName Name {
			get {
				return new GameName("GameTimeMonth");
			}
		}
	}
	public class T_GameTimeYear : ValueType {
		public override GameName Name {
			get {
				return new GameName("GameTimeYear");
			}
		}
	}
	public class T_GameTimeMinute : ValueType {
		public override GameName Name {
			get {
				return new GameName("GameTimeMinute");
			}
		}
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

	public class GameTurn:GameComponent {
		public GameTime Time;
		public TurnOrder TurnOrder;
		public TurnPhases Phases;
	}
	public class GameClock : GameComponent {
		public GameClock() : base() { 
			DATA_time = new GameTime();		
		}
		
		private GameTime DATA_time;
		public GameTime Time {
			get { return DATA_time; }
		}
		public Duration SimulationDebt = new Duration();
	}
	public class GameParty : GameComponent {
		public bool IsAi;
	}
	public class TurnOrder : GameList<GameParty> { }
	public class TurnPhase : GameComponent { }
	public class TurnPhases : GameList<TurnPhase> { }

}
