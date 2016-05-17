using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities.Utility

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Plane:EntityComponent {

		WorldClock Clock;

		public Plane(string name, WorldClock clock) : base() {
			Name = new ConceptualName(name);
			Clock = clock;
		}

		public Regions Regions;
		ConceptualName Name;

		public Sites Sites = new Sites();

		public Site NewSite {
			get {
				string generatedName = NameGenerator.Create(NameFormulae.Site);;
				LastUsedRegion.AddToBuffer(new Site(generatedName,Clock))
			}
		}

	}

}
