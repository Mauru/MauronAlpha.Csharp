using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities.Quantifiers;
using MauronAlpha.MonoGame.Quantifiers.Units;


namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Memory:EntityComponent {

		WorldTime TimeCreated;

		EntityComponent Target;
		EntityComponent Source;

		Location Location;

		EntityValue<T_Memory> Strength;
		Modifiers Modifiers;

		Crowd Witnesses;

		Motives Motives;

		public Memory(MemoryType type, EntityComponent source, EntityComponent target, Location location, EntityValue<T_Time> time)	: base() {
			Source = source;
			Target = target;
			Location = location;
			TimeCreated = time;
		}
	
	}

	public abstract class MemoryType : EntityComponent {
		public abstract string Name { get; }

		public static MemoryType LearnedTrait {
			get {
				return new M_LearnedTrait();
			}
		}
		public static MemoryType SufferedCondition {
			get {
				return new M_SufferedCondition();
			}
		}
	}

	public class M_LearnedTrait : MemoryType {
		public override string Name { get { return "LearnedTrait"; } }
	}
	public class M_SufferedCondition : MemoryType {
		public override string Name { get { return "SufferedCondition"; } }
	}

}
