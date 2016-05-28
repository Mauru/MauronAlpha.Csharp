using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Species:GameComponent {

		public GameName Name; 

		public Species(GameName name, SpeciesStats statistics) : base() {
			Name = name;
			Stats = statistics;
		}

		public SpeciesStats Stats;
	}

}
