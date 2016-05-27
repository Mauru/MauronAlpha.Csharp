using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.Utility {
	
	public class SpeciesGenerator:GameComponent {


		public static Species GenerateStartingSpecies(MapBluePrint rules) {

			Species result = new Species(rules.StartingSpeciesName,rules.StartingStatistics);

			return result;

		}

	}
}
