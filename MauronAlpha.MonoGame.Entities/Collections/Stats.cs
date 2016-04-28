using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Entities.Collections {

	public class Stats:EntityComponent {

		public static Attribute Intelligence {
			get {
				return new Attribute("Intelligence");
			}
		}
	}
}
