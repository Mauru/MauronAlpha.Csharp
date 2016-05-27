namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class StarSystem:GameComponent {

		public GameList<Orbital> Orbitals;

		public Galaxy Galaxy;

		public StarSystem(Galaxy galaxy):base() {
			Galaxy = galaxy;
		}
	
	}
}
