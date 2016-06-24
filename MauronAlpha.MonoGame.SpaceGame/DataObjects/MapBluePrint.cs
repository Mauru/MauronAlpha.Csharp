using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;

	public class MapGenerationRules : GameRules {

		public MapGenerationRules() : base() { }

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

	}

	public class PlayerSpeciesDefinition : GameComponent, I_SpeciesDefinition {
		BaseStats DATA_BaseStats = new BaseStats();
		public SpeciesStats BaseStats {
			get { return DATA_BaseStats; }
		}
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	public interface I_Habitable {

		MoveData MovementDataFor(GameLocation location);
		GameLocation Location { get; }
	}

}
