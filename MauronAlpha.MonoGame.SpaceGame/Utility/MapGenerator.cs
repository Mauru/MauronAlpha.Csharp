using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
//using MauronAlpha.MonoGame.SpaceGame.Collections;

using MauronAlpha.MonoGame.HexGrid.Units;

namespace MauronAlpha.MonoGame.SpaceGame.Utility {

	public class MapGenerator : GameComponent {

		Map Map;
		MapBluePrint Rules;
		Universe Universe;
		Planet StartingPlanet;

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

			StartingPlanet = new Planet(rules.StartingPlanetName, location, rules.StartingPlanetSize, rules.StartingPlanetDistanceFromCentre);
			StarSystem system = location.System;
			system.AddHabitable(StartingPlanet);
			return StartingPlanet;

		}


	}

}
