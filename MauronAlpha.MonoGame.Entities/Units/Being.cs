using MauronAlpha.MonoGame.Entities.Collections;
using MauronAlpha.MonoGame.Entities.Utility;
using MauronAlpha.MonoGame.Entities.Quantifiers;

namespace MauronAlpha.MonoGame.Entities.Units {

	public class Being:EntityComponent {

		public ConceptualName Name;

		public readonly bool IsNull = false;

		public Being(bool isNull) : base() {
			IsNull = isNull;
		}
		public Being() : base() {
			Attributes.Add("Intelligence");
			Attributes.Add("Strength");
			Attributes.Add("Dexterity");
		}

		public bool IsActive = false;
		public bool HasSpawned {
			get {
				return SpawnTime.IsNull;
			}
		}

		public readonly WorldTime SpawnTime;
		public readonly WorldTime LastModified;

		public Attributes Attributes = new Attributes();
		public Conditions Conditions = new Conditions();
		public Traits Traits = new Traits();
		public Possessions Possessions = new Possessions();

		public Aspirations Aspirations = new Aspirations();
		public Memories Memories = new Memories();
		public Relations Relations = new Relations();

		Location Location;

		public void LearnTrait(Trait trait, WorldTime time) {
			Traits.Add(trait, time);
			Memories.Add(MemoryMaker.LearnedTrait(this, trait, Location, time));
		}
		public void SufferCondition(Condition condition, WorldTime time) {
			Conditions.Add(condition, time);
			Memories.Add(MemoryMaker.SufferedCondition(this, condition, Location, time));
		}

		public static Being DoesNotExist {
			get {  
				return new Being();
			}
		}
	
		public bool Equals(Being other) {
			if (IsNull && other.IsNull)
				return true;
			if (other.IsNull)
				return false;
			return Id == other.Id;
		}
	
	}

}
