using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Collections {

	public class EventModels:MauronCode_eventComponent {

		public static EventUnit_subscriptionModel Continous {
			get {
				return new EventUnit_subscriptionModel_continous();
			}
		}

		//Runs continously
		public class EventUnit_subscriptionModel_continous:EventUnit_subscriptionModel {

            //Continous events use a trigger
            public override bool UsesTrigger {
                get {
                    return true;
                }
            }
        }
	}
}
