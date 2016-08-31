using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Collections {
	public class Subscriptions<T>:MauronCode_eventComponent where T:EventUnit_event {

		public MauronCode_dataList<I_subscriber<T>> Subscribers;

		public int ReceiveEvent(T e)  {
			if(Subscribers == null)
				return 0;
			int count = Subscribers.Count;
			for(int n = 0; n < count; n++) {

				I_subscriber<T> subscriber = Subscribers.Value(n);
				subscriber.ReceiveEvent(e);
			}
				return count;
		}


		public void Add(I_subscriber<T> e) {
			if (Subscribers == null)
				Subscribers = new MauronCode_dataList<I_subscriber<T>>();
			foreach (I_subscriber<T> s in Subscribers)
				if (s.Equals(e))
					return;
			Subscribers.Add(e);
		}
		public void Remove(I_subscriber<T> e) {
			if (Subscribers == null)
				return;
			int ctr = -1;
			foreach (I_subscriber<T> s in Subscribers) {
				ctr++;
				if (s.Equals(e)) { 
					Subscribers.RemoveByKey(ctr);
					return;
				}
			}


		}

	}
}
