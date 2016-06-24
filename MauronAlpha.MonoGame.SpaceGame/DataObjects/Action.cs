//Interfaces
namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	//Defines something that can initiate or be the target of an action
	public interface I_GameActor {

		GameLocation Location { get; }

		bool IsGroup { get; }

	}
	//Defines a collection of GameActors
	public interface I_GameActorGroup { }
}

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	public class GameActor : GameComponent, I_GameActor {
		
		public GameActor(GameLocation location, bool isGroup): base() {
			DATA_location = location;
			B_isGroup = isGroup;
		}

		GameLocation DATA_location;
		public GameLocation Location {
			get { return DATA_location; }
		}

		bool B_isGroup;
		public bool IsGroup {
			get { return B_isGroup; }
		}

	}
}

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	using MauronAlpha.MonoGame.SpaceGame.Collections;

	//An unobservable behavior of an individual
	public abstract class Activity : GameComponent { 
		
		public abstract GameName Name { get; }

		public virtual WeightedList<T_FeatureLikelyHood,MetaPosition> LikelyPositions {
			get { return MetaPosition.Any; }
		}

	}

	//Describes where an individual might be on a map without actually placing it
	public class MetaPosition : GameComponent {

		public MetaPosition(bool isNull) : base() {
			B_isNull = isNull;
		}
		private bool B_isNull = false;
		public bool IsNull {
			get {
				return B_isNull;
			}
		}

		public static WeightedList<T_FeatureLikelyHood, MetaPosition> Any {
			get {
				WeightedList<T_FeatureLikelyHood, MetaPosition> result = new WeightedList<T_FeatureLikelyHood, MetaPosition>();
				ValueType t = new T_FeatureLikelyHood();
				MetaPosition instance = new MetaPosition(true);
				result.Set(ValueType.NullAs<T_FeatureLikelyHood>(), instance);
				return result;
			} 
		}

		public WeightedList<T_FeatureLikelyHood, MapFeature> DATA_features;
		public WeightedList<T_FeatureLikelyHood, MapFeature> FeatureRequests {
			get {
				if (DATA_features == null)
					return new WeightedList<T_FeatureLikelyHood, MapFeature>();
				return DATA_features;
			}
		}

	}

	//A point of interest in Map Generation
	public class MapFeature : GameComponent {

	}

	public class IndividualShedule : WeightedList<T_GameTimeHour,Activity> {



	}

	public abstract class Action : GameComponent {

		public abstract ActionType Type { get; }
		public abstract ActionPhase Phase { get; }
		public abstract ActionLayer Layer { get; }

		public abstract ActionFactors Factors { get; } 

		public ActionQuery FormRequest(GameLocation location, I_GameActor source, I_GameActor target) {

			return new ActionQuery(this, source, target);

		}

	}
	public class Action_Undefined : Action {
		public Action_Undefined(): base() {}
		public override ActionType Type {
			get {
				return ActionTypes.Undefined;
			}
		}
		public override ActionPhase Phase {
			get {
				return ActionPhases.Undefined;
			}
		}
		public override ActionLayer Layer {
			get {
				return ActionLayers.Undefined;
			}
		}
		public override ActionFactors Factors {
			get { return new ActionFactors(); }
		}
	}
	
	//See what the results of an action would be
	public class ActionQuery : GameComponent {

		I_GameActor Source;
		I_GameActor Target;

		public ActionQuery() { }
		public bool IsDirectional;
		public bool HasEffectsSelf;
		public bool HasEffectsOther;

		internal Action DATA_action;
		public Action Action {
			get {
				if (DATA_action == null)
					return GameActions.Undefined;
				return DATA_action;
			}
		}

		public ActionQuery(Action action, I_GameActor source, I_GameActor target) : base() { }

	}

	//what influences an action's success and how it is preceived
	public class ActionFactor : GameComponent {	}

	//Describes the kind of action
	public abstract class ActionType : GameComponent {

		public abstract GameName Name { get; }
		public virtual bool IsNull { get { return false; } }

		public bool Equals(ActionType other) {
			return Name.Equals(other.Name);
		}
	}

	//WHERE an action can take place
	public abstract class ActionLayer : GameComponent {
		public abstract GameName Name { get; }
		public virtual bool IsHierarchyKing {
			get { return false; }
		}
		public virtual bool IsHierarchyBottom {
			get {
				return false;
			}
		}
		public virtual bool IsAll { get { return false; } }
		public virtual bool IsNull { get { return false; } }

		public abstract ActionLayer ParentType { get; }
		public abstract ActionLayers ChildTypes { get; }

		public abstract int Order { get; }

		public int Compare(ActionLayer other) {
			int o = other.Order;
			if (Order == o)
				return 0;
			if (Order > o)
				return 1;
			return -1;
		}
		public bool Equals(ActionLayer other) {
			return Name.Equals(other.Name);
		}
	}

	//WHEN an action can take place
	public abstract class ActionPhase : GameComponent {

		public ActionPhase(bool isNull)	: base() {
				B_isNull = isNull;
		}

		private bool B_isNull = false;
		public bool IsNull { get { return B_isNull; } }

		public abstract GameName Name { get; }
	}
	public class AP_Undefined:ActionPhase {
		public AP_Undefined() : base(true) { }

		public override GameName Name {
			get { return new GameName("Undefined"); }
		}
	}

	/* Define Action Types and their use */
	public class AT_Movement : ActionType {
		public override GameName Name { get { return new GameName("Movement"); } }
	}
	public class AT_Undefined : ActionType {
		public override bool IsNull { get { return true; } }
		public override GameName Name { get { return new GameName("Undefined"); } }
	}

	/* Define Action Layers */
	public class AL_Undefined : ActionLayer {
		public override GameName Name {
			get { return new GameName("Undefined"); }
		}
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { ActionLayers.All };
			}
		}
		public override ActionLayer ParentType {
			get {
				return ActionLayers.Undefined;
			}
		}
		public override bool IsNull { get { return true; } }
		public override int Order {
			get { return 0; }
		}
	}
	public class AL_All : ActionLayer {
		public override GameName Name {
			get { return new GameName("All"); }
		}
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { ActionLayers.Undefined };
			}
		}
		public override ActionLayer ParentType {
			get {
				return ActionLayers.Undefined;
			}
		}
		public override bool IsAll { get { return true; } }
		public override int Order {
			get { return 0; }
		}
	}
}
namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	
	public class T_FeatureLikelyHood : T_Percent {

		public override GameName Name {
			get {
				return new GameName("FeatureLikelyHood");
			}
		}

	}
}
namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Actuals;

	public class SpaceGameActionLayers : ActionLayers {
		public static AL_Map Map {
			get {
				return new AL_Map();
			}
		}
		public static AL_Realm Realm {
			get {
				return new AL_Realm();
			}
		}
		public static AL_Universe Universe {
			get {
				return new AL_Universe();
			}
		}
		public static AL_Galaxy Galaxy {
			get {
				return new AL_Galaxy();
			}
		}
		public static AL_StarSystem StarSystem {
			get {
				return new AL_StarSystem();
			}
		}
		public static AL_Orbital Orbital {
			get {
				return new AL_Orbital();
			}
		}
		public static AL_OrbitalLayer OrbitalLayer {
			get {
				return new AL_OrbitalLayer();
			}
		}
		public static AL_Sector Sector {
			get {
				return new AL_Sector();
			}
		}
		public static AL_Site Site {
			get {
				return new AL_Site();
			}
		}
		public static AL_Section Section {
			get {
				return new AL_Section();
			}
		}
		public static AL_Level Level {
			get {
				return new AL_Level();
			}
		}
		public static AL_Position Position {
			get {
				return new AL_Position();
			}
		}
	}

	/// <summary>	Defines the order of ActionLayers - 12 (Highest) to 0 (All,Undefined)	</summary>
	public struct ActionLayerIndex {
		public static int IndexOf(AL_Map map) {
			return 12;
		}
		public static int IndexOf(AL_Realm realm) {
			return 11;
		}
		public static int IndexOf(AL_Universe universe) {
			return 10;
		}
		public static int IndexOf(AL_Galaxy galaxy) {
			return 9;
		}
		public static int IndexOf(AL_StarSystem system) {
			return 8;
		}
		public static int IndexOf(AL_Orbital orbital) {
			return 7;
		}
		public static int IndexOf(AL_OrbitalLayer layer) {
			return 6;
		}
		public static int IndexOf(AL_Sector sector) {
			return 5;
		}
		public static int IndexOf(AL_Site site) {
			return 4;
		}
		public static int IndexOf(AL_Section section) {
			return 3;
		}
		public static int IndexOf(AL_Level level) {
			return 2;
		}
		public static int IndexOf(AL_Position position) {
			return 1;
		}
		public static int IndexOf(AL_Undefined undefined) {
			return 0;
		}
		public static int IndexOf(AL_All undefined) {
			return 0;
		}
	}

	public class AL_Map : ActionLayer {
		public override bool IsHierarchyKing {
			get {
				return true;
			}
		}
		//The main/realm universe map
		public override GameName Name {
			get {
				return new GameName("Map");
			}
		}
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Universe };
			}
		}
		public override ActionLayer ParentType {
			get {
				return this;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Realm : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Realm");
			}
		}
		//Defines different layers of reality
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Universe };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Map;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Universe : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Universe");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Galaxy };
			}
		}
		public override ActionLayer ParentType {
			get { return SpaceGameActionLayers.Realm; }
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Galaxy : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Galaxy");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.StarSystem };
			}
		}
		public override ActionLayer ParentType {
			get { return SpaceGameActionLayers.Universe; }
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_StarSystem : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("StarSystem");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Orbital };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Galaxy;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Orbital : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Orbital");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.OrbitalLayer };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Orbital;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_OrbitalLayer : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("OrbitalLayer");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Sector };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Orbital;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Sector : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Sector");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Site };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Orbital;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Site : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Site");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.OrbitalLayer };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Section;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Section : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Section");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Level };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Site;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Level : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Level");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers() { SpaceGameActionLayers.Position };
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Section;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
	public class AL_Position : ActionLayer {
		public override GameName Name {
			get {
				return new GameName("Position");
			}
		}
		//A universe in a realm
		public override ActionLayers ChildTypes {
			get {
				return new ActionLayers();
			}
		}
		public override ActionLayer ParentType {
			get {
				return SpaceGameActionLayers.Level;
			}
		}
		public override int Order {
			get { return ActionLayerIndex.IndexOf(this); }
		}
	}
}
namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;

	public class ActionFactors : GameList<ActionFactor> { }


	public class ActionLayers : GameList<ActionLayer> {

		public static ActionLayer Undefined {
			get {
				return new AL_Undefined();
			}
		}
		public static ActionLayer All {
			get {
				return new AL_All();
			}
		}
		
	}
	public class ActionPhases : GameList<ActionPhase> {
		public static ActionPhase Undefined {
			get {
				return new AP_Undefined();
			}
		}
	}
	public class ActionTypes : GameList<ActionType> {

		public static ActionType Undefined {
			get {
				return new AT_Undefined();
			}
		}

	}

	public class GameActions : GameList<Action> {
		public static Action Undefined {
			get {
				return new Action_Undefined();
			}
		}
	}

	public class Group<T> : GameComponent, I_GameActor, I_GameActorGroup where T : I_GameActor {
		public bool IsGroup {
			get {
				return true;
			}
		}
		internal GameLocation DATA_location;
		public GameLocation Location {
			get {
				return DATA_location;
			}
		}
		public Group(GameLocation location)
			: base() {
			DATA_location = location;
		}

	}
	public class MixedGroup : GameComponent, I_GameActor, I_GameActorGroup {
		public bool IsGroup { get { return true; } }
		internal GameLocation DATA_location;
		public GameLocation Location {
			get {
				return DATA_location;
			}
		}
		public MixedGroup(GameLocation location)
			: base() {
			DATA_location = location;
		}

	}
	public class DispersedGroup<T> : GameComponent, I_GameActor, I_GameActorGroup where T : I_GameActor {
		public bool IsGroup { get { return true; } }
		internal GameLocation DATA_location;
		public GameLocation Location {
			get {
				return DATA_location;
			}
		}
		public DispersedGroup(GameLocation location): base() {
			DATA_location = location;
		}


	}
	public class DispersedMixedGroup : GameComponent, I_GameActor, I_GameActorGroup {
		public bool IsGroup { get { return true; } }
		internal GameLocation DATA_location;
		public GameLocation Location {
			get {
				return DATA_location;
			}
		}
	}

	public class GameActors : DispersedMixedGroup { }
}
