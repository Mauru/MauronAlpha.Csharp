namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Population:GameComponent {

		public Sector Sector;
		public Population(Sector parent):base() {
			Sector = parent;
		}

	}
}
