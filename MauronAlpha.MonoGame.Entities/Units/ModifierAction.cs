using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Units {

	public class ModifierAction<T_in,T_out> : EntityComponent where T_in:EntityValueType,new() where T_out:EntityValueType,new(){

		public EntityValue<T_Time> Duration = new EntityValue<T_Time>(true);
		public virtual EntityValue<T_out> Perform(EntityValue<T_out> old, EntityValue<T_in> influence) {
			return old;
		}

		public static MOD_add<T_in, T_out> Increase {
			get {
				return new MOD_add<T_in,T_out>();
			}
		}
	}

	public class MOD_add<T_in, T_out> : ModifierAction<T_in, T_out>  where T_in:EntityValueType,new() where T_out:EntityValueType,new() {
		public override EntityValue<T_out> Perform(EntityValue<T_out> old, EntityValue<T_in> influence) {
			old.SetNumericValue(old.ValueAsNumeric + influence.ValueAsNumeric);
			return old;
		}
	}
}
