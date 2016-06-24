namespace MauronAlpha.MonoGame.SpaceGame.Units {
	using MauronAlpha.MonoGame.SpaceGame.Collections;
	using MauronAlpha.MonoGame.SpaceGame.Interfaces;
	
	//Defines an animal or sentient being as an instance of 1
	public abstract class Creature:GameComponent {

		public Creature(Species species):base() {



		}

		private Species DATA_species;
		public Species Species;

		public bool IsOrganic { get { return Species.IsOrganic; } }
		public virtual bool IsMachine { get { return Species.IsMachine; } }
		public virtual bool IsSentient { get { return Species.IsSentient; } }
		public virtual bool IsGroup { get { return false; } }

	}

	//Defines a being with beliefs
	public abstract class SentientCreature : Creature {

		public override bool IsSentient {
			get { return true; }
		}
		public abstract InfluenceMap<I_InfluenceActor> Allegiance { get; }

	}

}
