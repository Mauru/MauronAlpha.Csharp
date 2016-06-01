using MauronAlpha.MonoGame.SpaceGame.Interfaces;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Units;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	public class Population:GameList<Being> {

		public I_Habitable Parent;
		public Population(I_Habitable parent):base() {
			Parent = parent;
		}

	}

	public interface I_ComplexBeing:I_Being {
		GameLocation Origin { get; }
	}
	public interface I_SimpleBeing : I_Being { }
	public interface I_Being {
		Species Species { get; }

		bool IsBeing { get; }
		bool IsEquipment { get; }
		bool IsValue { get; }
		bool IsResource { get; }
		bool IsSkill { get; }
		bool IsStat { get; }

		GameLocation Location { get; }

	}

	public class Agent : Being, I_ComplexBeing {
		
		public Agent(I_Habitable origin, Species species) : base(origin.Location, species) {
			D_origin = origin;
		}

		I_Habitable D_origin;
		public GameLocation Origin {
			get { return D_origin.Location; }
		}
	}
	public class CrewMember : Being, I_SimpleBeing {
		public CrewMember(I_Habitable origin, Species species): base(origin.Location, species) {
			D_origin = origin;
		}
		I_Habitable D_origin;
		public GameLocation Origin {
			get { return D_origin.Location; }
		}

	}

	public class Worker : Being, I_SimpleBeing {
		public Worker(I_Habitable origin, Species species): base(origin.Location, species) {
			D_origin = origin;
		}
		I_Habitable D_origin;
		public GameLocation Origin {
			get { return D_origin.Location; }
		}


	}
	public class PlanetPop : Being, I_SimpleBeing {

		public PlanetPop(I_Habitable origin, Species species) : base(origin.Location, species) {
			D_origin = origin;
		}
		I_Habitable D_origin;
		public GameLocation Origin {
			get { return D_origin.Location; }
		}
	}

	public class Being : GameObject, I_Being, I_RelationTarget, I_RelationSource, I_Movable {


		public Being(GameLocation location, Species species):base() {
			D_location = location;
			D_species = species;
			
		}

		public BaseStats BaseStats {
			get {
				return Species.BaseStats;
			}
		}
		GameLocation D_location;
		public GameLocation Location { get { return D_location; } }
		
		Species D_species;
		public Species Species { get { return D_species; } }

		public MoveData Move(I_Habitable place) { 
			return place.MovementDataFor(this);
		}

		public override GameName Name {
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsBeing {
			get { return true; }
		}

		public override bool IsEquipment {
			get { return false; }
		}

		public override bool IsQuantity {
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsResource {
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsSkill {
			get { return false; }
		}
	}

	public class Vessel : Being, I_Habitable, I_ComplexBeing {

		public Vessel(GameLocation orgin, Species creator) : base(orgin, creator) {
			D_origin = orgin;
		}

		GameLocation D_origin;
		public GameLocation Origin {
			get {
				return D_origin;
			}
		}

		public MoveData MovementDataFor(I_Movable obj) {
			throw new System.NotImplementedException();
		}

		public bool IsAttached {
			get { return false; }
		}
	}

	public interface I_Machine : I_Being {

		Species BuiltBy { get; }
		Species MaintenedBy { get; }

	}
	public class Machine : Being, I_Machine {
		public Species BuiltBy {
			get { throw new System.NotImplementedException(); }
		}

		public Species MaintenedBy {
			get { throw new System.NotImplementedException(); }
		}

		public Machine(GameLocation location, Species species): base(location, species) {

		}
	}


}
