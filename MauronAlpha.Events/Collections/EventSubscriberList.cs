﻿using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Collections
{

    public class EventSubscriberList:MauronCode_dataMap<MauronCode_dataList<EventUnit_subscription>>{
	
		//constructor
		public EventSubscriberList():base() {}

		public EventSubscriberList Instance {
			get {
				EventSubscriberList result = new EventSubscriberList();
				foreach(string key in Keys) {
					result.AddKey(key);
					long index = IndexOfKey(key);
					if(ContainsValueAtIndex(index)) {
						result.SetValue(key,ValueByIndex(index));
					}
				}
				return result;
			}		
		}
		public new EventSubscriberList SetIsReadOnly(bool state) {
			base.SetIsReadOnly(state);
			return this;
		}
	
		public MauronCode_dataList<EventUnit_subscription> ByCode(string code) {
			if(!ContainsKey(code)) {
				return new MauronCode_dataList<EventUnit_subscription>();
			}
			return Value(code);
		}

		public bool Equals(EventSubscriberList other){
			long count = KeyCount;
		
			if(count!=other.KeyCount) return false;

			for(long index = 0; index < count; index++) {
				if(!ContainsValueAtIndex(index)&&other.ContainsValueAtIndex(index))
					return false;
				MauronCode_dataList<EventUnit_subscription> source = ValueByIndex(index);
				MauronCode_dataList<EventUnit_subscription> candidate = other.ValueByIndex(index);

				if(!source.Equals(candidate))
					return false;
			}
			return true;

		}
	}

}
