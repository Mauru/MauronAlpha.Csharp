using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Data {

	//A set for event data
	public class EventData:MauronCode_dataSet {
		public static EventData New { get {
			return new EventData();
		} }
	}

}
