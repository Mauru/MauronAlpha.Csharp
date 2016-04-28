using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class ModifierActions:EntityComponent {
		MauronCode_dataList<ModifierAction<EntityValueType, EntityValueType>> Members = new MauronCode_dataList<ModifierAction<EntityValueType, EntityValueType>>();
	}

}
