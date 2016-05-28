using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
using MauronAlpha.MonoGame.SpaceGame.Units;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	
	public class ResourceAmount : GameComponent {

		public static ResourceAmount GameResource(ResourceType resourceType, int level) {
			return new ResourceAmount();
		}

		public static ResourceAmount StartingPopulationByPlanetLevel(ResourceAmount level, I_Habitable planet) {
			return new ResourceAmount();
		}

		public static ResourceAmount ResearchForEnergyCost {
			get {
				return new ResourceAmount();
			}
		}
		public static ResourceAmount ResearchForEnergyYield {
			get {
				return new ResourceAmount();
			}
		}
		public static ResourceAmount ResearchForEnergyScientistAmount {
			get {
				return new ResourceAmount();
			}
		}
		public static ResourceAmount FoodPerTurn(Structure structure) {
			return new ResourceAmount();
		}
		public static ResourceAmount EnergyPerTurn(Structure structure) {
			return new ResourceAmount();
		}

	}

}
