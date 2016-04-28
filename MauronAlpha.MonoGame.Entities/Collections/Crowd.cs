using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Crowd:EntityComponent {

		public Crowd(string name, EntityValue<T_Time> time)	: this(time) {
			STR_name = name;
		}
		public Crowd(EntityValue<T_Time> time)
			: base() {
			DATA_TimeCreated = time;
		}

		public MauronCode_dataMap<EntityValue<T_Time>> TimeAdded = new MauronCode_dataMap<EntityValue<T_Time>>();

		EntityValue<T_Time> DATA_TimeCreated;
		public EntityValue<T_Time> TimeCreated {
			get {
				if(DATA_TimeCreated == null)
					return new EntityValue<T_Time>(true);
				return DATA_TimeCreated;
			}
		}
		MauronCode_dataList<Group> Members = new MauronCode_dataList<Group>();

		public void Add(Group entity, EntityValue<T_Time> time) {
			if (TimeAdded.ContainsKey(entity.Id))
				return;
			Members.Add(entity);
			TimeAdded.SetValue(entity.Id, time);
		}
		public bool Contains(Group entity) {
			foreach (Group candidate in Members)
				if (candidate.Id == entity.Id)
					return true;
			return false;
		}
		public Group ById(string id) {
			foreach (Group candidate in Members)
				if (candidate.Id == id)
					return candidate;
			return Group.DoesNotExist;
		}

		public bool WasMember(Group member) {
			return TimeAdded.ContainsKey(member.Id);
		}

		string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}
		public void SetName(string name) {
			STR_name = name;
		}


	}
}
