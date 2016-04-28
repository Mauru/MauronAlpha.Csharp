using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Inventory.Units;

namespace MauronAlpha.MonoGame.Inventory.Collections {
	
	public class Pile:InventoryComponent {

		public int Weight;
		public int Bulk;

		MauronCode_dataList<Item> Items = new MauronCode_dataList<Item>();
	}

}
