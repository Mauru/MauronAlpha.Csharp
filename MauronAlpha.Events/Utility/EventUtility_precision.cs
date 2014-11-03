namespace MauronAlpha.Events.Utility {

	//A Class that preforms precision-related comparisons
	public class EventUtility_precision:EventUtility {

		//constructor
		public EventUtility_precision(EventPrecisionRuleSet ruleSet):base() {
			RULES_comparison = ruleSet;
		}

		private EventPrecisionRuleSet RULES_comparison;
		public EventPrecisionRuleSet RuleSet {
			get {
				return RULES_comparison;
			}
		}

		public static bool AreCompatible(EventUtility_precision source, EventUtility_precision other) {
			return source.RuleSet.Equals(other.RuleSet);
		}
		public bool IsCompatibleWith(EventUtility_precision other){
			return AreCompatible(this,other);
		}
	
		public bool EQUALS_long(long source, long other) {
			long result = source-other;

			if(result == 0)
				return true;

			if(result > 0)
				return (result <= RULES_comparison.Limit_max);
	
			if(result < 0)
				return (result>=RULES_comparison.Limit_min);

			return false;
		}
	
	}
	
	//A Class that dictates values for the precision ruleset
	public abstract class EventPrecisionRuleSet:MauronCode_eventComponent {

		public abstract string Name { get; }

		public virtual bool Equals(EventPrecisionRuleSet other) {
			return Name==other.Name;
		}

		public static EventPrecisionRuleSet SystemTime {
			get { return new EventPrecisionRuleSet_systemTime(); }
		}
		public static EventPrecisionRuleSet Absolute {
			get {
				return new EventPrecisionRuleSet_absolute();
			}
		}
		
		public abstract long Range_past { get; }
		public abstract long Range_future { get; }

		public abstract long Limit_min { get; }
		public abstract long Limit_max { get; }

		public abstract long Numeric_length_min { get; }
		public abstract long Numeric_length_max { get; }
		
	}


	public class EventPrecisionRuleSet_absolute:EventPrecisionRuleSet {

		public override string Name { get { return "absolute"; } }
		public override long Range_past { get { return -1; } }
		public override long Range_future { get { return -1; } }

		public override long Limit_min { get { return 0; } }
		public override long Limit_max { get { return 0; } }

		public override long Numeric_length_min { get { return -1; } }
		public override long Numeric_length_max { get { return -1; } }
	}

	
	/// <summary>
	/// Compare "one Event" into the future with absolute precision
	/// </summary>
	/// <remarks>We try to make up for potential discrepancies due to test execution time
	/// (LIMIT should probably be customized for "lag")
	/// </remarks>
	public class EventPrecisionRuleSet_systemTime : EventPrecisionRuleSet {

		public override string Name { get { return "systemTime"; } }
		public override long Range_past { get { return 0; } }
		public override long Range_future { get { return 1; } }

		public override long Limit_min { get { return 0; } }
		public override long Limit_max { get { return 0; } }

		public override long Numeric_length_min { get { return -1; } }
		public override long Numeric_length_max { get { return -1; } }

	}
}
