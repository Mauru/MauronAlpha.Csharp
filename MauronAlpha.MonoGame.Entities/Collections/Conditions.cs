using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Conditions:EntityComponent {

		MauronCode_dataList<Condition> Members = new MauronCode_dataList<Condition>();
		MauronCode_dataMap<EntityValue<T_Time>> TimeCreated = new MauronCode_dataMap<EntityValue<T_Time>>();

		public void Add(Condition condition, EntityValue<T_Time> time) {
			Members.Add(condition);
			TimeCreated.SetValue(condition.Id, time);
		}
	}
}
