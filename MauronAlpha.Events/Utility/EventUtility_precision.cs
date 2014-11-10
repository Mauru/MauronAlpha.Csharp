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

		#region Shortcuts to Event Precision Prefabs
		public static EventPrecisionRuleSet SystemTime {
			get { return new EventPrecisionRuleSet_systemTime(); }
		}
		public static EventPrecisionRuleSet Absolute {
			get {
				return new EventPrecisionRuleSet_absolute();
			}
		}
		public static EventPrecisionRuleSet Counter {
			get {
				return new EventPrecisionRuleSet_counter();
			}
		}
		public static EventPrecisionRuleSet ExceptionHandler { get {
			return new EventPrecisionRuleSet_exceptionHandler();
		} }
		#endregion
		
		//How far into the past do we check events when comparing
		public abstract long Range_past { get; }

		//How far into the future do we check events when comparing
		public abstract long Range_future { get; }

		//How much can values "vary" from a result to still return true
		public abstract long Limit_min { get; }
		public abstract long Limit_max { get; }

		//How many digits are compared from a number to get a valid result
		public abstract long Numeric_length_min { get; }
		public abstract long Numeric_length_max { get; }
		
	}

}
