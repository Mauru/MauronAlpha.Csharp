namespace MauronAlpha.GameEngine.Events {

	//A type describing an event shedule
	public abstract class GameEventSheduleType : MauronCode_subtype {
		private string str_name="";
		public abstract string Name { get; }

		//Run the event only once
		public virtual bool RunOnce { get { return true; } }
		//Run the event Repeatedly
		public virtual bool RunRepeatedly { get { return false; } }
		//Run the event manually
		public virtual bool RunOnCall { get { return false; } }

	}
}
