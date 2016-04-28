using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	public class Modifiers:EntityComponent {

		public MauronCode_dataList<Modifier<EntityValueType,EntityValueType>> Members = new MauronCode_dataList<Modifier<EntityValueType,EntityValueType>>();

		public void Add(Modifier<EntityValueType, EntityValueType> member) {
			Members.Add(member);
		}

	}
}
