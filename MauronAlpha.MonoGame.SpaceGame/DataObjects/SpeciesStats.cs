using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.SpaceGame.Quantifiers;
using MauronAlpha.MonoGame.SpaceGame.Units;


namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	
	public class SpeciesStats:MauronCode_dataMap<SpeciesStat> {
	}

	public interface I_SpeciesDefinition { }
	public class SpeciesDefintion : RuleSet, I_RuleSet<RULES_species>, I_SpeciesDefinition {

		public SpeciesDefintion() : base(new RULES_species()) { }

		public SpeciesStats BaseStats;

		public override RuleSetType Type {
			get { return new RULES_species(); }
		}


		RULES_species I_RuleSet<RULES_species>.Type {
			get { return new RULES_species(); }
		}
	}

	public class RULES_species : RuleSetType { }

}
