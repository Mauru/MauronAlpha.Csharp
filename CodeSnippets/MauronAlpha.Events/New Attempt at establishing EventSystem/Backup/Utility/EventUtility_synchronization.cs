using MauronAlpha.Events.Units;

using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Utility {


	//A utility class that can synchronize two clocks (or at least tries to do it)
	public class EventUtility_synchronization:EventUtility {

		//constructor
		public EventUtility_synchronization(EventUtility_precision precisionHandler) {
			UTILITY_precision = precisionHandler;
		}
		private EventUtility_precision UTILITY_precision;

		public EventPrecisionRuleSet PrecisionRuleSet { get { return UTILITY_precision.RuleSet; } }

		public EventUtility_synchronization Instance {
			get {
				return new EventUtility_synchronization(UTILITY_precision);
			}
		}

	}
}
