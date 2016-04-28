using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	
	public class Aspiration:EntityComponent {

		MemoryType Type;
		EntityValue<T_Time> TimeCreated;
		EntityComponent Target;

		public readonly bool IsNull = false;

		public Aspiration(bool isNull) : base() {
			IsNull = isNull;
		}
		public Aspiration(MemoryType type, EntityComponent target, EntityValue<T_Time> time) : this(false) {
			Type = type;
			TimeCreated = time;
			Target = target;
		}

		public static Aspiration DoesNotExist {
			get {
				return new Aspiration(true);
			}
		}
	}

}
