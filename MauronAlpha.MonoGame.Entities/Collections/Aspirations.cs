using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Quantifiers;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Aspirations:EntityComponent {

		public Aspirations() : base() {}

		public MauronCode_dataMap<EntityValue<T_Time>> TimeAdded = new MauronCode_dataMap<EntityValue<T_Time>>();
		public MauronCode_dataMap<EntityValue<T_Urge>> Priority = new MauronCode_dataMap<EntityValue<T_Urge>>();
		MauronCode_dataList<Aspiration> Members = new MauronCode_dataList<Aspiration>();

		public void Add(Aspiration entity, EntityValue<T_Time> time, EntityValue<T_Urge> priority) {
			if (entity.IsNull)
				return;
			if (TimeAdded.ContainsKey(entity.Id))
				return;
			Members.Add(entity);
			TimeAdded.SetValue(entity.Id, time);
			Priority.SetValue(entity.Id, priority);
		}
		public bool Contains(Aspiration entity) {
			if (entity.IsNull)
				return false;
			foreach (Aspiration candidate in Members)
				if (candidate.Id == entity.Id)
					return true;
			return false;
		}
		public Aspiration ById(string id) {
			foreach (Aspiration candidate in Members)
				if (candidate.Id == id)
					return candidate;
			return Aspiration.DoesNotExist;
		}

		public bool WasMember(Aspiration member) {
			if (member.IsNull)
				return false;
			return TimeAdded.ContainsKey(member.Id);
		}
	}

}
