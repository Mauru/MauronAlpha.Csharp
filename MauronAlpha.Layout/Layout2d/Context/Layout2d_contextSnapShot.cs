using System;

using MauronAlpha.Events.Units;


namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A Timed Snapshot of the Values of a LayoutContext
	public class Layout2d_contextSnapShot:Layout2d_context {
		
		//constructor
		public Layout2d_contextSnapShot(EventUnit_timeStamp time, Layout2d_context context):base() {
			TIME_snapshot = time;
			CONTEXT_snapshot = context;		
		}

		private EventUnit_timeStamp TIME_snapshot;
		public EventUnit_timeStamp Time { get { return TIME_snapshot; } }

		private Layout2d_context CONTEXT_snapshot;
		public Layout2d_context Context { get { return CONTEXT_snapshot; } }

	}

}
