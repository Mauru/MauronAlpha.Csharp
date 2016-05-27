using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Collections;

using MauronAlpha.MonoGame.HexGrid.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Utility {

	public class MapGenerator : GameComponent {

		Map Map;
		MapBluePrint Rules;
		Universe Universe;

		public void Start(MapBluePrint bluePrint) {
			Rules = bluePrint;
			Map = new Map(this);
			Universe = new Universe(Map);
			Galaxy startGalaxy = new Galaxy(Universe);
			StarSystem startSystem = new StarSystem(startGalaxy);

			Hex startHex = Map.Grid.Start;
			MapLocation startLocation = new MapLocation(startHex, startSystem);
			Map.Register(startLocation);

			//Create the starting Faction
			Species playerSpecies = SpeciesGenerator.GenerateStartingSpecies(Rules);
			Planet StartingPlanet = GenerateStartingPlanet(Rules, startLocation, playerSpecies);

		}

		public Planet GenerateStartingPlanet(MapBluePrint rules, MapLocation location, Species species) {

			Planet result = new Planet(rules.StartingPlanetName, location);

		}


	}

	public class MapBluePrint : GameComponent {

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

		public SpeciesStats StartingStatistics {
			get {
				return new SpeciesStats();
			}
		}
	}

}
