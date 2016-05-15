using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Utility {
	
	public static class MemoryMaker:EntityComponent {

		public static Memory LearnedTrait(Being source, Trait trait, Location location, EntityValue<T_Time> time) {
			Memory result = new Memory(MemoryType.LearnedTrait, source, trait, location, time);
			return result;
		}

		public static Memory SufferedCondition(Being source, Condition condition, Location location, EntityValue<T_Time> time) {
			Memory result = new Memory(MemoryType.SufferedCondition, source, condition, location, time);
			return result;
		}

	}
}
