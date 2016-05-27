using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	
	public class Sector:GameComponent {

		public GameList<Structure> Structures;
		public I_Habitable Parent;
		public Population Population;

		public Sector(I_Habitable parent)	: base() {
			Parent = parent;
			Population = new Population(this);
		}

	}

}
