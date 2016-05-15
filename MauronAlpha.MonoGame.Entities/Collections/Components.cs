using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Inventory.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Components:InventoryComponent {

		MauronCode_dataList<Item> Items = new MauronCode_dataList<Item>();
	}
}
