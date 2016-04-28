using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	public class Site:EntityComponent {

		Crowd Population;
		EntityValue<T_Time> TimeFounded;

		string STR_name;
		public string Name { get { return STR_name; } }

		public Site(string name, EntityValue<T_Time> time) : base() {
			TimeFounded = time;
			STR_name = name;
		}


	}
}
