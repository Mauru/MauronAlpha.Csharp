namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Actuals;

	//Defines the type of an orbital
	public abstract class OrbitalType : GameComponent {
		public abstract GameName Name { get; }

		public virtual bool IsSystemCore {
			get { return false; }
		}
		public virtual bool IsSystemOrbital {
			get { return false; }
		}
		public virtual bool IsSubOrbital {
			get { return false; }
		}

		public virtual bool CheckIsSystemCore(Orbital o) {
			return IsSystemCore;
		}
		public virtual bool CheckIsSystemOrbital(Orbital o) {
			return IsSystemOrbital && o.ParentType.IsSystemCore;
		}
		public virtual bool CheckIsSubOrbital(Orbital o) {
			return IsSubOrbital;
		}

		public abstract OrbitalTypes ChildTypes(Orbital o);
		public abstract OrbitalTypes ParentTypes(Orbital o);
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Units; //LocationObject - might want to move to dataunits

	//An Object on a StarSystem Map
	public class Orbital : LocationObject {
		bool B_isNull = false;
		public bool IsNull {
			get { return B_isNull;}
		}

		internal StarSystem DATA_StarSystem;
		public StarSystem StarSystem {
			get {
				if (DATA_StarSystem == null)
					return DATA_OrbitalParent.StarSystem;
				return DATA_StarSystem;
			}
		}

		internal OrbitalType DATA_orbitalType;
		public OrbitalType OrbitalType {
			get { return DATA_orbitalType; }
		}

		public Orbital(StarSystem system, bool isNull):base() {
			DATA_StarSystem = system;
			B_isNull = isNull;
			if(isNull)
				DATA_orbitalType = OrbitalTypes.Null;
			else
				DATA_orbitalType = OrbitalTypes.Undefined;
		}
		public Orbital(StarSystem system, OrbitalType type): base() {
			DATA_StarSystem = system;
			DATA_orbitalType = type;
		}
		public Orbital(Orbital o, OrbitalType type):base() {
			DATA_orbitalType = type;
			DATA_OrbitalParent = o;
		}

		public virtual Sun ClostestSun {
			get {
				if (DATA_orbitalType.Equals(OrbitalTypes.Sun))
					return this;
			}
		}

		internal Orbital DATA_OrbitalParent;
		public Orbital OrbitalParent {
			get {
				if (DATA_OrbitalParent == null)
					return OrbitalPresets.NoParentOf(this);
				return DATA_OrbitalParent;
			}
		}

		public override GameRules GameRules {
			get { return DATA_StarSystem.GameRules; }
		}
		public override ActionLayer ActionLayer {
			get { return SpaceGameActionLayers.Orbital; }
		}
		public override GameLocation Location {
			get { return new GameLocation(this); }
		}

		public bool IsSystemCore { get { return DATA_orbitalType.CheckIsSystemCore(this); } }
		public bool IsSystemOrbital { get { return DATA_orbitalType.CheckIsSystemOrbital(this); } }
		public bool IsSubSystemOrbital { get { return DATA_orbitalType.CheckIsSubOrbital(this); } }

		public OrbitalType ParentType { get { 
			if(!HasParent)
				return 
		} }

		public WeightedList<T_DistanceFromOrbitalParent, Orbital> Orbitals;
		public GameValue<T_GravityWeight> GravityWeight {
			get {
				return OwnGravityWeight.Sum(GravityWeightOfChildren);
			}
		}
		public GameValue<T_GravityWeight> GravityWeightOfChildren;
		public GameValue<T_GravityWeight> OwnGravityWeight;

		public WeightedList<T_OrbitalLayerDepth, OrbitalLayer> OrbitalLayers;

		GameValue<T_OrbitalLayerDepth> DATA_MaxOrbitalLayers;
		public GameValue<T_OrbitalLayerDepth> MaxOrbitalLayers {
			get {
				if (DATA_MaxOrbitalLayers == null)
					DATA_MaxOrbitalLayers = GameRules.Locations.MaxOrbitalLayersOf(this);
				return DATA_MaxOrbitalLayers;
			}
		}
	}

	public class OT_Undefined : OrbitalType {
		public override GameName Name {
			get {
				return new GameName("Undefined");
			}
		}
	
		public override bool IsNull {
			get { return false; }
		}
	}
	public class OT_Null : OrbitalType {
		public override GameName Name {
			get {
				return new GameName("Null");
			}
		}
	
		public override bool IsNull {
			get { return true; }
		}
	}
	
	public class OT_Sun : OrbitalType {

		public override GameName Name {
			get {
				return new GameName("Sun");
			}
		}
		public override bool IsSystemCore {
			get {
				return true;
			}
		}
		public override bool IsSystemOrbital {
			get {
				return false;
			}
		}
		public override bool CanHaveChildOrbitals {
			get {
				return true;
			}
		}

		public override OrbitalTypes ChildTypes(Orbital o) {
			return o.GameRules.Locations.AllowedChildOrbitalsOfSun(o);
		}
		public override OrbitalTypes ParentTypes(Orbital o) {
			return new OrbitalTypes();
		}

	}
	public class OT_Planetoid : OrbitalType {

		public override GameName Name {
			get { return new GameName("Planetoid"); }
		}

		public override bool IsSystemOrbital {
			get {
				return true;
			}
		}

		public override OrbitalTypes ChildTypes(Orbital o) {
			return o.GameRules.Locations.AllowedChildOrbitalsOfPlanetoid(o);
		}

		public override OrbitalTypes ParentTypes(Orbital o) {
			return o.GameRules.Locations.AllowedParentOrbitalsOfPlanetoid(o);
		}
	}
	public class OT_Debris : OrbitalType {

		public override GameName Name {
			get { return new GameName("Debris"); }
		}

		public override bool IsSystemOrbital {
			get {
				return true;
			}
		}

		public override OrbitalTypes ChildTypes(Orbital o) {
			return o.GameRules.Locations.AllowedChildOrbitalsOfDebris(o);
		}

		public override OrbitalTypes ParentTypes(Orbital o) {
			return o.GameRules.Locations.AllowedParentOrbitalsOfDebris(o);
		}
	}
	public class OT_Moon : OrbitalType {
		public override GameName Name {
			get { return new GameName("Moon"); }
		}

		public OrbitalTypes ChildTypes(Orbital o) {
			return OrbitalTypes.MoonChildTypes;
		}
		public OrbitalTypes ParentTypes(Orbital o) {
			return new OrbitalTypes() {
				OrbitalTypes.Planetoid,
			};
		}
	}
	public class OT_Station : OrbitalType {
		public override GameName Name {
			get {
				return new GameName("Station");
			}
		}
		public override OrbitalTypes ChildTypes(Orbital o) {
			return OrbitalTypes.StationChildTypes;
		}
		public override OrbitalTypes ParentTypes(Orbital o) {
			return OrbitalTypes.StationParentTypes;
		}
		public override bool CanHaveChildOrbitals {
			get {
				return true;
			}
		}
	}
	public class OT_Satelite : OrbitalType {
		public override GameName Name {
			get { return new GameName("Satelite"); }
		}
		public override OrbitalTypes ParentTypes(Orbital o) {
			return OrbitalTypes.SateliteParentTypes;
		}
		public override OrbitalTypes ChildTypes(Orbital o) {
			return OrbitalTypes.None;
		}
	}
	public class OT_Probe : OrbitalType {
		public override GameName Name {
			get { return new GameName("Probe"); }
		}
		public override OrbitalTypes ParentTypes(Orbital o) {
			return OrbitalTypes.ProbeParentTypes;
		}
		public override OrbitalTypes ChildTypes(Orbital o) {
			return OrbitalTypes.None;
		}
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	public class T_DistanceFromOrbitalParent : ValueType {
		public override GameName Name {
			get { return new GameName("DistanceFromOrbitalParent"); }
		}
	}
	public class T_OrbitalLayerDepth : ValueType {
		public override GameName Name {
			get { return new GameName("OrbitalLayerDepth"); }
		}
	}
}

namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Actuals;
	
	//Defaults for orbitals
	public class OrbitalPresets:GameComponent {

		public static Orbital NoParentOf(Orbital o) {
			return new Orbital(o.StarSystem,true);
		}

	}

	public class OrbitalTypes : GameList<OrbitalType> { 
	
		public static OT_Null Null {
			get {
				return new OT_Null();
			}
		}
		public static OT_Undefined Undefined {
			get {
				return new OT_Undefined();
			}
		}
		public static OT_Sun Sun {
			get {
				return new OT_Sun();
			}
		}
		public static OT_Planetoid Planet {
			get {
				return new OT_Planetoid();
			}
		}
		public static OT_Debris Debris {
			get {
				return new OT_Debris();
			}
		}
		public class OT_Fleet : OrbitalType {
			public override GameName Name {
				get { return new GameName("Fleet"); }
			}
			public override OrbitalTypes ChildTypes(Orbital o) {
				return o.GameRules.Locations.AllowedChildOrbitalsOfFleet(o);
			}
			public override OrbitalTypes ParentTypes(Orbital o) {
				return OrbitalTypes.FleetParentTypes;
			}
		}

	}
}