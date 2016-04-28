using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Units {

	public class Modifier<T_in,T_out> : EntityComponent where T_in:EntityValueType,new() where T_out:EntityValueType,new() {

		public Modifier(ModifierAction<EntityValueType, EntityValueType> action) : base() {
			Action = action;
		}

		ModifierAction<EntityValueType, EntityValueType> Action;
		long NumericValue = 0;

		EntityValue<T_Time> VAL_timeAdded;
		public EntityValue<T_Time> TimeAdded {
			get {
				if (VAL_timeAdded == null)
					return new EntityValue<T_Time>(true);
				return VAL_timeAdded;
			}
		}

		EntityValue<T_Duration> VAL_duration;
		EntityValue<T_Duration> Duration {
			get {
				if (VAL_duration == null)
					return new EntityValue<T_Duration>(true);
				return VAL_duration;
			}
		}
		public void SetDuration(EntityValue<T_Duration> time) {
			VAL_duration = time;
		}
		public void SetTimeAdded(EntityValue<T_Time> time) {
			VAL_timeAdded = time;
		}

		public void SetNumericValue(long value) {
			NumericValue = value;
		}

	}

}
