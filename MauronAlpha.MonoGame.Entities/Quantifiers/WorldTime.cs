using MauronAlpha.MonoGame.Quantifiers.Units;
using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Entities.Quantifiers {
	
	public class WorldTime:EntityValue<T_Time> {

		WorldClock Clock;

		public WorldTime() : base(true) { }
		public WorldTime(WorldClock clock)	: base(clock.AsFloat) {
			Clock = clock;
		}

	}

	public class Year : EntityValue<T_Duration> { }
	public class Month : EntityValue<T_Duration> { }
	public class Day : EntityValue<T_Duration> { }
	public class Hour : EntityValue<T_Duration> { }

}
