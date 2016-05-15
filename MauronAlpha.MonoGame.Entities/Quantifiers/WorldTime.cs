using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Quantifiers {
	
	public class WorldTime:EntityValue<T_Time> {}

	public class Year : EntityValue<T_Duration> { }
	public class Month : EntityValue<T_Duration> { }
	public class Day : EntityValue<T_Duration> { }
	public class Hour : EntityValue<T_Duration> { }

}
