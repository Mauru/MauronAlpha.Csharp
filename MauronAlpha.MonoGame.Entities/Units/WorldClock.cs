using MauronAlpha.MonoGame.Entities.Quantifiers;

namespace MauronAlpha.MonoGame.Entities.Units {
	
	public class WorldClock:EntityComponent {

		public float AsFloat = 0;

		public WorldTime CurrentTime {
			get {
				return new WorldTime(this);
			}
		}

	}
}
