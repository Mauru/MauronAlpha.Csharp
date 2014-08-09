using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Data {

	//A set for event data
	public class EventData:MauronCode_dataSet {

		//constructor
		public EventData():base("Event data"){
		}

		public static EventData New { get {
			return new EventData();
		} }
	}

}
