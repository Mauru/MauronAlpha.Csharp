

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
	using MauronAlpha.MonoGame.SpaceGame.Collections;

	public class Taint : GameComponent {
		public Taint(RealmInfo info) : base() { }
	}

	public abstract class Ethic : GameComponent {

		public abstract GameName Name { get; }
		public bool Equals(Ethic other) {
			return Name.Equals(other.Name);
		}
	
	}

	//Explanation of why something is something
	public abstract class EthicalJustification:GameComponent {

		private GameName D_Name;
		public virtual GameName Name { 
			get { 
				if(D_Name == null)
					return new GameName("EthicalJustification.Generic");
				return D_Name;
			}
		}
	
		public EthicalJustification(GameName name):base() {}
	
	}

	//How one Ethic sees another
	public class EthicalRelation : GameComponent {

		public EthicalRelation(Ethic ethic, GameValue<T_EthicalRelation> strength, EthicalJustification justification):base() {
			DATA_why.Add(justification);
			Value = strength;
		}
		public EthicalRelation(Ethic ethic) : base() { 
			DATA_source = ethic;
		}
		
		internal Ethic DATA_source;
		public Ethic Source {
			get {
				if (DATA_source == null)
					return Ethics.Undefined;
				return DATA_source;
			}
		}
		public Ethic Target;
		public GameValue<T_EthicalRelation> Value;
		public GameValue<T_EthicalRelation> MaxValue;
		public static GameValue<T_EthicalRelation> Self {
			get {
				return new GameValue<T_EthicalRelation>(false,false,true);
			}
		}
		
		internal EthicalJustifications DATA_why = new EthicalJustifications();
		public EthicalJustifications Why {
			get {
				return DATA_why;
			}
		}
	
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Quantifiers {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;

	public class T_EthicalRelation : ValueType {

		public override GameName Name {
			get { return new GameName("EthicalRelation"); }
		}
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Collections {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Actuals;

	public class Ethics:GameList<Ethic> {

		public static ETHIC_Undefined Undefined {
			get {
				return new ETHIC_Undefined();
			}
		}

		public static ETHIC_Death Death {
			get { return new ETHIC_Death(); }
		}
		public static ETHIC_Life Life {
			get { return new ETHIC_Life(); }
		}
		
		public static ETHIC_Community Community {
			get { return new ETHIC_Community(); }
		}
		public static ETHIC_Individualism Individualism {
			get { return new ETHIC_Individualism(); }
		}
		
		public static ETHIC_Violence Violence {
			get { return new ETHIC_Violence(); }
		}
		public static ETHIC_Diplomacy Diplomacy {
			get { return new ETHIC_Diplomacy(); }
		}
	
		public static ETHIC_Materialism Materialism {
			get { return new ETHIC_Materialism(); }
		}
		public static ETHIC_Spiritualism Spiritualism {
			get { return new ETHIC_Spiritualism(); }
		}

		public static ETHIC_Knowledge Knowledge {
			get {
				return new ETHIC_Knowledge();
			}
		}
	}

	public class EthicalJustifications:GameList<EthicalJustification> {
	
		public static EthicalJustification SelfPreservation {
			get { 
				return new EJ_SelfPreservation();
			}
		}
	
	}

}

namespace MauronAlpha.MonoGame.SpaceGame.Actuals {
	using MauronAlpha.MonoGame.SpaceGame.DataObjects;
	using MauronAlpha.MonoGame.SpaceGame.Collections;

	//Undefined, something that needs to be filled in later
	public class EJ_Undefined : EthicalJustification {
		public override GameName Name {
			get {
				return new GameName("Undefined");
			}
		}
	}
	//The will to stay alive
	public class EJ_SelfPreservation:EthicalJustification {
		public override GameName Name { get { return new GameName("SelfPreservation"); } }
	}
	//Wanting a certain item
	public class EJ_WantItem:EthicalJustification {
		public override GameName Name { get { return new GameName("WantItem"); } }
	}
	//Wanting a resource
	public class EJ_WantResource:EthicalJustification {
		public override GameName Name { get { return new GameName("WantResource"); } }
	}
	//Wanting information
	public class EJ_WantKnowledge:EthicalJustification {
		public override GameName Name { get { return new GameName("WantKnowledge"); } }
	}
	//Preservation of  a species
	public class EJ_CollectiveSurvival:EthicalJustification {
		public override GameName Name { get { return new GameName("CollectiveSurvival"); } }
	}
	//Represent the will/needs of a faction superimposed on ones own
	public class EJ_FactionWill:EthicalJustification {
		public override GameName Name { get { return new GameName("FactionWill"); } }
	}
	//Revenge
	public class EJ_Revenge:EthicalJustification {
		public override GameName Name { get { return new GameName("Revenge"); } }
	}
	//Food / Hunger
	public class EJ_Food:EthicalJustification {
		public override GameName Name { get { return new GameName("Food"); } }
	}
	//Politics, the will to advance in ones hierarchy
	public class EJ_Politics:EthicalJustification {
		public override GameName Name { get { return new GameName("Politics"); } }
	}
	//A Moral Code depends on either the faction or the species ethics
	public class EJ_Morals:EthicalJustification {
		public override GameName Name { get { return new GameName("Morals"); } }
	}
	//Territorial, an interest in space
	public class EJ_Territorial:EthicalJustification {
		public override GameName Name { get { return new GameName("Territorial"); } }
	}
	

	public class ETHIC_Undefined : Ethic {
		public override GameName Name { get { return new GameName("Undefined"); } }
		public GameList<EthicalRelation> Relations = new GameList<EthicalRelation>();
	}
	//Belief in the end of things and its attractiveness to an individual
	public class ETHIC_Death : Ethic {

		public override GameName Name { get { return new GameName("Death"); } }
		public GameList<EthicalRelation> Relations = new GameList<EthicalRelation>() {
			new EthicalRelation(Ethics.Death, EthicalRelation.Self, EthicalJustifications.SelfPreservation),
		};

	}

	//Belief in the preservation of things and its attractiveness to an individual
	public class ETHIC_Life : Ethic {
		public override GameName Name {
			get { return new GameName("Life"); }
		}
	}
	//Belief in the usage of words to achieve things
	public class ETHIC_Diplomacy : Ethic {
		public override GameName Name {
			get { return new GameName("Diplomacy"); }
		}
	}
	//Belief in aggressiveness to achieve one's means
	public class ETHIC_Violence : Ethic {
		public override GameName Name {
			get { return new GameName("Violence"); }
		}
	}
	//Belief in the importance of individual thought
	public class ETHIC_Individualism : Ethic {
		public override GameName Name {
			get { return new GameName("Individualism"); }
		}
	}
	//Belief in the importance of being a member of something
	public class ETHIC_Community : Ethic {
		public override GameName Name {
			get { return new GameName("Community"); }
		}
	}
	//Belief in the Necessity of Tools
	public class ETHIC_Materialism : Ethic {
		public override GameName Name {
			get { return new GameName("Materialism"); }
		}
	}
	//Belief in the Moral-Society Codes
	public class ETHIC_Spiritualism : Ethic {
		public override GameName Name {
			get { return new GameName("Spiritualism"); }
		}
	}
	//The quest for knowledge
	public class ETHIC_Knowledge : Ethic {
		public override GameName Name {
			get { return new GameName("Knowledge"); }
		}
	}
}

