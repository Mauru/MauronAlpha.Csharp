using MauronAlpha.MonoGame.Entities.Units;
using MauronAlpha.MonoGame.Entities.Quantifiers;

namespace MauronAlpha.MonoGame.Entities.Collections {
	
	public class Population:Hierarchy {

		Site Location;
		WorldTime TimeFounded;

		public Population(Site site) : base() {
			Location = site;
			TimeFounded = site.Clock.CurrentTime;
		}


	}

}
