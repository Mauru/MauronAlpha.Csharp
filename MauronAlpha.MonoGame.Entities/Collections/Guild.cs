using MauronAlpha.MonoGame.Entities.Quantifiers;
using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {


	public class Guild:EntityComponent {

		Group D_members;
		public Group Members {
			get {
				if (D_members != null)
					return D_members;
				if (D_members.Count > 0) {
					return D_members;
				}
				return Group.DoesNotExist;
			}
		}

		Memories D_memories;
		public Memories Memories {
			get {
				if (D_memories == null)
					return Memories.Empty;
				return D_memories;
			}
		}

		public void Add(EntityValue<T_Time> time, Being being) {
			if (D_members == null)
				D_members = new Group(time);
			D_members.Add(being, time);
		}

	}

}
