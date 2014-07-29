using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.GameEngine.Events {

	public class GameEventShedule_conditional:GameEventShedule {
		public GameEventShedule_conditional(I_GameEventSender source, I_GameEventListener target):base(
			SheduleType_conditional.Instance,
			GameMasterClock.Instance.EventClock,
			source,
			target,
			source.GameEventWatcher.CheckEvent(),
			target.ReceiveEvents(source.GameEventWatcher.Events)
		){}
	}


	public sealed class SheduleType_conditional : GameEventSheduleType {
		#region Singleton

		private static volatile SheduleType_conditional instance=new SheduleType_conditional();
		private static object syncRoot=new Object();

		//constructor singleton multithread safe
		static SheduleType_conditional ( ) { }
		public static SheduleType_conditional Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new SheduleType_conditional();
					}
				}
				return instance;
			}
		}

		#endregion

		public override bool RunOnce { get { return false; } }
		public override bool RunRepeatedly { get { return true; } }
		public override bool RunOnCall { get { return true; } }

		public override string Name {
			get { return "Conditional"; }
		}

	}
}
