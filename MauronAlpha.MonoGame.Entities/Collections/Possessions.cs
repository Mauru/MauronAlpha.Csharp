using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Inventory.Collections;

namespace MauronAlpha.MonoGame.Entities.Collections {

	public class Possessions:EntityComponent {
		MauronCode_dataMap<Pile> Members = new MauronCode_dataMap<Pile>();
		MauronCode_dataMap<Location> Locations = new MauronCode_dataMap<Location>();
	}
}
