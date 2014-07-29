using System;
using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine.Rendering.Events {

	// A timer for render events
	public abstract class GameRenderShedule : GameEventShedule {
		public virtual bool RenderAtStart { get { return false; } }
		public virtual bool RenderEveryFrame { get { return false; } }

		public delegate bool DELEGATE_void();

		//constructor
		public GameRenderShedule (GameEventSheduleType renderShedule, I_Drawable source, Delegate action)
		: base(
			SheduleType_spawn.Instance,
			GameMasterClock.Instance.RenderClock,
			source,
			source.RenderData,
			v => { return (bool) (source.RenderData!=null && source.RenderData.HasResult); },
			n => { source.RenderData.SheduleRender(); }
		){}

		public new I_Drawable Source {
			get {
				return (I_Drawable) base.Source;
			}
		}

		// Do You need to be re-rendered?
		public virtual bool NeedsUpdate(GameRenderShedule renderShedule) {
			return renderShedule.Executed.CompareTo(0) <= 0 && renderShedule.Sheduled.CompareTo(Time) < 0;
		}

		#region Interacting with the shedule
		private GameTick GT_sheduled=new GameTick(GameMasterClock.Instance.RenderClock,0);
		public GameTick LastSheduled { get { return GT_sheduled; } }
		public void SetLastSheduled(GameTick tick) { GT_sheduled = tick; }
		public virtual bool IsSheduled { get { return LastSheduled.CompareTo(0) > 0; } }
		#endregion

		#region Interacting with the Execution of the render
		private GameTick GT_executed;
		public GameTick LastExecuted { get { return GT_executed; } }
		public void SetLastExecuted(GameTick tick) {
			GT_executed=tick;
		}
		public bool HasRendered { get { return Source.HasRendered; } }
		#endregion

		#region What is the time of your event clock?
		public virtual GameTick Time { get { return Clock.Time; } }
		#endregion
	}
}