using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Relation:EntityComponent {

		public Relation(EntityComponent target, EntityComponent source, EntityValue<T_Time> time, RelationType type)	: base() {
			TimeCreated = time;
			Target = target;
			Source = source;
			Type = type;
		}

		EntityValue<T_Time> TimeCreated;

		EntityComponent Target;
		EntityComponent Source;

		EntityValue<T_Relation> Strength = new EntityValue<T_Relation>(true);

		Modifiers Modifiers = new Modifiers();

		private bool B_isStatic;
		public bool IsStatic {
			get {
				return B_isStatic;
			}
		}

		public RelationType Type;

	}

}
