using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Group:EntityComponent {

		public readonly bool IsNull = false;

		public Group(bool isNull) : base() {
			IsNull = isNull;
		}
		public Group() : this(new EntityValue<T_Time>(true)) { }
		public Group(string name, EntityValue<T_Time> time)	: this(time) {
			STR_name = name;
		}
		public Group(EntityValue<T_Time> time)	: base() {
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
		MauronCode_dataList<Being> Members = new MauronCode_dataList<Being>();

		public static Group DoesNotExist {
			get {
				return new Group(true);
			}
		}

		public void Add(Being entity, EntityValue<T_Time> time) {
			if (entity.IsNull || IsNull)
				return;
			if (TimeAdded.ContainsKey(entity.Id))
				return;
			Members.Add(entity);
			TimeAdded.SetValue(entity.Id, time);
		}
		public bool Contains(Being entity) {
			if (entity.IsNull || IsNull)
				return false;
			foreach (Being candidate in Members)
				if (candidate.Id == entity.Id)
					return true;
			return false;
		}
		public Being ById(string id) {
			foreach (Being candidate in Members)
				if (candidate.Id == id)
					return candidate;
			return Being.DoesNotExist;
		}

		public bool WasMember(Being member) {
			if (member.IsNull || IsNull)
				return false;
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
