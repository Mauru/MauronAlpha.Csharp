using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities.Quantifiers;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class Site:EntityComponent {

		public WorldClock Clock;

		Population Population;
		Group Visitors;
		SGroup<Clan> Clans;
		SGroup<Army> Armies;
		SGroup<Guild> Guilds;
		SGroup<Cult> Cults;

		Geography Geography;

		Memories History;

		WorldTime TimeFounded;

		string STR_name;
		public string Name { get { return STR_name; } }

		public Site(string name, WorldClock clock) : base() {
			STR_name = name;
			Clock = clock;
			TimeFounded = clock.CurrentTime;
		}

		public Geography SetGeography(Geography data) {
			Geography = data;
		}
	}
}
