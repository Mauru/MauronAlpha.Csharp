/*using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Utility;
using MauronAlpha.MonoGame.SpaceGame.DataObjects;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;
//using MauronAlpha.MonoGame.SpaceGame.Collections;*/

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;

	public class Faction:GameComponent, I_GameActorGroup {

		//A relative Value returning the "relative power of a factrion"
		public GameValue<T_FactionStrength> Strength;
		Taint Taint;
		InfluenceMap<I_InfluenceActor> Power;
		InfluenceMap<I_InfluenceActor> LoyalTo;
		Hierarchy<I_InfluenceActor> Hierarchy;
		GameActors DATA_members = new GameActors();
		I_GameActorGroup Members {
			get {
				return DATA_members;
			}
		}

	}
}

namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Units;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;

	public class InfluenceMap<T> : Assign<InfluenceStrength, T> where T : I_InfluenceActor {

		public bool TryHighestInfluence(ref T reference) {
			if(IsEmpty)
				return false;
			InfluenceStrength highest = null;
			T result = default(T);
			foreach(KVP<InfluenceStrength,T> kvp in AsKVPList()) {
				InfluenceStrength i = kvp.Key;
				if(highest == null || highest.IsSmallerThan(i)) {
					highest = i;
					result = kvp.Value;
				}
			}
			if (highest == null)
				return false;
			reference = result;
			return true;
		}
	
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Units;
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	using MauronAlpha.MonoGame.SpaceGame.Collections;

	public class Influence : GameComponent {

		public Faction Source;
		public Faction Target;
		public GameValue<T_InfluenceStrength> Strength;

	}

	public class Hierarchy<T> : GameComponent where T:I_HierarchyMember {

		GameList<SentientCreature> NamedMembers;
		I_HierarchyLeader Leader;

	}

	//Determines how a Hierarchy is sorted
	public class HierarchyModel : GameComponent {

		public virtual bool TryLeaderOf<T>(ref T defaultLeader, InfluenceMap<T> map) where T : I_InfluenceActor {
			return map.TryHighestInfluence(ref defaultLeader); 
		}
	
	}

	//Defines static examples of ruling types
	public class FactionEthics : GameComponent {}

}

namespace MauronAlpha.MonoGame.SpaceGame.Interfaces {

	//Defines someone or something that can be a member of a hierarchy
	public interface I_HierarchyMember {}

	//Defines someone or something that can be a leader of a hierarchy
	public interface I_HierarchyLeader {}

	//Defines someone or somethng  who can be influenced
	public interface I_InfluenceTarget {}
	
	//Defines someone or something which can interact with influence
	public interface I_InfluenceActor:I_HierarchyMember { }

}

namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	//A perceived value of the total "Strength" of a faction
	public class T_FactionStrength : ValueType {
		public override GameName Name { get { return new GameName("FactionStrength");} }
	}

	//A numerical value of one faction's influence over another
	public class T_InfluenceStrength : ValueType {
		public override GameName Name { get { return new GameName("InfluenceStrength"); } }
	}

	public class InfluenceStrength : GameValue<T_InfluenceStrength> { }

}
