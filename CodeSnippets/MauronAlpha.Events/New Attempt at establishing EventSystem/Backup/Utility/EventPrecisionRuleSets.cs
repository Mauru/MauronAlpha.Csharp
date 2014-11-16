namespace MauronAlpha.Events.Utility {
	//A Precision RuleSet that has absolute values
	public class EventPrecisionRuleSet_absolute : EventPrecisionRuleSet {

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

	//A recision ruleset made for counters
	public class EventPrecisionRuleSet_counter : EventPrecisionRuleSet {

		public override string Name {
			get { return "counter"; }
		}

		public override long Range_past {
			get { return 0; }
		}

		public override long Range_future {
			get { return -1; }
		}

		public override long Limit_min {
			get { return 0; }
		}
		public override long Limit_max {
			get { return 0; }
		}

		public override long Numeric_length_min {
			get { return -1; }
		}
		public override long Numeric_length_max {
			get { return -1; }
		}
	}

	/// <summary>
	/// Compare "one Event" into the future with absolute precision
	/// </summary>
	/// <remarks>We try to make up for potential discrepancies due to test execution time
	/// (LIMIT should probably be customized for "lag")
	/// </remarks>
	public class EventPrecisionRuleSet_exceptionHandler : EventPrecisionRuleSet {

		public override string Name { get { return "exceptionHandler"; } }
		public override long Range_past { get { return 0; } }
		public override long Range_future { get { return 1; } }

		public override long Limit_min { get { return 0; } }
		public override long Limit_max { get { return 0; } }

		public override long Numeric_length_min { get { return -1; } }
		public override long Numeric_length_max { get { return -1; } }

	}

}
