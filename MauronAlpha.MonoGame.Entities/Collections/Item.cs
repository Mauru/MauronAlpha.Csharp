using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities.Quantifiers;
using MauronAlpha.MonoGame.Quantifiers.Units;


namespace MauronAlpha.MonoGame.Entities.Units {

	public class Item:InventoryComponent {

		GeneratedName Name;

		Location Location;
		Components Components = new Components();

		public EntityValue<T_Weight> Weight;
		public EntityValue<T_Bulk> Bulk;

	}

}
