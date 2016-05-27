
namespace MauronAlpha.MonoGame.SpaceGame.Units {
	public class Universe:GameComponent {
		Map Map;
		GameList<Galaxy> Galaxies;

		public Universe(Map map):base() {
			Map = map;
		}
	}
}
