using System;

using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Interfaces {

	//A subscriber to an event
    public interface I_eventSubscriber {

		bool Equals( I_eventSubscriber other );

		bool ReceiveEvent( EventUnit_event e );

		string Id { get; }
		
    }

	public interface I_subscriber<T> where T:EventUnit_event {

		bool ReceiveEvent( T e );
		bool Equals(I_subscriber<T> other);

		string Id { get; }

	}

}
