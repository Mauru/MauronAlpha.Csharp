using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {

	public class MapBluePrint : RuleSet, I_RuleSet<RuleSet_MapGenerator> {

		public MapBluePrint() : base(RuleSet_MapGenerator.Instance) { }

		/*public override RuleSetType Type {
			get { return new RuleSet_MapGenerator(); }
		}*/

		public GameName StartingSpeciesName {
			get {
				return new GameName("StartingSpecies");
			}
		}
		public GameName StartingPlanetName {
			get {
				return new GameName("StartingPlanet");
			}
		}
		public GameValue<T_PlanetSize> StartingPlanetSize {
			get {
				return new GameValue<T_PlanetSize>();
			}
		}
		public GameValue<T_DistanceFromStarCentre> StartingPlanetDistanceFromCentre {
			get {
				return new GameValue<T_DistanceFromStarCentre>();
			}
		}

		public SpeciesStats StartingStatistics {
			get {
				return new SpeciesStats();
			}
		}

		public I_SpeciesDefinition PlayerSpeciesDefinition {
			get { return new PlayerSpeciesDefinition(); }
		}

		public override RuleSetType Type {
			get { return new RuleSet_MapGenerator(); }
		}

		RuleSet_MapGenerator I_RuleSet<RuleSet_MapGenerator>.Type {
			get { return new RuleSet_MapGenerator(); }
		}
	}

	public class RuleSet_MapGenerator:RuleSetType { 
		
		public static RuleSet_MapGenerator Instance { 
			get {
				return  new RuleSet_MapGenerator();
			}
		}
	}

	public class PlayerSpeciesDefinition : GameComponent, I_SpeciesDefinition { }

}
