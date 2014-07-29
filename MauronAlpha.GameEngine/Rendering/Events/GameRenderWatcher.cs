using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine.Rendering.Events {

	public class GameRenderWatcher : GameEventShedule {
		public GameRenderWatcher (I_Drawable source)
		: base(
			SheduleType_spawn.Instance,//sheduletype
			GameMasterClock.Instance.RenderClock,//clock
			source,//source
			source,//target
			source.RenderData.E_checkRenderStatus,//condition
			source.RenderData.E_checkDataStatus//result
		) {
			//Everything done in baseclass
		}
	}
}
