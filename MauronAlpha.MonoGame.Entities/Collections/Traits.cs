using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Traits:EntityComponent {
		public MauronCode_dataList<Trait> Members = new MauronCode_dataList<Trait>();
		public MauronCode_dataMap<EntityValue<T_Time>> TimeLearned = new MauronCode_dataMap<EntityValue<T_Time>>();

		public void Add(Trait unit, EntityValue<T_Time> timeLearned) {
			if (Members.ContainsValue(unit))
				return;
			TimeLearned.SetValue(unit.Name, timeLearned);
		}
		public Trait ByName(string name) {
			foreach (Trait candidate in Members) {
				if (candidate.Name == name)
					return candidate;
			}
			return Trait.Default;
		}
	}
}
