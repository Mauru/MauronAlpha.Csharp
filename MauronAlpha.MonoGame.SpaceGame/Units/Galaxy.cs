namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Galaxy:GameComponent {

		Universe Universe;

		public Galaxy(Universe universe) : base() {
			Universe = universe;
		}

		public GameList<StarSystem> StarSystems;
	}
}
