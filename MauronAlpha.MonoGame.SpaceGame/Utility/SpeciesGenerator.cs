using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
using MauronAlpha.MonoGame.SpaceGame.Units;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

namespace MauronAlpha.MonoGame.SpaceGame.Utility {
	
	public class SpeciesGenerator:GameComponent {


		public static Species GenerateStartingSpecies(GameRules rules) {

			Species result = new Species(rules.Species.StartingSpeciesName, rules.Species.PlayerSpeciesDefinition);

			return result;

		}

	}
}
