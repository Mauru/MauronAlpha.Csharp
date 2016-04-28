using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Quantifiers.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Population:EntityComponent {

		Site Location;
		EntityValue<T_Time> TimeFounded;
		Crowd Members;

		public Population(Site site, EntityValue<T_Time> time) : base() {
			Members = new Crowd(time);
			Location = site;
			TimeFounded = time;
		}


	}

}
