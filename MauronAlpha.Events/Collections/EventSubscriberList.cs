using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Collections
{

    public class EventSubscriberList:MauronCode_dataMap<MauronCode_dataList<EventUnit_subscription>>{
	
		//constructor
		public EventSubscriberList():base() {}

		public EventSubscriberList Instance {
			get {
				
			}		
		}
	}

}
