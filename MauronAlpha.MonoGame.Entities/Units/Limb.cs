using MauronAlpha.MonoGame.Entities.Quantifiers;
using MauronAlpha.MonoGame.Entities.Collections;

namespace MauronAlpha.MonoGame.Entities.Units {

	public class Limb : EntityComponent {

		public string Name;

		T_FoodValue FoodValue;

		public bool CanGrasp;
		public T_Weight MaxWeight;
		public T_Bulk MaxBulk;

		public Actions Actions;

		public bool CanEmit;

		public bool IsMobility;

		public bool IsMind;

		public bool IsCritical;

		Modifiers Modifiers;

	}

}
