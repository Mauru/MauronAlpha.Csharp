using System;

namespace MauronAlpha.GameEngine.Events {

	//A Timeunit in the game
	public class GameTick : GameComponent, IComparable<double>, IComparable<GameTick>, IComparable<int>, ICloneable {

		//The Time (ticks of a System.TimeSpan)
		private TimeSpan TS_time=new TimeSpan(0);
		public TimeSpan Time { get { return TS_time; } set { throw new GameCodeError("Use SetTime()", this); } }
		private void SetTime (long time) { TS_time=new TimeSpan(time); }
		private void SetTime (GameTick time) { TS_time=time.Time; }
		private void SetTime (TimeSpan time) { TS_time=time; }

		//What clock this tick runs on
		private GameEventClock GEC_clock=null;
		public GameEventClock Clock {
			get {
				if( GEC_clock==null ) {
					throw new GameCodeError("Clock can't be null", this);
				}
				return GEC_clock;
			}
			set {
				throw new GameCodeError("Use SetClock", this);
			}
		}
		private void SetClock (GameEventClock clock) {
			GEC_clock=clock;
		}

		//constructors
		public GameTick (GameEventClock clock, TimeSpan time) { SetClock(clock); SetTime(time); }
		public GameTick (GameEventClock clock, GameTick time) { SetClock(clock); SetTime(time); }
		public GameTick (GameEventClock clock, long time) { SetClock(clock); SetTime(time); }

		//Advance the timespan in the gametick
		public GameTick Forward (double time) {
			TimeSpan t=new TimeSpan((long) time);
			TS_time=Time.Add(t);
			return CloneMe();
		}
		public GameTick Forward ( ) {
			TS_time=Time.Add(new TimeSpan(1));
			return CloneMe();
		}

		//Make Tick comparable to other stuff
		public int CompareTo (double other) {
			TimeSpan t=new TimeSpan((long) other);
			if( Time<t ) { return 1; }
			if( Time==t ) { return 0; }
			return -1;
		}
		public int CompareTo (GameTick other) {
			if( Time<other.Time ) { return 1; }
			if( Time==other.Time ) { return 0; }
			return -1;
		}
		public int CompareTo (int other) {
			return CompareTo((double) other);
		}

		//Clone Tick
		public GameTick CloneMe ( ) {
			return new GameTick(Clock, Time);
		}

		public object Clone ( ) {
			return CloneMe();
		}


	}

}
