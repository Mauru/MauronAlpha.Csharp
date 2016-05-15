using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Pile:InventoryComponent {

		public int Weight;
		public int Bulk;

		MauronCode_dataList<Item> Items = new MauronCode_dataList<Item>();
	}

}
