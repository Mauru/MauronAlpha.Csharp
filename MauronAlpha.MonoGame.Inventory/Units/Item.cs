using MauronAlpha.MonoGame.Inventory.Collections;
using MauronAlpha.MonoGame.Inventory.Quantifiers;
using MauronAlpha.MonoGame.Quantifiers.Units;


namespace MauronAlpha.MonoGame.Inventory.Units {

	public class Item:InventoryComponent {

		Components Components = new Components();

		public EntityValue<T_Weight> Weight;
		public EntityValue<T_Bulk> Bulk;

	}

}
