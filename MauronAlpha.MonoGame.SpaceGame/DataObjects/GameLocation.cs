
namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Units;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Actuals;

	//A DataObject which shares Point of view and the nature of a universe
	public class RealmInfo : GameComponent {
		public RealmInfo(Realm realm) : base() { }
	}

	public class GridPosition<T> : GameComponent where T : GridType { }

	public abstract class GridType : GameComponent {
		public abstract GameName Name { get; }
		public abstract ActionLayer ActionLayer { get; }
	}

	public class GameRules : GameComponent {
		public GameRules() : base() { }

		GameState DATA_GameState;
		public GameState GameState {
			get {
				if (DATA_GameState == null)
					DATA_GameState = new GameState(this);
				return DATA_GameState;
			}
		}

		RuleSet_Location DATA_location;
		public RuleSet_Location Locations {
			get {
				if (DATA_location == null)
					DATA_location = new RuleSet_Location(this);
				return DATA_location;
			}
		}

		RuleSet_Species DATA_species;
		public RuleSet_Species Species {
			get {
				if (DATA_species == null)
					DATA_species = new RuleSet_Species(this);
				return DATA_species;
			}
		}

	}

	//Presents a location in the game
	public class GameLocation : GameComponent {

		Map DATA_map;
		public Map Map {
			get {
				return DATA_map;
			}
		}

		Realm DATA_realm;
		public Realm Realm {
			get {
				if (DATA_realm == null)
					DATA_realm = SOLVE_Realm(this);
				return DATA_realm;
			}
		}

		Universe DATA_universe;
		Universe Universe {
			get {
				return DATA_universe;
			}
		}

		Galaxy DATA_galaxy;
		Galaxy Galaxy {
			get {
				return DATA_galaxy;
			}
		}

		StarSystem DATA_starSystem;
		StarSystem StarSystem {
			get {
				return DATA_starSystem;
			}
		}

		Orbital DATA_orbital;
		Orbital Orbital {
			get {
				return DATA_orbital;
			}
		}

		OrbitalLayer DATA_orbitalLayer;
		OrbitalLayer OrbitalLayer {
			get {
				return DATA_orbitalLayer;
			}
		}

		Sector DATA_sector;
		Sector Sector {
			get {
				return DATA_sector;
			}
		}

		Site DATA_site;
		Site Site {
			get {
				return DATA_site;
			}
		}

		Section DATA_section;
		Section Section {
			get {
				return DATA_section;
			}
		}

		Level DATA_level;
		Level Level {
			get {
				return DATA_level;
			}
		}

		Position DATA_position;
		Position Position {
			get {
				return DATA_position;
			}
		}

		private bool B_isNull = false;
		public bool IsNull { get { return B_isNull; } }

		public GameLocation(bool isNull): base() {
			B_isNull = isNull;
		}
		public GameLocation(Position position): this(false) {
			DATA_position = position;
		}
		public GameLocation(Level level): this(false) {
			DATA_level = level;
		}
		public GameLocation(Section section): this(false) {
			DATA_section = section;
		}
		public GameLocation(Site site)
			: this(false) {
			DATA_site = site;
		}
		public GameLocation(Sector sector)
			: this(false) {
			DATA_sector = sector;
		}
		public GameLocation(OrbitalLayer layer)
			: this(false) {
			DATA_orbitalLayer = layer;
		}
		public GameLocation(Orbital orbital)
			: this(false) {
			DATA_orbital = orbital;
		}
		public GameLocation(Map map)
			: this(false) {
			DATA_map = map;
		}
		public GameLocation(Realm realm)
			: this(false) {
			DATA_realm = realm;
		}
		public GameLocation(Universe universe)
			: this(false) {
			DATA_universe = universe;
		}
		public GameLocation(Galaxy galaxy)
			: this(false) {
			DATA_galaxy = galaxy;
		}
		public GameLocation(StarSystem system)
			: this(false) {
			DATA_starSystem = system;
		}
		public GameLocation() : this(true) { }

		public GameLocation Set(Map u) {
			DATA_map = u;
			B_isNull = false;
			return this;
		}
		public GameLocation Set(Realm u) {
			DATA_realm = u;
			B_isNull = false;
			return this;
		}

		public GameLocation Location {
			get { return this; }
		}

		public ActionLayer LowestKnown {
			get {
				return Lowest.ActionLayer;
			}
		}
		public LocationObject Lowest {
			get {
				if (DATA_position != null)
					return DATA_position;
				if (DATA_level != null)
					return DATA_level;
				if (DATA_section != null)
					return DATA_section;
				if (DATA_site != null)
					return DATA_site;
				if (DATA_orbitalLayer != null)
					return DATA_orbitalLayer;
				if (DATA_orbital != null)
					return DATA_orbital;
				if (DATA_starSystem != null)
					return DATA_starSystem;
				if (DATA_galaxy != null)
					return DATA_galaxy;
				if (DATA_universe != null)
					return DATA_universe;
				if (DATA_realm != null)
					return DATA_realm;
				if (DATA_map != null)
					return DATA_map;
				throw new CriticalGameError("A GameLocation needs at least one object in it!", this, "GameLocation.Lowest");
			}
		}

		public static Realm SOLVE_Realm(GameLocation location) {

			LocationObject source = location.Lowest;
			ActionLayer layer = source.ActionLayer;
			AL_Realm target = SpaceGameActionLayers.Realm;
			if (layer.Equals(target))
				return location.Realm;

			// source is higher up the chain
			if (layer.Order < target.Order)
				return location.Realm;
				
			return source.GameRules.Locations.FindASuitableDefaultRealm(ref location);
		}
	}

	public abstract class RuleSet : GameComponent, I_RuleSet {
		public abstract GameName Name { get; }
	}

	//Defines rules for locations
	public class RuleSet_Location : RuleSet {

		GameRules DATA_masterRules;

		public GameState GameState {
			get { return DATA_masterRules.GameState; }
		}

		public RuleSet_Location(GameRules masterRules): base() {
			DATA_masterRules = masterRules;
		}

		public override GameName Name {
			get { return new GameName("Location"); }
		}

		public GameValue<T_OrbitalLimit> MaxOrbitalsOfSun(Sun sun) {

			return new GameValue<T_OrbitalLimit>(sun.MaxOrbitals.ValueAsInt);

		}
		public OrbitalTypes AllowedChildOrbitalsOfSun(Orbital o) {
			return new OrbitalTypes() {
				OrbitalTypes.Planet,
				OrbitalTypes.Debris,
				OrbitalTypes.Fleet,
				OrbitalTypes.Probe,
			};
		}
		public OrbitalTypes AllowedChildOrbitalsOfPlanetoid(Orbital o) {
			return new OrbitalTypes() {
				OrbitalTypes.Moon,
				OrbitalTypes.Structure,
				OrbitalTypes.Fleet,
				OrbitalTypes.Debris,
				OrbitalTypes.Probe,
			};
		}
		public OrbitalTypes AllowedChildOrbitalsOfDebris(Orbital o) {
			return new OrbitalTypes() {
				OrbitalTypes.Structure,
				OrbitalTypes.Fleet,
				OrbitalTypes.Probe,
			};
		}
		public OrbitalTypes AllowedParentOrbitalsOfDebris(Orbital o) {
			return new OrbitalTypes() {
				OrbitalTypes.Sun,
				OrbitalTypes.Planetoid,
				OrbitalTypes.Probe,
			};
		}
		public OrbitalTypes AllowedParentOrbitalsOfPlanetoid(Orbital o) {
			return new OrbitalTypes() {
				OrbitalTypes.Sun
			};
		}

		public GameValue<T_GravityWeight> GenerateGravityOfSun(Sun sun) {
			return new GameValue<T_GravityWeight>();
		}

		public GameValue<T_DistanceFromOrbitalParent> GenerateNumberOfOrbitals(Sun sun) {
			return new GameValue<T_DistanceFromOrbitalParent>(5);
		}
		public GameValue<T_OrbitalLayerDepth> MaxOrbitalLayersOf(Orbital o) {
			OrbitalType t = o.OrbitalType;
			if(t.Equals(OrbitalTypes.Null))
				return new GameValue<T_OrbitalLayerDepth>(true);
			if (t.Equals(OrbitalTypes.Sun))
				return MaxOrbitalLayersOfSun(o.ClostestSun);
			
		}

		public Sun GenerateSun(StarSystem system) {
			Sun result = new Sun(system, GenerateSunType(system));
			return result;

		}
		public SunType GenerateSunType(StarSystem system) {
			return new SunType_Regular();
		}

		public GameValue<T_StarSystemLimit> MaxStarSystemsOfGalaxy(Galaxy galaxy) {
			return new GameValue<T_StarSystemLimit>(5);
		}

		public RealmInfo GenerateRealmInfo(Realm realm) {
			return new RealmInfo(realm);
		}

		public Realm FindASuitableDefaultRealm(ref GameLocation location) {

			Map map = location.Map;
			Realm result = new Realm(map);
			location.Set(result);
			return result;

		}

		public Universe GenerateDefaultUniverse(Realm realm) {

			Universe result = new Universe(realm);
			return result;
		}
	}

	public abstract class SunType : GameComponent {
		public abstract GameName Name { get; }
	}
	public class SunType_Regular : SunType {
		public override GameName Name {
			get { return new GameName("Regular"); }
		}
	}
	public class SunType_Unstable : SunType {
		public override GameName Name {
			get { return new GameName("Unstable"); }
		}
	}
	public class SunType_BlackHole : SunType {
		public override GameName Name { get { return new GameName("BlackHole"); } }
	}
	public class SunType_SuperNova : SunType {
		public override GameName Name {
			get { return new GameName("SuperNova"); }
		}
	}
	public class SunType_MaelStroem : SunType {
		public override GameName Name { get { return new GameName("MaelStroem"); } }
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	using MauronAlpha.MonoGame.SpaceGame.Units;
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

	public class SunTypes : GameList<SunType> { }

	public class SpaceGameGridTypes : GridTypes {
		public static GridType_StarSystem StarSystem {
			get {
				return new GridType_StarSystem();
			}
		}
	}
	public class GridType_StarSystem : GridType {

		public override GameName Name {
			get { return new GameName("StarSystem"); }
		}

		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.StarSystem; }
		}
		public static GridPosition<GridType_StarSystem> Center {
			get {
				return new GridPosition<GridType_StarSystem>();
			}
		}
	}
	public class GridType_Galaxy : GridType {

		public override GameName Name {
			get { return new GameName("Galaxy"); }
		}

		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Galaxy; }
		}
		public static GridPosition<GridType_Galaxy> Center {
			get {
				return new GridPosition<GridType_Galaxy>();
			}
		}
		public static GridPosition<GridType_Galaxy> GenerateFromUniversePosition(GridPosition<GridType_Universe> position) {
			return new GridPosition<GridType_Galaxy>();
		}
	}
	public class GridType_Universe : GridType {
		public override GameName Name {
			get { return new GameName("Universe"); }
		}

		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Universe; }
		}
		public static GridPosition<GridType_Universe> Center {
			get {
				return new GridPosition<GridType_Universe>();
			}
		}
	}

	public class Map : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Map; }
		}

		internal Assign<GameName, Realm> DATA_Realms;
		public Assign<GameName, Realm> Realms { get { return DATA_Realms; } }

		internal GameRules DATA_GameRules;
		public override GameRules GameRules {
			get { return DATA_GameRules; }
		}
		public Map(GameRules rules)	: base() {
			DATA_GameRules = rules;
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}
	//The universe from a "point of view" - Seperation to contain different DataSets which can overlap when required (Merged modifiers, modifiers which only apply to a faction, GameParty etc
	public class Realm : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Realm; }
		}
		public override GameRules GameRules {
			get {
				return Map.GameRules;
			}
		}
		public override GameLocation Location {
			get { return new GameLocation(this); }
		}

		internal Map DATA_map;
		public Map Map { get { return DATA_map; } }

		public Realm(Map map): base() {
			DATA_map = map;
			DATA_RealmInfo = map.GameRules.Locations.GenerateRealmInfo(this);
		}

		internal Universe DATA_Universe;
		public Universe Universe {
			get {
				if (DATA_Universe == null)
					DATA_Universe = GameRules.Locations.GenerateDefaultUniverse(this);
				return DATA_Universe;
			}
		}
	}
	public class Universe : LocationObject {

		public Universe(Realm realm) : base() { }

		internal Realm DATA_Realm;
		public Realm Realm {
			get { return DATA_Realm; }
		}

		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Universe; }
		}
		public override GameRules GameRules {
			get {
				return DATA_Realm.GameRules;
			}
		}
		public override GameLocation Location {
			get { return new GameLocation(this); }
		}

		internal GridPositionMap<GridType_Universe,Galaxy> DATA_Galaxies;
	}
	public class Galaxy : LocationObject {

		public Galaxy(Universe universe, GridPositions<GridType_Universe> allocatedMapSpots): base() {
			DATA_Universe = universe;
			PopulateGridPositions(allocatedMapSpots);
		}

		internal Universe DATA_Universe;
		public Universe Universe {
			get { return DATA_Universe; }
		}

		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Galaxy; }
		}
		public override GameRules GameRules {
			get {
				return DATA_Universe.GameRules;
			}
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}

		private GameValue<T_StarSystemLimit> DATA_StarSystemLimit;
		public GameValue<T_StarSystemLimit> StarSystemLimit {
			get {
				if (DATA_StarSystemLimit == null)
					DATA_StarSystemLimit = GameRules.Locations.MaxStarSystemsOfGalaxy(this);
				return DATA_StarSystemLimit;
			}
		}

		GridPositionMap<GridType_Galaxy, StarSystem> DATA_StarSystems;

		internal void PopulateGridPositions(GridPositions<GridType_Universe> allocatedSpots) {
			foreach (GridPosition<GridType_Universe> position in allocatedSpots) {
				GridPosition<GridType_Galaxy> pp = GridType_Galaxy.GenerateFromUniversePosition(position);
			}
		}
	}
	public class StarSystem : LocationObject {
		public StarSystem(Galaxy galaxy): base() {
			DATA_Galaxy = galaxy;
		}

		internal Galaxy DATA_Galaxy;
		public Galaxy Galaxy { get { return DATA_Galaxy; } }

		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.StarSystem; }
		}
		public override GameRules GameRules {
			get {
				return DATA_Galaxy.GameRules;
			}
		}
		public override GameLocation Location {
			get { return new GameLocation(this); }
		}

		internal Sun DATA_sun;
		public Sun Sun {
			get {
				if (DATA_sun == null)
					HANDLE_noSun();
				return DATA_sun;
			}
		}

		GridPositionMap<GridType_StarSystem, Orbital> DATA_orbitals;

		private GameValue<T_OrbitalLimit> DATA_orbitalLimit;
		public GameValue<T_OrbitalLimit> OrbitalLimit {
			get {
				if (DATA_orbitalLimit == null)
					DATA_orbitalLimit = GameRules.Locations.MaxOrbitalsOfSun(Sun);
				return DATA_orbitalLimit;
			}
		}

		internal void HANDLE_noSun() {
			DATA_sun = GameRules.Locations.GenerateSun(this);
			DATA_orbitals.Set(GridType_StarSystem.Center, DATA_sun);
		}
	}

	public class OrbitalLayer : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.OrbitalLayer; }
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}
	public class Sector : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Sector; }
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}
	public class Site : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Site; }
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}
	public class Section : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Section; }
		}
		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}
	public class Level : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Level; }
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}
	public class Position : LocationObject {
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Position; }
		}

		public override GameLocation Location {
			get { return new GameLocation(this); }
		}
	}

	public class Sun : Orbital {

		public override GameRules GameRules {
			get { return StarSystem.GameRules; }
		}

		internal SunType DATA_SunType;
		public SunType SunType {
			get {
				return DATA_SunType;
			}
		}

		public Sun(StarSystem starSystem, SunType type)	: base(starSystem) {
			DATA_SunType = type;
		}

		public ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.StarSystem;
			}
		}
		public ActionLayer ChildType {
			get {
				return SpaceGameActionLayers.Orbital;
			}
		}

		internal GameValue<T_GravityWeight> DATA_Gravity;
		public GameValue<T_GravityWeight> Gravity {
			get {
				if (DATA_Gravity == null)
					DATA_Gravity = GameRules.Locations.GenerateGravityOfSun(this);
				return DATA_Gravity;
			}
		}

		internal GameValue<T_OrbitalLimit> DATA_maxOrbitals;
		public GameValue<T_OrbitalLimit> MaxOrbitals {
			get {
				if (DATA_maxOrbitals == null)
					DATA_maxOrbitals = GameRules.Locations.MaxOrbitalsOfSun(this);
				return DATA_maxOrbitals;
			}
		}

		internal WeightedList<T_DistanceFromSun, Orbital> DATA_Orbitals;
		public WeightedList<T_DistanceFromSun, Orbital> Orbitals {
			get {
				if (DATA_Orbitals == null) {
					DATA_Orbitals = new WeightedList<T_DistanceFromSun, Orbital>();
					PopulateOrbitalRanges();
				}
				return DATA_Orbitals;
			}
		}

		//internal helpers
		internal void PopulateOrbitalRanges() {
			GameValue<T_Int> number = GameRules.Locations.GenerateNumberOfOrbitals(this);
			int nn = number.ValueAsInt;
			for (int n = 0; n < nn; n++) {
				GameValue<T_DistanceFromSun> distance = new GameValue<T_DistanceFromSun>(n);
				DATA_Orbitals.SetValue(distance, new GameList<Orbital>());
			}
			return;
		}

	}
}

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	
	public abstract class LocationObject:GameComponent, I_Location {

		internal GameName DATA_name;
		public virtual GameName Name {
			get {
				if (DATA_name == null)
					DATA_name = new GameName(ActionLayer.Name + "." + Id);
				return DATA_name;
			}
		}

		public abstract ActionLayer ActionLayer { get; }
		public abstract GameLocation Location { get; }
		public abstract GameRules GameRules { get; }

		internal Taint DATA_Taint;
		public Taint Taint { get { 
			if(DATA_Taint==null)
				DATA_Taint = new Taint(this.RealmInfo);
				return DATA_Taint;
			}
		}

		internal RealmInfo DATA_RealmInfo;
		public RealmInfo RealmInfo { get {
			if (DATA_RealmInfo == null)
				DATA_RealmInfo = GameRules.Locations.GenerateRealmInfo(Location.Realm);
			return DATA_RealmInfo;
		}}
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {

	public interface I_Location { }
	public interface I_RuleSet { }

}
namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.Units;
	using MauronAlpha.MonoGame.SpaceGame.Actuals;
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	public class Locations : GameList<LocationObject> { }
	public class GridTypes : GameList<GridTypes> {	}
	public class GridPositions<T> : GameList<GridPosition<T>> where T:GridType { }
	public class GridPositionMap<K, V> : Assign<GridPosition<K>, V> where K : GridType,new() where V:LocationObject {
		public GridPositionMap<K, V> Set(GridPosition<K> key, V value) {
			base.SetValue(key, value);
			return this;
		}
	}
}
namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	public class T_StarSystemLimit : ValueType {
		public override GameName Name {
			get { return new GameName("StarSystemLimit"); }
		}
	}
	public class T_OrbitalLimit : ValueType {
		public override GameName Name {
			get { return new GameName("OrbitalLimit"); }
		}
	}
	public class T_DistanceFromSun : ValueType {
		public override GameName Name {
			get { return new GameName("DistanceFromSun"); }
		}
	}
	public class T_GravityWeight : ValueType {
		public override GameName Name {
			get { return new GameName("GravityWeight"); }
		}		
	}
}
