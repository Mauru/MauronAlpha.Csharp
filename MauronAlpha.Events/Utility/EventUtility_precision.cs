namespace MauronAlpha.Events.Utility {

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
	}

	public abstract class EventPrecisionRuleSet:MauronCode_eventComponent {

		public abstract string Name { get; }

		public virtual bool Equals(EventPrecisionRuleSet other) {
			return Name==other.Name;
		}

		public static EventPrecisionRuleSet SystemTime {
			get { return new EventPrecisionRuleSet_systemTime(); }
		}
		public abstract long Limit_past { get; }
		public abstract long Limit_future { get; }
		
	}

	public class EventPrecisionRuleSet_absolute:EventPrecisionRuleSet {
		public override string Name { get { return "absolute"; } }
		public override long Limit_past { get { return -1; } }
		public override long Limit_future { get { return -1; } }
	}

	public class EventPrecisionRuleSet_systemTime : EventPrecisionRuleSet {
		public override string Name { get { return "systemTime"; } }
		public override long Limit_past { get { return 0; } }
		public override long Limit_future { get { return 1; } }
	}
}
